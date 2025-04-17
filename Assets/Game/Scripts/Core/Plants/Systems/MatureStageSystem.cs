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
		private Stash<GrowthRequest> _growthRequests;
		private Stash<Progress> _progresses;

		public override void OnAwake()
		{
			_maturePlants = World.Filter
				.With<Plant>()
				.With<MatureStage>()
				.With<Progress>().Build();

			_growthRequests = World.GetStash<GrowthRequest>();
			_progresses = World.GetStash<Progress>();
		}

		public override void OnUpdate(float deltaTime)
		{
			foreach (var entity in _maturePlants)
			{
				ref var progress = ref _progresses.Get(entity);

				if (progress.IsDone)
				{
					_growthRequests.SetEvent(new GrowthRequest()
					{
						Entity = entity,
						GrowthStage = GrowthStage.Fruiting,
					});
				}
			}
		}
	}
}
