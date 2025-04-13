using System;
using Scellecs.Morpeh;

namespace Farm
{
	public static class StashExtensions
	{
		public static ref T Single<T>(this Stash<T> stash) where T : struct, IComponent
		{
			var enumerator = stash.GetEnumerator();
			var exist = enumerator.MoveNext();
			if (exist)
			{
				ref var component = ref enumerator.Current;
				var otherMove = enumerator.MoveNext();
				if (!otherMove)
				{
					return ref component;
				}
			}
			else
			{
				throw new InvalidOperationException("The stash is empty.");
			}

			throw new InvalidOperationException("The stash contains more than one element.");
		}
	}
}
