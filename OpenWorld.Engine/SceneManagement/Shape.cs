using BulletSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Defines a physical shape.
	/// </summary>
	public abstract class Shape : Component
	{
		/// <summary>
		/// Gets the Jitter shape fitting this shape component.
		/// </summary>
		/// <returns>A Jitter shape.</returns>
		internal protected abstract CollisionShape GetShape();
	}
}
