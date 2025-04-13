using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;

namespace Farm
{
	[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(UpdateGrowAndFruitSpeedSystem))]
	public class UpdateGrowAndFruitSpeedSystem : UpdateSystem
	{
		private Stash<SeedStage> _seeds;
		private Stash<MatureStage> _matures;

		public override void OnAwake()
		{
			_seeds = World.GetStash<SeedStage>();
			_matures = World.GetStash<MatureStage>();
		}

		public override void OnUpdate(float deltaTime)
		{
			// Just setting the default, but this can be anything.
			foreach (ref var seed in _seeds)
			{
				seed.GrowSpeed = 1f * (1f - seed.GrowSpeedVariation);
			}
			foreach (ref var mature in _matures)
			{
				mature.FruitingSpeed = 1f * (1f - mature.FruitingSpeedVariation);
			}
		}
	}
}
