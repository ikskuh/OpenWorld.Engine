using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Defines a point light.
	/// </summary>
	public sealed class PointLight  : SceneNode.Component
	{
		/// <summary>
		/// Creates a new point light.
		/// </summary>
		public PointLight()
		{
			this.Color = Color.White;
			this.Radius = 10.0f;
		}

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
			e.Renderer.PointLight(this.Node.Transform.GetGlobalMatrix().Row3.Xyz, this.Radius, this.Color);
		}

		/// <summary>
		/// Gets or sets the radius of the light.
		/// </summary>
		public float Radius { get; set; }

		/// <summary>
		/// Gets or sets the color of the light.
		/// </summary>
		public Color Color { get; set; }
	}
}
