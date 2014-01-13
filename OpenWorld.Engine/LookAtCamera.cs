using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines a camera that looks at things.
	/// </summary>
	public abstract class LookAtCamera : Camera
	{
		Matrix4 viewMatrix = Matrix4.Identity;

		/// <summary>
		/// Lets the camera look at a specific point.
		/// </summary>
		/// <param name="eye">The position of the camera.</param>
		/// <param name="target">The target to look at.</param>
		public void LookAt(Vector3 eye, Vector3 target)
		{
			this.viewMatrix = Matrix4.LookAt(
				eye,
				target,
				Camera.Up);
		}

		/// <summary>
		/// Gets the cameras view matrix.
		/// </summary>
		public override Matrix4 ViewMatrix
		{
			get { return this.viewMatrix; }
		}

		/// <summary>
		/// Gets or sets the position of the camera.
		/// </summary>
		public Vector3 Position
		{
			get { return -this.viewMatrix.Row3.Xyz; }
			set { this.viewMatrix.Row3.Xyz = -value; }
		}
	}
}
