using System;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Farm
{
	[AddComponentMenu("Farm/Plant")]
	public sealed class PlantProvider : EntityProvider
	{
		[SerializeField] private PlantConfig _plantConfig;

		protected override void Initialize()
		{
			Entity.SetComponent(new Plant()
			{
				Position = transform.position,
				ConfigResource = new Resource<PlantConfig>(ResourceDB.GetResourceId(_plantConfig))
			});

			Entity.SetComponent(new Progress()
			{
				TargetTime = _plantConfig.GrowTime, 
				SpeedMultiplier = 1f - Random.Range(0f, _plantConfig.GrowSpeedVariation)
			});
		}

		protected override void Deinitialize()
		{
			Entity.RemoveComponent<Plant>();
			Entity.RemoveComponent<Progress>();
		}
	}

	[Serializable]
	public struct Plant : IComponent
	{
		public Vector3 Position;
		public Resource<PlantConfig> ConfigResource;

		public PlantConfig Config => ConfigResource.Value;
	}
}
