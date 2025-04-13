using Scellecs.Morpeh;
using UnityEngine;

namespace Farm
{
	public struct ViewChangeRequest : IComponent
	{
		public Entity Entity;
		public EntityView ViewPrefab;
		public Vector3 Position;
	}
}
