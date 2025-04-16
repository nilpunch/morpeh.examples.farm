using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Feature.Events;
using Scellecs.Morpeh.Systems;
using UnityEngine;

namespace Farm
{
	[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(InitializePlantsViewsSystem))]
	public class InitializePlantsViewsSystem : Initializer
	{
		public override void OnAwake()
		{
			var plants = World.GetStash<Plant>();
			var viewChangeRequests = World.GetStash<ViewChangeRequest>();
			var allPlantsWithNoViews = World.Filter.With<Plant>().Without<ViewHolder>().Build();

			foreach (var entity in allPlantsWithNoViews)
			{
				ref var plant = ref plants.Get(entity);

				viewChangeRequests.SetEvent(new ViewChangeRequest()
				{
					Entity = entity,
					Position = plant.Position,
					ViewPrefab = plant.Config.Value.PlantPrefab,
				});
			}

			allPlantsWithNoViews.Dispose();
		}
	}
}
