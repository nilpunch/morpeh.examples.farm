﻿using System;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace Farm
{
	[AddComponentMenu("Farm/MatureStage")]
	public sealed class MatureStageProvider : MonoProvider<MatureStage>
	{
	}

	[Serializable]
	public struct MatureStage : IComponent
	{
	}
}
