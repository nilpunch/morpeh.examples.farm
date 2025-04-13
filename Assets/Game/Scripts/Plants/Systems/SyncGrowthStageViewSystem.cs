using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;

namespace Farm
{
	[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(SyncGrowthStageViewSystem))]
	public class SyncGrowthStageViewSystem : UpdateSystem
	{
		private Stash<GrowthEvent> _growthEvents;

		private Stash<ViewHolder> _viewHolders;

		public override void OnAwake()
		{
			_growthEvents = World.GetStash<GrowthEvent>();
			
			_viewHolders = World.GetStash<ViewHolder>();
		}

		public override void OnUpdate(float deltaTime)
		{
			foreach (var growthEvent in _growthEvents)
			{
				var growthView = _viewHolders.Get(growthEvent.Entity).View.GetComponent<GrowthStageView>();

				switch (growthEvent.GrowthStage)
				{
					case GrowthStage.Seed:
						growthView.ShowSeed();
						break;
					case GrowthStage.Mature:
						growthView.ShowMature();
						break;
					case GrowthStage.Fruiting:
						growthView.ShowFruits();
						break;
				}
			}
		}
	}
}
