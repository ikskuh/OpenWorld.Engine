using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Defines a component that can be bound to a scene node.
	/// </summary>
	public abstract class Component : SceneNode.Component
	{
		/// <summary>
		/// Creates a new component.
		/// </summary>
		protected Component()
			: base()
		{

		}
	}
}
