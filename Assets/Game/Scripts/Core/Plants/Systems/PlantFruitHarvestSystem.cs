using System;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Feature.Events;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Farm
{
	[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlantFruitHarvestSystem))]
	public class PlantFruitHarvestSystem : UpdateSystem
	{
		private Stash<FruitStock> _fruitStock;

		private Stash<GrowthEvent> _growthEvents;
		private Stash<Plant> _plants;
		private Stash<FruitingStage> _fruits;

		private Stash<PressEvent> _pressEvents;

		public override void OnAwake()
		{
			_fruitStock = World.GetStash<FruitStock>();

			_growthEvents = World.GetStash<GrowthEvent>();
			_plants = World.GetStash<Plant>();
			_fruits = World.GetStash<FruitingStage>();

			_pressEvents = World.GetStash<PressEvent>();
		}

		public override void OnUpdate(float deltaTime)
		{
			if (_pressEvents.IsEmpty())
			{
				return;
			}

			var entity = _pressEvents.Single().Entity;

			if (_fruits.Has(entity))
			{
				var plantConfig = _plants.Get(entity).Config.Value;

				// Add fruits to stock.
				_fruitStock.Single().Amount += plantConfig.WorthFruits;

				// Change fruit stage to mature.
				_growthEvents.SetEvent(new GrowthEvent()
				{
					Entity = entity,
					GrowthStage = GrowthStage.Mature,
				});
			}
		}
	}
}
