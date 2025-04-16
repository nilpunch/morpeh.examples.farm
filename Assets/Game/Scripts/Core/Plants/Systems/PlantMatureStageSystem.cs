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
		private Stash<Plant> _plants;
		private Stash<Progress> _progresses;

		public override void OnAwake()
		{
			_maturePlants = World.Filter
				.With<Plant>()
				.With<MatureStage>()
				.With<Progress>().Build();

			_growthEvents = World.GetStash<GrowthEvent>();
			_plants = World.GetStash<Plant>();
			_progresses = World.GetStash<Progress>();
		}

		public override void OnUpdate(float deltaTime)
		{
			foreach (var entity in _maturePlants)
			{
				ref var plant = ref _plants.Get(entity);
				ref var progress = ref _progresses.Get(entity);

				if (progress.ElapsedTime >= plant.Config.Value.FruitTime)
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
