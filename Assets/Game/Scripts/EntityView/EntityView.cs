using Scellecs.Morpeh;
using UnityEngine;

namespace Farm
{
	public class EntityView : MonoBehaviour
	{
		[SerializeField] private ViewBehaviour[] _viewBehaviours;

		public Entity Entity { get; private set; }

		public void AssignEntity(Entity entity)
		{
			Entity = entity;
			foreach (var viewBehaviour in _viewBehaviours)
			{
				viewBehaviour.OnEntityAssigned(entity);
			}
		}
	}
}
