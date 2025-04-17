using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Farm
{
	[CreateAssetMenu(menuName = "ECS/" + nameof(ResourceDB), fileName = nameof(ResourceDB))]
	public class ResourceDB : ScriptableObject
	{
		[SerializeField] private Object[] _resources;

		private readonly Dictionary<Object, int> _idByResources = new();

		private static ResourceDB s_instance;
		public static ResourceDB Instance => s_instance ?? LoadDB();

		public static int GetResourceId(Object resource)
		{
			return Instance._idByResources[resource];
		}

		public static T GetResource<T>(int id) where T : Object
		{
			return Instance._resources[id] as T;
		}

		private static ResourceDB LoadDB()
		{
			var db = Resources.Load<ResourceDB>(nameof(ResourceDB));
			var resourceId = 0;
			foreach (var resource in db._resources)
			{
				db._idByResources.Add(resource, resourceId);
				resourceId++;
			}
			s_instance = db;
			return db;
		}
	}
}
