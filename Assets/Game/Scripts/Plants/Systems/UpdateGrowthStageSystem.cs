using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.OneShot;
using Scellecs.Morpeh.Systems;
using UnityEngine;

namespace Farm
{
	[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(UpdateGrowthStageSystem))]
	public class UpdateGrowthStageSystem : UpdateSystem
	{
		private Stash<GrowthEvent> _growthEvents;

		private Stash<Plant> _plants;
		private Stash<SeedStage> _seeds;
		private Stash<MatureStage> _matures;
		private Stash<FruitingStage> _fruits;

		public override void OnAwake()
		{
			World.RegisterOneShot<GrowthEvent>();
			_growthEvents = World.GetStash<GrowthEvent>();

			_plants = World.GetStash<Plant>();
			_seeds = World.GetStash<SeedStage>();
			_matures = World.GetStash<MatureStage>();
			_fruits = World.GetStash<FruitingStage>();
		}

		public override void OnUpdate(float deltaTime)
		{
			foreach (var growthEvent in _growthEvents)
			{
				var entity = growthEvent.Entity;
				ref var plant = ref _plants.Get(entity);

				switch (growthEvent.GrowthStage)
				{
					case GrowthStage.Seed:
						_matures.Remove(entity);
						_fruits.Remove(entity);
						_seeds.Set(entity, new SeedStage()
						{
							GrowSpeedVariation = Random.Range(0f, plant.Config.GrowSpeedVariation)
						});
						break;
					case GrowthStage.Mature:
						_seeds.Remove(entity);
						_fruits.Remove(entity);
						_matures.Set(entity, new MatureStage()
						{
							FruitingSpeedVariation = Random.Range(0f, plant.Config.FruitingSpeedVariation)
						});
						break;
					case GrowthStage.Fruiting:
						_seeds.Remove(entity);
						_matures.Remove(entity);
						_fruits.Set(entity);
						break;
				}
			}
		}
	}
}
