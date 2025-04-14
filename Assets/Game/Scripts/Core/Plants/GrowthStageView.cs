using Scellecs.Morpeh;
using UnityEngine;

namespace Farm
{
	public class GrowthStageView : ViewBehaviour
	{
		[SerializeField] private GameObject _seed;
		[SerializeField] private GameObject _fullyGrown;
		[SerializeField] private GameObject _fruit;

		public override void OnEntityAssigned(Entity entity)
		{
			if (entity.Has<SeedStage>())
			{
				ShowSeed();
			}
			else if (entity.Has<MatureStage>())
			{
				ShowMature();
			}
			else
			{
				ShowFruits();
			}
		}

		public void ShowSeed()
		{
			_seed.SetActive(true);
			_fullyGrown.SetActive(false);
			_fruit.SetActive(false);
		}

		public void ShowMature()
		{
			_seed.SetActive(false);
			_fullyGrown.SetActive(true);
			_fruit.SetActive(false);
		}

		public void ShowFruits()
		{
			_seed.SetActive(false);
			_fullyGrown.SetActive(true);
			_fruit.SetActive(true);
		}
	}
}
