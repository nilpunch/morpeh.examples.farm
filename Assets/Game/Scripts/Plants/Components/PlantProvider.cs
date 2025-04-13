using System;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace Farm
{
	[AddComponentMenu("Farm/Plant")]
	public sealed class PlantProvider : MonoProvider<Plant>
	{
		protected override void Initialize()
		{
			GetData().Position = transform.position;
		}
	}

	[Serializable]
	public struct Plant : IComponent
	{
		public Vector3 Position;
		public PlantConfig Config;
	}
}
