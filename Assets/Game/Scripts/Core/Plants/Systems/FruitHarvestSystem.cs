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

		private Stash<GrowthRequest> _growthRequests;
		private Stash<Plant> _plants;
		private Stash<FruitingStage> _fruits;

		private Stash<PressEvent> _pressEvents;

		public override void OnAwake()
		{
			_fruitStock = World.GetStash<FruitStock>();

			_growthRequests = World.GetStash<GrowthRequest>();
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
				// Add fruits to stock.
				_fruitStock.Single().Amount += _plants.Get(entity).Config.WorthFruits;

				// Change fruit stage to mature.
				_growthRequests.SetEvent(new GrowthRequest()
				{
					Entity = entity,
					GrowthStage = GrowthStage.Mature,
				});
			}
		}
	}
}
