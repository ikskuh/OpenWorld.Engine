using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Defines a sky that clears to a single color.
	/// </summary>
	public sealed class ColorSky : Sky
	{
		/// <summary>
		/// Creates a new colored sky.
		/// </summary>
		public ColorSky()
		{
			this.Color = Color.SkyBlue;
		}

		/// <summary>
		/// Draws the sky.
		/// </summary>
		/// <param name="renderer"></param>
		/// <param name="camera"></param>
		public override void Draw(SceneRenderer renderer, Camera camera)
		{
			GL.ClearColor(
				this.Color.R,
				this.Color.G,
				this.Color.B,
				this.Color.A);
			GL.Clear(ClearBufferMask.ColorBufferBit);
		}

		/// <summary>
		/// Gets or sets the color of the sky.
		/// </summary>
		public Color Color { get; set; }
	}
}
