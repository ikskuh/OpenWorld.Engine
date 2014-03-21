using OpenTK;
using OpenTK.Graphics.OpenGL4;
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
		/// Sets the viewport for the camera.
		/// </summary>
		public void Setup()
		{
			var area = GetViewport();
			GL.Viewport(
				(int)area.Left,
				(int)area.Top,
				(int)area.Width,
				(int)area.Height);
			this.OnSetup();
		}

		/// <summary>
		/// Sets up the camera.
		/// </summary>
		protected virtual void OnSetup()
		{

		}

		/// <summary>
		/// Gets the camera viewport.
		/// </summary>
		/// <returns></returns>
		public Box2 GetViewport()
		{
			var area = this.Area;
			// If we have no pixel to draw to
			if (area.Width < 1.0f || area.Height < 1.0f)
				area = new Box2(0, 0, Game.Current.Size.Width, Game.Current.Size.Height);
			return area;
		}

		/// <summary>
		/// Constructs a ray from a screen position.
		/// </summary>
		/// <param name="x">X coordinate</param>
		/// <param name="y">Y coordinate</param>
		/// <returns>Constructed ray.</returns>
		public Ray CreateRay(int x, int y)
		{
			var viewport = this.GetViewport();

			float fx = 2.0f * (x - viewport.Left) / viewport.Width - 1.0f;
			float fy = 1.0f - 2.0f * (y - viewport.Top) / viewport.Height;

			Vector3 from = this.Unproject(new Vector3(fx, fy, 0.0f));
			Vector3 to = this.Unproject(new Vector3(fx, fy, 1.0f));

			return new Ray(from, to - from);
		}

		/// <summary>
		/// Unprojects a vector in screen space coordinates
		/// </summary>
		/// <param name="worldCoord">Vector in screen space coordinates</param>
		/// <returns>Vector in world coordinates</returns>
		/// <remarks>X: [-1;1], Y: [-1;1], Z: [0;1]</remarks>
		public Vector3 Unproject(Vector3 worldCoord)
		{
			Matrix4 matrix = this.ViewMatrix;
			matrix = Matrix4.Mult (matrix, this.ProjectionMatrix);
			matrix = Matrix4.Invert (matrix);
			Vector3 vector = Vector3.Transform (worldCoord, matrix);
			float num = worldCoord.X * matrix.M14 + worldCoord.Y * matrix.M24 + worldCoord.Z * matrix.M34 + matrix.M44;
			vector /= num;
			return vector;
		}

		/// <summary>
		/// Gets the cameras view matrix.
		/// </summary>
		public abstract Matrix4 ViewMatrix { get; }

		/// <summary>
		/// Gets the cameras projection matrix.
		/// </summary>
		public abstract Matrix4 ProjectionMatrix { get; }

		/// <summary>
		/// Gets or sets the screen area the camera renders to.
		/// </summary>
		public Box2 Area { get; set; }

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
