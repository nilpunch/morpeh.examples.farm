using System;
using Scellecs.Morpeh;
using TMPro;
using UnityEngine;

namespace Farm.UI
{
	public class PlantInfoPanel : MonoBehaviour
	{
		[SerializeField] private RectTransform _panel;
		[SerializeField] private TextMeshProUGUI _text;

		private Camera _camera;
		private Stash<HoverEvent> _hoverEvents;
		private Stash<Plant> _plants;
		private Stash<SeedStage> _seeds;
		private Stash<MatureStage> _matures;

		private void Awake()
		{
			_camera = Camera.main;
			var world = World.Default;
			_hoverEvents = world.GetStash<HoverEvent>();
			_plants = world.GetStash<Plant>();
			_seeds = world.GetStash<SeedStage>();
			_matures = world.GetStash<MatureStage>();
		}

		private void LateUpdate()
		{
			if (_hoverEvents.IsEmpty())
			{
				_text.enabled = false;
				return;
			}

			var hoverEvent = _hoverEvents.Single();

			_text.enabled = true;

			var screenPoint = _camera.WorldToScreenPoint(hoverEvent.HitPosition);
			_panel.position = screenPoint;

			if (screenPoint.x > Screen.width * 2f / 3f)
			{
				_text.rectTransform.anchorMin = new Vector2(1, 0);
				_text.rectTransform.anchorMax = new Vector2(1, 0);
				_text.rectTransform.pivot = new Vector2(1, 0);
				_text.alignment = TextAlignmentOptions.BottomRight;
			}
			else
			{
				_text.rectTransform.anchorMin = new Vector2(0, 0);
				_text.rectTransform.anchorMax = new Vector2(0, 0);
				_text.rectTransform.pivot = new Vector2(0, 0);
				_text.alignment = TextAlignmentOptions.BottomLeft;
			}

			string plantStage;
			if (_seeds.Has(hoverEvent.Entity))
			{
				plantStage = "Seed";
			}
			else if (_matures.Has(hoverEvent.Entity))
			{
				plantStage = "Mature";
			}
			else
			{
				plantStage = "Fruiting";
			}

			var plantName = _plants.Get(hoverEvent.Entity).Config.Value.Name;
			_text.text = $"{plantName} ({plantStage})";
		}
	}
}
