using System;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Farm
{
	[AddComponentMenu("Farm/MatureStage")]
	public sealed class MatureStageProvider : MonoProvider<MatureStage>
	{
		protected override void Initialize()
		{
			GetData().FruitingSpeedVariation = Random.Range(0f, 0.9f);
		}
	}

	[Serializable]
	public struct MatureStage : IComponent
	{
		public float FruitingProgress;
		public float FruitingSpeed;

		/// <summary>
		/// 0 - no variation. 1 - full random, may result in zero speed.
		/// </summary>
		public float FruitingSpeedVariation;
	}
}
