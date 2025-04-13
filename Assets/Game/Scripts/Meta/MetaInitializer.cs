using Scellecs.Morpeh;
using UnityEngine;
using Initializer = Scellecs.Morpeh.Systems.Initializer;

namespace Farm
{
	[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(MetaInitializer))]
	public class MetaInitializer : Initializer
	{
		private Stash<FruitStock> _woodStock;

		public override void OnAwake()
		{
			_woodStock = World.GetStash<FruitStock>();

			_woodStock.Add(World.CreateEntity()).Amount = PlayerPrefs.GetInt(nameof(FruitStock), 0);
		}

		public override void Dispose()
		{
			PlayerPrefs.SetInt(nameof(FruitStock), _woodStock.Single().Amount);
			PlayerPrefs.Save();
		}
	}
}
