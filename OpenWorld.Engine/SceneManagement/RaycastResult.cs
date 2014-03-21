using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Defines a raycast result.
	/// </summary>
	public sealed class RaycastResult
	{
		internal RaycastResult()
		{

		}

		/// <summary>
		/// Gets the position of the hit.
		/// </summary>
		public Vector3 Position { get; internal set; }

		/// <summary>
		/// Gets the normal of the hit.
		/// </summary>
		public Vector3 Normal { get; internal set; }

		/// <summary>
		/// Gets the rigid body the ray hit.
		/// </summary>
		public RigidBody RigidBody { get; set; }
		
		/// <summary>
		/// Gets the scene node the ray hit.
		/// </summary>
		public SceneNode Node { get; set; }

	}
}
