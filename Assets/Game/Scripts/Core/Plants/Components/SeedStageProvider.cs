using System;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace Farm
{
	[AddComponentMenu("Farm/SeedStage")]
	public sealed class SeedStageProvider : MonoProvider<SeedStage>
	{
	}

	[Serializable]
	public struct SeedStage : IComponent
	{
	}
}
