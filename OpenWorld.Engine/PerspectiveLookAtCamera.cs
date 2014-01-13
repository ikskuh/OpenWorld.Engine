using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines a perspective look-at camera.
	/// </summary>
	public sealed class PerspectiveLookAtCamera : LookAtCamera
	{
		float fov = 60.0f;
		float aspect = 1.0f;
		float zNear = 0.1f;
		float zFar = 10000.0f;
		Matrix4 projectionMatrix;

		/// <summary>
		/// Creates a new perspective look-at camera.
		/// </summary>
		public PerspectiveLookAtCamera()
		{

		}

		/// <summary>
		/// Sets up the camera.
		/// </summary>
		protected override void OnSetup()
		{
			var viewport = this.GetViewport();
			this.projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(
				GameMath.ToRadians(this.FieldOfView),
				this.aspect * viewport.Width / viewport.Height,
				this.zNear,
				this.zFar);
			base.OnSetup();
		}

		/// <summary>
		/// Gets the cameras projection matrix.
		/// </summary>
		public override OpenTK.Matrix4 ProjectionMatrix
		{
			get { return this.projectionMatrix; }
		}

		/// <summary>
		/// Gets or sets the z near plane distance.
		/// </summary>
		public float ZNear
		{
			get { return zNear; }
			set
			{
				if (zNear <= 0.0f)
					throw new InvalidOperationException("ZNear must be larger than 0.0");
				zNear = value;
			}
		}

		/// <summary>
		/// Gets or sets the z far plane distance.
		/// </summary>
		public float ZFar
		{
			get { return zFar; }
			set
			{
				if (zFar <= 0.0f)
					throw new InvalidOperationException("ZFar must be larger than 0.0");
				zFar = value;
			}
		}

		/// <summary>
		/// Gets or sets the view angle.
		/// </summary>
		public float FieldOfView
		{
			get { return fov; }
			set
			{
				if (fov <= 0.0f)
					throw new InvalidOperationException("FieldOfView must be larger than 0.0");
				if (fov >= 180.0f)
					throw new InvalidOperationException("FieldOfView must be lesser than 180.0");
				fov = value;
			}
		}

		/// <summary>
		/// Gets or sets the aspect that will be applied to the camera.
		/// </summary>
		public float Aspect
		{
			get { return aspect; }
			set
			{
				if (fov == 0.0f)
					throw new InvalidOperationException("FieldOfView must not be 0.0");
				aspect = value;
			}
		}
	}
}
