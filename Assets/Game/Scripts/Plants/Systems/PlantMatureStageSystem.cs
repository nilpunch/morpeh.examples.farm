using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Feature.Events;
using Scellecs.Morpeh.Systems;
using UnityEngine;

namespace Farm
{
	[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlantMatureStageSystem))]
	public class PlantMatureStageSystem : UpdateSystem
	{
		private Filter _maturePlants;
		private Stash<GrowthEvent> _growthEvents;
		private Stash<MatureStage> _mature;
		private Stash<Plant> _plants;

		public override void OnAwake()
		{
			_maturePlants = World.Filter.With<Plant>().With<MatureStage>().Build();

			_growthEvents = World.GetStash<GrowthEvent>();
			_mature = World.GetStash<MatureStage>();
			_plants = World.GetStash<Plant>();
		}

		public override void OnUpdate(float deltaTime)
		{
			foreach (var entity in _maturePlants)
			{
				ref var mature = ref _mature.Get(entity);
				ref var plant = ref _plants.Get(entity);
				mature.FruitingProgress += mature.FruitingSpeed * Time.deltaTime;

				if (mature.FruitingProgress >= plant.Config.FruitTime)
				{
					_growthEvents.SetEvent(new GrowthEvent()
					{
						Entity = entity,
						GrowthStage = GrowthStage.Fruiting,
					});
				}
			}
		}
	}
}
