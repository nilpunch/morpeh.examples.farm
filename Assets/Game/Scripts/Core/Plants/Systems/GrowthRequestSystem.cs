using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Feature.Events;
using Scellecs.Morpeh.Addons.OneShot;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Farm
{
	[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(GrowthRequestSystem))]
	public class GrowthRequestSystem : UpdateSystem
	{
		private Stash<GrowthRequest> _growthRequests;
		private Stash<GrowthEvent> _growthEvents;

		private Stash<Plant> _plants;
		private Stash<SeedStage> _seeds;
		private Stash<MatureStage> _matures;
		private Stash<FruitingStage> _fruits;
		private Stash<Progress> _progresses;

		public override void OnAwake()
		{
			World.RegisterOneShot<GrowthEvent>();

			_growthRequests = World.GetStash<GrowthRequest>();
			_growthEvents = World.GetStash<GrowthEvent>();

			_plants = World.GetStash<Plant>();
			_seeds = World.GetStash<SeedStage>();
			_matures = World.GetStash<MatureStage>();
			_fruits = World.GetStash<FruitingStage>();
			_progresses = World.GetStash<Progress>();
		}

		public override void OnUpdate(float deltaTime)
		{
			foreach (var growthRequest in _growthRequests)
			{
				var entity = growthRequest.Entity;
				ref var plant = ref _plants.Get(entity);

				switch (growthRequest.GrowthStage)
				{
					case GrowthStage.Seed:
						_seeds.Set(entity);
						_matures.Remove(entity);
						_fruits.Remove(entity);
						_progresses.Set(entity, new Progress()
						{
							TargetTime = plant.Config.GrowTime,
							SpeedMultiplier = 1f - Random.Range(0f, plant.Config.GrowSpeedVariation)
						});
						break;
					case GrowthStage.Mature:
						_seeds.Remove(entity);
						_matures.Set(entity);
						_fruits.Remove(entity);
						_progresses.Set(entity, new Progress()
						{
							TargetTime = plant.Config.FruitTime,
							SpeedMultiplier = 1f - Random.Range(0f, plant.Config.FruitingSpeedVariation)
						});
						break;
					case GrowthStage.Fruiting:
						_seeds.Remove(entity);
						_matures.Remove(entity);
						_fruits.Set(entity);
						_progresses.Remove(entity);
						break;
				}

				_growthEvents.SetEvent(new GrowthEvent()
				{
					Entity = entity,
					GrowthStage = growthRequest.GrowthStage
				});
			}

			_growthRequests.RemoveAll();
		}
	}
}
