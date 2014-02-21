using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Defines a sky.
	/// </summary>
	public abstract class Sky
	{
		/// <summary>
		/// Draws the sky.
		/// </summary>
		/// <param name="renderer">The renderer that is drawing the sky.</param>
		/// <param name="camera">The camera that shows the sky.</param>
		public abstract void Draw(SceneRenderer renderer, Camera camera);
	}
}
