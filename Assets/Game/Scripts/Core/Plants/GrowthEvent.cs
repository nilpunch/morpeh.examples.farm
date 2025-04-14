using Scellecs.Morpeh;

namespace Farm
{
	public struct GrowthEvent : IComponent
	{
		public Entity Entity;

		public GrowthStage GrowthStage;
	}
}
