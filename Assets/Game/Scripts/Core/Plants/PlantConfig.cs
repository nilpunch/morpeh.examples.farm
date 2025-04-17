using System;
using UnityEngine;

namespace Farm
{
	[CreateAssetMenu(menuName = "ECS/Configs/" + nameof(PlantConfig))]
	public class PlantConfig : ScriptableObject
	{
		public string PlantName;
		public EntityView PlantPrefab;

		public float GrowTime = 10f;
		[Range(0f, 0.95f)]
		public float GrowSpeedVariation = 0f;

		public float FruitTime = 5f;
		[Range(0f, 0.95f)]
		public float FruitingSpeedVariation = 0f;

		public int WorthFruits = 10;
		public int PlantCost = 10;
	}
}
