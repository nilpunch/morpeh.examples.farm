using System;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace Farm
{
	[AddComponentMenu("Farm/ViewHolder")]
	public sealed class ViewHolderProvider : MonoProvider<ViewHolder>
	{
	}

	[Serializable]
	public struct ViewHolder : IComponent
	{
		public EntityView View;
	}
}
