using Scellecs.Morpeh;
using UnityEngine;
using Initializer = Scellecs.Morpeh.Systems.Initializer;

namespace Farm
{
	[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(MetaInitializer))]
	public class MetaInitializer : Initializer
	{
		private Stash<FruitStock> _fruitStock;

		public override void OnAwake()
		{
			_fruitStock = World.GetStash<FruitStock>();

			if (_fruitStock.IsEmpty())
			{
				_fruitStock.Add(World.CreateEntity()).Amount = 0;
			}
		}
	}
}
