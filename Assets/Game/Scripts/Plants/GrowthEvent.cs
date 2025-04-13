using Scellecs.Morpeh;

namespace Farm
{
	public enum GrowthStage
	{
		Seed,
		Mature,
		Fruiting,
	}

	public struct GrowthEvent : IComponent
	{
		public Entity Entity;

		public GrowthStage GrowthStage;
	}
}
