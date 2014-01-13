using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Defines a 3D scene.
	/// </summary>
	public sealed class Scene
	{
		readonly SceneNode root = null;

		/// <summary>
		/// Instantiates a new scene.
		/// </summary>
		public Scene()
		{
			this.root = new SceneNode();
		}

		/// <summary>
		/// Updates the whole scene.
		/// </summary>
		/// <param name="time">Time snapshot</param>
		public void Update(GameTime time)
		{
			// Just update the root node.
			this.root.Update(time);
		}

		/// <summary>
		/// Draws the whole scene.
		/// </summary>
		/// <param name="time">Time snapshot</param>
		public void Draw(GameTime time)
		{
			if (Renderer == null)
				return; // Draw nothing without a renderer.

			this.Renderer.Begin();
			this.root.Draw(time, this.Renderer);
			this.Renderer.End();
		}

		/// <summary>
		/// Gets or sets the renderer of the scene.
		/// </summary>
		public SceneRenderer Renderer { get; set; }
	}
}
