using Scellecs.Morpeh;

namespace Farm
{
	public static class FarmSaver
	{
		public const string DefaultSaveFile = "DefaultWorldSave.data";

		public static WorldSerializer Serializer { get; } = new WorldSerializer()
			// Meta
			.RegisterComponent<FruitStock>()

			// Plants
			.RegisterComponent<Plant>()
			.RegisterComponent<SeedStage>()
			.RegisterComponent<MatureStage>()
			.RegisterComponent<FruitingStage>()
			.RegisterComponent<Progress>();

		public static void Save(World world, string saveFileName = DefaultSaveFile)
		{
			var pathToSaveFile = SaveUtils.GetPathToSaveFile(saveFileName);
			SaveUtils.WriteToFile(pathToSaveFile, stream =>
			{
				Serializer.Serialize(stream, world);
			});
		}

		public static void Load(World world, string saveFileName = DefaultSaveFile)
		{
			var pathToSaveFile = SaveUtils.GetPathToSaveFile(saveFileName);
			SaveUtils.ReadFromFile(pathToSaveFile, stream =>
			{
				Serializer.Deserialize(stream, world);
			});
		}
	}
}
