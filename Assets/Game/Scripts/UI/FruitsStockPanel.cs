using Scellecs.Morpeh;
using TMPro;
using UnityEngine;

namespace Farm.UI
{
	public class FruitsStockPanel : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _text;

		private Camera _camera;
		private Stash<FruitStock> _fruitStock;
		private int _lastAmount = -1;

		private void Awake()
		{
			_fruitStock = World.Default.GetStash<FruitStock>();
		}

		private void LateUpdate()
		{
			var currentAmount = _fruitStock.Single().Amount;

			if (_lastAmount != currentAmount)
			{
				_lastAmount = currentAmount;
				_text.text = currentAmount.ToString();
			}
		}
	}
}
