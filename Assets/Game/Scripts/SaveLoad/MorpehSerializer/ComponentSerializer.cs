using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Scellecs.Morpeh;
using UnityEngine;

namespace Farm
{
	public class ComponentSerializer<T> : IComponentSerializer where T : struct, IComponent
	{
		[Serializable]
		public struct SavedEntry
		{
			public int EntityId;
			public T Component;
		}

		[Serializable]
		public struct EntriesArray
		{
			public SavedEntry[] Array;
		}

		public void CollectEntityIds(World world, HashSet<int> entities)
		{
			var filter = world.Filter.With<T>().Build();
			foreach (var entity in filter)
			{
				entities.Add(entity.Id);
			}
			filter.Dispose();
		}

		public void Serialize(Stream stream, World world)
		{
			var stash = world.GetStash<T>();
			var filter = world.Filter.With<T>().Build();

			// Prepare entities to save.
			var savedEntries = new SavedEntry[filter.GetLengthSlow()];
			var count = 0;
			foreach (var entity in filter)
			{
				savedEntries[count++] = new SavedEntry()
				{
					EntityId = entity.Id,
					Component = stash.Get(entity),
				};
			}

			// Save using json, converted in bytes.
			var json = JsonUtility.ToJson(new EntriesArray() { Array = savedEntries });
			var bytes = Encoding.Default.GetBytes(json);

			stream.Write(BitConverter.GetBytes(bytes.Length));
			stream.Write(bytes);

			filter.Dispose();
		}

		public void Deserialize(Stream stream, World world, Dictionary<int, Entity> entitiesMap)
		{
			var stash = world.GetStash<T>();

			Span<byte> bytesCountBuffer = stackalloc byte[4];
			_ = stream.Read(bytesCountBuffer);

			var bytesCount = BitConverter.ToInt32(bytesCountBuffer);
			var bytes = new byte[bytesCount];
			stream.Read(bytes);

			var json = Encoding.Default.GetString(bytes);
			var savedEntries = JsonUtility.FromJson<EntriesArray>(json).Array;

			foreach (var savedEntry in savedEntries)
			{
				var targetEntity = entitiesMap[savedEntry.EntityId];
				stash.Set(targetEntity, savedEntry.Component);
			}
		}
	}
}
