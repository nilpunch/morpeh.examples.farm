using System;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Feature.Events;
using Scellecs.Morpeh.Systems;
using Scellecs.Morpeh.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Farm
{
	[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(FirstLaunch))]
	public class FirstLaunch : Initializer
	{
		[SerializeField] private SceneReference _authoringScene;

		public const string PrefKey = nameof(FirstLaunch);

		public static bool IsFirstLaunch => PlayerPrefs.HasKey(PrefKey);

		public override void OnAwake()
		{
			// if (IsFirstLaunch)
			{
				// Populate World with entities from the scene.
				SceneManager.LoadScene(_authoringScene.ScenePath, LoadSceneMode.Additive);
			}
		}
	}
}
