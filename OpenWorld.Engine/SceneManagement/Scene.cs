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
			this.Renderer = new SimpleRenderer();
		}

		/// <summary>
		/// Updates the whole scene.
		/// </summary>
		/// <param name="time">Time snapshot</param>
		public void Update(GameTime time)
		{
			// Just update the root node.
			this.root.DoUpdate(time);
		}

		/// <summary>
		/// Draws the whole scene.
		/// </summary>
		/// <param name="camera">The camera setting for the scene.</param>
		/// <param name="time">Time snapshot</param>
		public void Draw(Camera camera, GameTime time)
		{
			if (Renderer == null)
				return; // Draw nothing without a renderer.

			this.Renderer.Begin();
			this.root.DoDraw(time, this.Renderer);
			this.Renderer.End(this, camera);
		}

		/// <summary>
		/// Gets or sets the renderer of the scene.
		/// </summary>
		public SceneRenderer Renderer { get; set; }

		/// <summary>
		/// Gets the root of the scene.
		/// </summary>
		public SceneNode Root
		{
			get { return root; }
		}
	}
}
