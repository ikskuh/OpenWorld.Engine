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
		/// Renders the scene node.
		/// </summary>
		/// <param name="time">Time snapshot</param>
		/// <param name="renderer">The renderer that is used for drawing.</param>
		protected override void OnRender(GameTime time, SceneRenderer renderer)
		{
			renderer.PointLight(this.Node.Transform.GetGlobalMatrix().Row3.Xyz, this.Radius, this.Color);
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
