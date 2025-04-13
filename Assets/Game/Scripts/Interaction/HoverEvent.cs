using Scellecs.Morpeh;
using UnityEngine;

namespace Farm
{
	public struct HoverEvent : IComponent
	{
		public Entity Entity;
		public Vector3 HitPosition;
	}
}