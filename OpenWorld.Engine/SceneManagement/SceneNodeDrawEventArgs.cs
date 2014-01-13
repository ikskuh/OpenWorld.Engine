using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Provides data for the Draw event.
	/// </summary>
	public class SceneNodeDrawEventArgs : EventArgs
	{
		private GameTime time;
		private SceneRenderer renderer;

		internal SceneNodeDrawEventArgs(GameTime time, SceneRenderer renderer)
		{
			this.time = time;
			this.renderer = renderer;
		}

		/// <summary>
		/// Gets the time snapshot.
		/// </summary>
		public GameTime Time
		{
			get { return time; }
		}

		/// <summary>
		/// Gets the renderer.
		/// </summary>
		public SceneRenderer Renderer
		{
			get { return renderer; }
		}
	}
}
