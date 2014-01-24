using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines a perspective projection for a camera.
	/// </summary>
	public sealed class Perspective : IProjectionMatrixSource
	{
		float fov = 60.0f;
		float aspect = 1.0f;
		float zNear = 0.1f;
		float zFar = 10000.0f;

		/// <summary>
		/// Creates a new perspective.
		/// </summary>
		public Perspective()
		{

		}

		/// <summary>
		/// Creates a new perspective.
		/// </summary>
		/// <param name="fov">Field of view in degrees.</param>
		public Perspective(float fov)
		{
			this.fov = fov;
		}

		public OpenTK.Matrix4 GetProjectionMatrix(Camera camera)
		{
			var viewport = camera.GetViewport();
			return Matrix4.CreatePerspectiveFieldOfView(
				GameMath.ToRadians(this.fov),
				this.aspect * viewport.Width / viewport.Height,
				this.zNear,
				this.zFar);
		}

		/// <summary>
		/// Gets or sets the aspect of the projection.
		/// </summary>
		public float Aspect
		{
			get { return aspect; }
			set { aspect = value; }
		}

		/// <summary>
		/// Gets or sets the field of view.
		/// </summary>
		public float FieldOfView
		{
			get { return fov; }
			set { fov = value; }
		}

		/// <summary>
		/// Gets or sets the near clipping plane.
		/// </summary>
		public float ZNear
		{
			get { return zNear; }
			set { zNear = value; }
		}

		/// <summary>
		/// Gets or sets the far clipping plane.
		/// </summary>
		public float ZFar
		{
			get { return zFar; }
			set { zFar = value; }
		}
	}
}
