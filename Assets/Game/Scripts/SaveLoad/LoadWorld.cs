using System;
using System.IO;
using Scellecs.Morpeh.Addons.Feature.Events;
using Scellecs.Morpeh.Systems;
using Scellecs.Morpeh.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Farm
{
	[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(LoadWorld))]
	public class LoadWorld : Initializer
	{
		[SerializeField] private SceneReference _authoringScene;

		public override void OnAwake()
		{
			if (!File.Exists(SaveUtils.GetPathToSaveFile(FarmSaver.DefaultSaveFile)))
			{
				// Populate World with entities from the scene.
				SceneManager.LoadScene(_authoringScene.ScenePath, LoadSceneMode.Additive);
			}
			else
			{
				// Load from default save file.
				FarmSaver.Load(World);
			}
		}
	}
}
