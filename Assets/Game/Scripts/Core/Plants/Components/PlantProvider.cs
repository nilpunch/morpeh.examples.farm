using System;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
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
			GetData().ConfigId = _plantConfig.Id;
		}
	}

	[Serializable]
	public struct Plant : IComponent
	{
		public Vector3 Position;
		public string ConfigId;

		[NonSerialized] private PlantConfig _config;

		public PlantConfig Config => _config ??= ConfigDB.Get<PlantConfig>(ConfigId);
	}
}
