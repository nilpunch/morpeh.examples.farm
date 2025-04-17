using System;
using Scellecs.Morpeh;

namespace Farm
{
	[Serializable]
	public struct Progress : IComponent
	{
		public float ElapsedTime;
		public float TargetTime;
		public float SpeedMultiplier;

		public bool IsDone => ElapsedTime >= TargetTime;
	}
}
