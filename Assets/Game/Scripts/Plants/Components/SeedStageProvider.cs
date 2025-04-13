using System;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Farm
{
	[AddComponentMenu("Farm/SeedStage")]
	public sealed class SeedStageProvider : MonoProvider<SeedStage>
	{
		protected override void Initialize()
		{
			GetData().GrowSpeedVariation = Random.Range(0f, 0.9f);
		}
	}

	[Serializable]
	public struct SeedStage : IComponent
	{
		public float GrowProgress;

		public float GrowSpeed;

		/// <summary>
		/// 0 - no variation. 1 - full random, may result in zero speed.
		/// </summary>
		public float GrowSpeedVariation;
	}
}
