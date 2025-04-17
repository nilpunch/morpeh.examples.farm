using System;
using System.IO;
using UnityEngine;

namespace Farm
{
	public static class SaveUtils
	{
		public static string GetPathToSaveFile(string saveFileName)
		{
			return Path.Combine(Application.persistentDataPath, saveFileName);
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
