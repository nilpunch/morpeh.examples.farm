using Scellecs.Morpeh;

namespace Farm
{
	public struct GrowthRequest : IComponent
	{
		public Entity Entity;

		public GrowthStage GrowthStage;
	}
}
