using OpenTK;
using OpenTK.Graphics.OpenGL;
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
		/// Draws the scene.
		/// </summary>
		/// <param name="scene">The scene to be drawn.</param>
		/// <param name="time">The time snapshot.</param>
		public void Draw(SceneManagement.Scene scene, GameTime time)
		{
			scene.Draw(this, time);
		}

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
				area = new Box2(0, 0, Window.Current.ViewportWidth, Window.Current.ViewportHeight);
			return area;
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
		/// Gets or sets the up direction all cameras share.
		/// </summary>
		public static Vector3 Up
		{
			get { return Camera.up; }
			set { Camera.up = value; }
		}

		/// <summary>
		/// Gets or sets the screen area the camera renders to.
		/// </summary>
		public Box2 Area { get; set; }
	}
}
