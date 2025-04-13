using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Feature.Events;
using Scellecs.Morpeh.Systems;
using UnityEngine;

namespace Farm
{
	[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlantSeedStageSystem))]
	public class PlantSeedStageSystem : UpdateSystem
	{
		private Filter _plantsInSeedStage;
		private Stash<GrowthEvent> _growthEvents;
		private Stash<SeedStage> _seeds;
		private Stash<Plant> _plants;

		public override void OnAwake()
		{
			_plantsInSeedStage = World.Filter.With<Plant>().With<SeedStage>().Build();
			_growthEvents = World.GetStash<GrowthEvent>();
			_seeds = World.GetStash<SeedStage>();
			_plants = World.GetStash<Plant>();
		}

		public override void OnUpdate(float deltaTime)
		{
			foreach (var entity in _plantsInSeedStage)
			{
				ref var seed = ref _seeds.Get(entity);
				ref var plant = ref _plants.Get(entity);
				seed.GrowProgress += seed.GrowSpeed * Time.deltaTime;

				if (seed.GrowProgress >= plant.Config.GrowTime)
				{
					_growthEvents.SetEvent(new GrowthEvent()
					{
						Entity = entity,
						GrowthStage = GrowthStage.Mature,
					});
				}
			}
		}
	}
}
