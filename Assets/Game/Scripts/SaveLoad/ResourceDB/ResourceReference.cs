using System;

namespace Farm
{
	[Serializable]
	public struct ResourceReference<T> where T : UnityEngine.Object
	{
		public string ResourcePath;

		[NonSerialized] private T _cache;

		public T Value => _cache ??= ResourceDB.Load<T>(ResourcePath);

		public static implicit operator ResourceReference<T>(T resource)
		{
			return new ResourceReference<T>()
			{
				ResourcePath = ResourceDB.GetResourcePath(resource)
			};
		}
	}
}
