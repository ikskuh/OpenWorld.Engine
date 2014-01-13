using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines a camera
	/// </summary>
	public abstract class Camera
	{
		static Vector3 up = Vector3.UnitY;

		/// <summary>
		/// Gets the cameras view matrix.
		/// </summary>
		public abstract Matrix4 ViewMatrix { get; }

		/// <summary>
		/// Gets the cameras projection matrix.
		/// </summary>
		public abstract Matrix4 ProjectionMatrix { get; }

		/// <summary>
		/// Gets or sets the up direction all cameras share.
		/// </summary>
		public static Vector3 Up
		{
			get { return Camera.up; }
			set { Camera.up = value; }
		}
	}
}
