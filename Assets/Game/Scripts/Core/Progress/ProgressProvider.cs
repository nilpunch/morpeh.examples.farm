using System;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Farm
{
	[AddComponentMenu("Farm/Progress")]
	public sealed class ProgressProvider : MonoProvider<Progress>
	{
		protected override void Initialize()
		{
			GetData().SpeedMultiplier = Random.Range(0.3f, 1f);
		}
	}

	[Serializable]
	public struct Progress : IComponent
	{
		public float ElapsedTime;
		public float SpeedMultiplier;
	}
}
