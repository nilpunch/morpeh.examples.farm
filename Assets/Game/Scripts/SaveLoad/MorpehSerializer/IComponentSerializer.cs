using System.Collections.Generic;
using System.IO;
using Scellecs.Morpeh;

namespace Farm
{
	public interface IComponentSerializer
	{
		void CollectEntityIds(World world, HashSet<int> entities);

		void Serialize(Stream stream, World world);

		void Deserialize(Stream stream, World world, Dictionary<int, Entity> entitiesMap);
	}
}
