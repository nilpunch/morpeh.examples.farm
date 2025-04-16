using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Farm
{
	public static class ResourceDB
	{
		private static readonly Dictionary<string, object> _loaded = new();

		public static T Load<T>(string path) where T : Object
		{
			if (!_loaded.TryGetValue(path, out var resource))
			{
				resource = Resources.Load<T>(path);
				_loaded.Add(path, resource);
			}

			return (T)resource;
		}

		/// <summary>
		/// Editor only. Returns path relative to Resources folder, without extension (as Resources.Load() expects).
		/// </summary>
		public static string GetResourcePath(Object obj)
		{
#if UNITY_EDITOR
			var assetPath = UnityEditor.AssetDatabase.GetAssetPath(obj);
			var resourcesIndex = assetPath.IndexOf("/Resources/", StringComparison.Ordinal);

			if (resourcesIndex >= 0)
			{
				var startIndex = resourcesIndex + "/Resources/".Length;
				var endIndex = assetPath.LastIndexOf('.');
				if (endIndex < 0)
				{
					endIndex = assetPath.Length;
				}
				return assetPath.Substring(startIndex, endIndex - startIndex);
			}
#endif

			return null;
		}
	}
}
