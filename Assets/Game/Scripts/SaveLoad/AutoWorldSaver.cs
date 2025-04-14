using Scellecs.Morpeh;
using TriInspector;
using UnityEngine;
using UnityEngine.Windows;

namespace Farm
{
	public class AutoWorldSaver : MonoBehaviour
	{
		[RuntimeInitializeOnLoadMethod]
		static void RegisterWantsToQuit()
		{
			Application.wantsToQuit += OnWantsToQuit;
		}

		static bool OnWantsToQuit()
		{
			FarmSaver.Save(World.Default);
			return true;
		}

		[Button("Delete Save")]
		private void DeleteSave()
		{
			var saveFilePath = SaveUtils.GetPathToSaveFile(FarmSaver.DefaultSaveFile);
			if (File.Exists(saveFilePath))
			{
				File.Delete(saveFilePath);
			}
		}
	}
}
