using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Feature.Events;
using Scellecs.Morpeh.Addons.OneShot;
using Scellecs.Morpeh.Systems;
using UnityEngine;

namespace Farm
{
	[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(InteractionSystem))]
	public class InteractionSystem : UpdateSystem
	{
		[SerializeField] private LayerMask _layerMask = -1;

		private Stash<HoverEvent> _hoverEvents;
		private Stash<PressEvent> _pressEvents;

		public override void OnAwake()
		{
			World.RegisterOneShot<HoverEvent>();
			World.RegisterOneShot<PressEvent>();

			_hoverEvents = World.GetStash<HoverEvent>();
			_pressEvents = World.GetStash<PressEvent>();
		}

		public override void OnUpdate(float deltaTime)
		{
			if (float.IsInfinity(Input.mousePosition.x))
			{
				return;
			}

			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out var hit))
			{
				if (hit.collider.TryGetComponent<EntityView>(out var entityView))
				{
					_hoverEvents.SetEvent(new HoverEvent()
					{
						Entity = entityView.Entity,
						HitPosition = hit.point
					});

					if (Input.GetMouseButtonDown(0))
					{
						_pressEvents.SetEvent(new PressEvent()
						{
							Entity = entityView.Entity,
							HitPosition = hit.point
						});
					}
				}
			}
		}
	}
}
