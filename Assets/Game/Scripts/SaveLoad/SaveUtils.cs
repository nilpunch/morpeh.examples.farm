using System;
using System.IO;
using UnityEngine;

namespace Farm
{
	public static class SaveUtils
	{
		public static string GetPathToSaveFile(string saveFileName)
		{
			var rootDirectory = Application.persistentDataPath;
			var pathToFile = Path.Combine(rootDirectory, saveFileName);
			return pathToFile;
		}

		public static void WriteToFile(string fileName, Action<Stream> write)
		{
			Directory.CreateDirectory(Path.GetDirectoryName(fileName)!);

			using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
			{
				write.Invoke(stream);
			}
		}

		public static void ReadFromFile(string fileName, Action<Stream> read)
		{
			using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
			{
				read.Invoke(stream);
			}
		}
	}
}
