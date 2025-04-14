using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;

namespace Farm
{
	[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ProgressSystem))]
	public class ProgressSystem : UpdateSystem
	{
		private Stash<Progress> _progresses;

		public override void OnAwake()
		{
			_progresses = World.GetStash<Progress>();
		}

		public override void OnUpdate(float deltaTime)
		{
			foreach (ref var progress in _progresses)
			{
				progress.ElapsedTime += deltaTime * progress.SpeedMultiplier;
			}
		}
	}
}
