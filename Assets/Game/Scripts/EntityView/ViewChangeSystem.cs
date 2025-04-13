using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;

namespace Farm
{
	[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ViewChangeSystem))]
	public class ViewChangeSystem : UpdateSystem
	{
		private Stash<ViewHolder> _viewHolders;
		private Stash<ViewChangeRequest> _changeRequests;

		public override void OnAwake()
		{
			_viewHolders = World.GetStash<ViewHolder>();
			_changeRequests = World.GetStash<ViewChangeRequest>();
		}

		public override void OnUpdate(float deltaTime)
		{
			foreach (var changeRequest in _changeRequests)
			{
				var requestedEntity = changeRequest.Entity;

				if (_viewHolders.Has(requestedEntity))
				{
					var view = _viewHolders.Get(requestedEntity).View;
					if (view != null)
					{
						Destroy(view.gameObject);
					}
				}

				if (changeRequest.ViewPrefab != null)
				{
					var viewHolder = new ViewHolder();
					viewHolder.View = Instantiate(changeRequest.ViewPrefab, changeRequest.Position, Quaternion.identity);
					viewHolder.View.AssignEntity(requestedEntity);
					_viewHolders.Set(requestedEntity, viewHolder);
				}
			}

			_changeRequests.RemoveAll();
		}
	}
}
