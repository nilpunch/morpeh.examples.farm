using System;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEditor;
using UnityEngine;

namespace Farm
{
	[AddComponentMenu("Farm/Plant")]
	public sealed class PlantProvider : MonoProvider<Plant>
	{
		[SerializeField] private PlantConfig _plantConfig;

		protected override void Initialize()
		{
			GetData().Position = transform.position;
			GetData().Config = _plantConfig;
		}
	}

	[Serializable]
	public struct Plant : IComponent
	{
		public Vector3 Position;
		public ResourceReference<PlantConfig> Config;
	}
}
