using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Scellecs.Morpeh;

namespace Farm
{
	public class EntitiesSerializer
	{
		public void Serialize(Stream stream, HashSet<int> entityIds)
		{
			var savedEntityIds = new int[entityIds.Count];
			var count = 0;
			foreach (var entityId in entityIds)
			{
				savedEntityIds[count++] = entityId;
			}

			stream.Write(BitConverter.GetBytes(savedEntityIds.Length * 4));
			stream.Write(MemoryMarshal.AsBytes((Span<int>)savedEntityIds));
		}

		public Dictionary<int, Entity> Deserialize(Stream stream, World world)
		{
			Span<byte> bytesCountBuffer = stackalloc byte[4];
			_ = stream.Read(bytesCountBuffer);

			var bytesCount = BitConverter.ToInt32(bytesCountBuffer);
			var bytes = new byte[bytesCount];
			stream.Read(bytes);

			var savedEntityIds = MemoryMarshal.Cast<byte, int>(bytes);

			var entitiesMap = new Dictionary<int, Entity>();
			foreach (var entityId in savedEntityIds)
			{
				var entity = world.CreateEntity();
				entitiesMap.Add(entityId, entity);
			}

			return entitiesMap;
		}
	}
}
