using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Draws a scene.
	/// </summary>
	public abstract class SceneRenderer
	{
		/// <summary>
		/// Instantiates a new renderer.
		/// </summary>
		protected SceneRenderer()
		{

		}

		/// <summary>
		/// Starts the drawing process.
		/// </summary>
		public void Begin()
		{
			if (this.IsDrawing)
				throw new InvalidOperationException("You need to call End() before drawing again..");


			this.IsDrawing = true;
		}

		/// <summary>
		/// Ends the drawing process and renders everything.
		/// </summary>
		public void End()
		{
			if (!this.IsDrawing)
				throw new InvalidOperationException("You need to call Begin() first.");


			this.IsDrawing = false;
		}

		/// <summary>
		/// Actually renders the scene.
		/// </summary>
		protected abstract void Render();

		/// <summary>
		/// Gets a value that indicates if the renderer is currently drawing.
		/// </summary>
		public bool IsDrawing { get; private set; }
	}
}
