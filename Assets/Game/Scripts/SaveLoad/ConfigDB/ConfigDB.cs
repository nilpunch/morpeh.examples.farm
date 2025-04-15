using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Farm
{
	public static class ConfigDB
	{
		private const string Directory = "Configs";

		private static readonly Dictionary<Type, Dictionary<string, IConfig>> _typeMap = new();

		public static void LoadConfigs<T>() where T : ScriptableObject, IConfig
		{
			var dictionary = Resources.LoadAll<T>(Directory).ToDictionary(c => c.Id, c => (IConfig)c);
			_typeMap[typeof(T)] = dictionary;
		}

		public static T Get<T>(string id) where T : class, IConfig
		{
			if (_typeMap.TryGetValue(typeof(T), out var map) && map.TryGetValue(id, out var config))
				return config as T;

			Debug.LogError($"Missing config: {typeof(T).Name} with ID '{id}'");
			return null;
		}
	}
}
