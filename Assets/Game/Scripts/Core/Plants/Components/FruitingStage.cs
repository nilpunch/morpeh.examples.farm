using System;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace Farm
{
	[AddComponentMenu("Farm/FruitingStage")]
	public sealed class FruitingStageProvider : MonoProvider<FruitingStage>
	{
	}

	[Serializable]
	public struct FruitingStage : IComponent
	{
	}
}
