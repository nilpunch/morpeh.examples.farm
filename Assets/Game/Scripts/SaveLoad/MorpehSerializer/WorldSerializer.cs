using System;
using System.Collections.Generic;
using System.IO;
using Scellecs.Morpeh;

namespace Farm
{
	public class WorldSerializer
	{
		private readonly EntitiesSerializer _entitiesSerializer = new EntitiesSerializer();
		private readonly List<IComponentSerializer> _componentSerializers = new List<IComponentSerializer>();
		private readonly HashSet<Type> _registeredTypes = new HashSet<Type>();

		public WorldSerializer RegisterComponent<T>() where T : struct, IComponent
		{
			if (!_registeredTypes.Add(typeof(T)))
			{
				throw new Exception($"Component of type {typeof(T).FullName} is already registered.");
			}
			_componentSerializers.Add(new ComponentSerializer<T>());
			return this;
		}

		public void Serialize(Stream stream, World world)
		{
			var involvedEntityIds = new HashSet<int>();

			foreach (var serializer in _componentSerializers)
			{
				serializer.CollectEntityIds(world, involvedEntityIds);
			}

			_entitiesSerializer.Serialize(stream, involvedEntityIds);

			foreach (var serializer in _componentSerializers)
			{
				serializer.Serialize(stream, world);
			}
		}

		public void Deserialize(Stream stream, World world)
		{
			var entitiesMap = _entitiesSerializer.Deserialize(stream, world);

			foreach (var serializer in _componentSerializers)
			{
				serializer.Deserialize(stream, world, entitiesMap);
			}
		}
	}
}
