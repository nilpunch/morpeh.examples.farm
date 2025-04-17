using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Feature.Events;
using Scellecs.Morpeh.Systems;
using UnityEngine;

namespace Farm
{
	[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(SeedPlantingSystem))]
	public class SeedPlantingSystem : UpdateSystem
	{
		[SerializeField] private LayerMask _layerMask = -1;
		[SerializeField] private PlantConfig _plantConfig;

		private Stash<Plant> _plants;
		private Stash<Progress> _progresses;
		private Stash<SeedStage> _seeds;
		private Stash<FruitStock> _fruitStock;
		private Stash<ViewChangeRequest> _viewChangeRequests;

		public override void OnAwake()
		{
			_plants = World.GetStash<Plant>();
			_progresses = World.GetStash<Progress>();
			_seeds = World.GetStash<SeedStage>();
			_fruitStock = World.GetStash<FruitStock>();
			_viewChangeRequests = World.GetStash<ViewChangeRequest>();
		}

		public override void OnUpdate(float deltaTime)
		{
			if (!Input.GetMouseButtonDown(1) || float.IsInfinity(Input.mousePosition.x))
			{
				return;
			}

			if (_fruitStock.Single().Amount < _plantConfig.PlantCost)
			{
				return;
			}
			
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out var hit))
			{
				if (hit.collider.TryGetComponent<EntityView>(out _))
				{
					// Skip non-empty places.
					return;
				}

				// Pay the price.
				_fruitStock.Single().Amount -= _plantConfig.PlantCost;

				var entity = World.CreateEntity();
				_plants.Set(entity, new Plant()
				{
					Position = hit.point,
					ConfigResource = new Resource<PlantConfig>(ResourceDB.GetResourceId(_plantConfig))
				});
				_seeds.Add(entity);
				_progresses.Set(entity, new Progress()
				{
					TargetTime = _plantConfig.GrowTime,
					SpeedMultiplier = 1f - Random.Range(0f, _plantConfig.GrowSpeedVariation),
				});
				_viewChangeRequests.SetEvent(new ViewChangeRequest()
				{
					Entity = entity,
					ViewPrefab = _plantConfig.PlantPrefab,
					Position = hit.point,
				});
			}
		}
	}
}
