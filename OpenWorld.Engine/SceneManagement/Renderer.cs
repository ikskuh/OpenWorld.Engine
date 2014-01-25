using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Defines a component that draws a scene node.
	/// </summary>
	public sealed class Renderer : SceneNode.Component
	{
		/// <summary>
		/// Renders the scene node.
		/// </summary>
		/// <param name="time">Time snapshot</param>
		/// <param name="renderer">The renderer that is used for drawing.</param>
		protected override void OnRender(GameTime time, SceneRenderer renderer)
		{
			if (this.Model == null)
				return;
			renderer.Draw(this.Model, this.Node.Transform.GetGlobalMatrix(), this.Node.Material);
		}

		/// <summary>
		/// Gets or sets the model the renderer should draw.
		/// </summary>
		public Model Model { get; set; }
	}
}
