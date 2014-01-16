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
		/// Starts the component.
		/// </summary>
		protected override void OnStart()
		{
			this.Node.Draw += Node_Draw;
		}

		/// <summary>
		/// Stops the component.
		/// </summary>
		protected override void OnStop()
		{
			this.Node.Draw -= Node_Draw;
		}

		void Node_Draw(object sender, SceneNodeDrawEventArgs e)
		{
			if (!this.Enabled)
				return;
			if (this.Model == null)
				return;
			e.Renderer.Draw(this.Model, this.Node.Transform.GetGlobalMatrix(), this.Material);
		}

		/// <summary>
		/// Gets or sets the model the renderer should draw.
		/// </summary>
		public Model Model { get; set; }

		/// <summary>
		/// Gets or sets the material the renderer should draw with.
		/// </summary>
		public Material Material { get; set; }
	}
}
