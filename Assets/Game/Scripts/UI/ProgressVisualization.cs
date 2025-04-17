using System.Collections.Generic;
using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.UI;

namespace Farm.UI
{
	public class ProgressVisualization : MonoBehaviour
	{
		[SerializeField] private Image _progressViewPrefab;
		[SerializeField] private Vector3 _positionOffset;

		private List<Image> _createdViews = new List<Image>();
		private int _usedCount;

		private Camera _camera;
		private Filter _filter;
		private Stash<Progress> _progresses;
		private Stash<ViewHolder> _viewHolders;

		private void Awake()
		{
			_camera = Camera.main;
			_filter = World.Default.Filter.With<Progress>().With<ViewHolder>().Build();
			_progresses = World.Default.GetStash<Progress>();
			_viewHolders = World.Default.GetStash<ViewHolder>();
		}

		private void LateUpdate()
		{
			int viewNeeded = _filter.GetLengthSlow();

			// Create lacking amount.
			for (int i = _createdViews.Count; i < viewNeeded; i++)
			{
				_createdViews.Add(Instantiate(_progressViewPrefab, transform));
			}

			// Disable excess.
			for (int i = viewNeeded; i < _createdViews.Count; i++)
			{
				_createdViews[i].enabled = false;
			}

			int count = 0;
			foreach (var entity in _filter)
			{
				var progressView = _createdViews[count++];
				var progress = _progresses.Get(entity);
				var worldTransform = _viewHolders.Get(entity).View.transform;

				progressView.enabled = true;
				progressView.rectTransform.anchorMin = Vector2.one * 0.5f;
				progressView.rectTransform.anchorMax = Vector2.one * 0.5f;
				progressView.rectTransform.pivot = Vector2.one * 0.5f;
				progressView.rectTransform.position = _camera.WorldToScreenPoint(worldTransform.position + _positionOffset);

				progressView.fillAmount = 1f - progress.ElapsedTime / progress.TargetTime;
			}
		}
	}
}
