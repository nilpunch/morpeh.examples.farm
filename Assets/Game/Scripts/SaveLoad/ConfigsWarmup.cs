using Scellecs.Morpeh.Systems;
using Scellecs.Morpeh.Utils;
using UnityEngine;

namespace Farm
{
	[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ConfigsWarmup))]
	public class ConfigsWarmup : Initializer
	{
		[SerializeField] private SceneReference _authoringScene;

		public override void OnAwake()
		{
			ConfigDB.LoadConfigs<PlantConfig>();
		}
	}
}
