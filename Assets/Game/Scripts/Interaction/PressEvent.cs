using Scellecs.Morpeh;
using UnityEngine;

namespace Farm
{
	public struct PressEvent : IComponent
	{
		public Entity Entity;
		public Vector3 HitPosition;
	}
}
