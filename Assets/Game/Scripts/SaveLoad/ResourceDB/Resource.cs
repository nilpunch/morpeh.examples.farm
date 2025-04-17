using System;

namespace Farm
{
	[Serializable]
	public struct Resource<T> where T : UnityEngine.Object
	{
		public int ResourceId;

		[NonSerialized] private T _cache;

		public Resource(int resourceId)
		{
			ResourceId = resourceId;
			_cache = null;
		}

		public T Value => _cache ??= ResourceDB.GetResource<T>(ResourceId);
	}
}
