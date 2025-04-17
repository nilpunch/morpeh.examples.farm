using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Feature.Events;
using Scellecs.Morpeh.Systems;
using UnityEngine;

namespace Farm
{
	[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(SeedStageSystem))]
	public class SeedStageSystem : UpdateSystem
	{
		private Filter _plantsInSeedStage;
		private Stash<GrowthRequest> _growthRequests;
		private Stash<Progress> _progresses;

		public override void OnAwake()
		{
			_plantsInSeedStage = World.Filter
				.With<Plant>()
				.With<SeedStage>()
				.With<Progress>().Build();

			_growthRequests = World.GetStash<GrowthRequest>();
			_progresses = World.GetStash<Progress>();
		}

		public override void OnUpdate(float deltaTime)
		{
			foreach (var entity in _plantsInSeedStage)
			{
				ref var progress = ref _progresses.Get(entity);

				if (progress.IsDone)
				{
					_growthRequests.SetEvent(new GrowthRequest()
					{
						Entity = entity,
						GrowthStage = GrowthStage.Mature,
					});
				}
			}
		}
	}
}
