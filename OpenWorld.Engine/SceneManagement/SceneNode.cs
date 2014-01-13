using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Defines a part of a scene.
	/// </summary>
	public sealed class SceneNode : DiGraph<SceneNode>
	{
		// TODO: Implement component system.

		/// <summary>
		/// Updates the node.
		/// </summary>
		/// <param name="time">Time snapshot</param>
		public void Update(GameTime time)
		{
			// TODO: Implement component updating.

			// Pass the call method to all children.
			foreach (var child in this.Children)
				child.Update(time);
		}

		/// <summary>
		/// Draws the node.
		/// </summary>
		/// <param name="time">Time snapshot</param>
		/// <param name="renderer">The renderer that draws the node.</param>
		public void Draw(GameTime time, SceneRenderer renderer)
		{
			// TODO: Implement component drawing.

			// Pass the draw call to all children.
			foreach (var child in this.Children)
				child.Draw(time, renderer);
		}
	}
}
