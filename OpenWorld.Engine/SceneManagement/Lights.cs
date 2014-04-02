using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{

	/// <summary>
	/// Defines a point light.
	/// </summary>
	[UniformPrefix("light")]
	public abstract class Light : Component
	{
		/// <summary>
		/// Creates a new point light.
		/// </summary>
		internal Light(LightType type)
		{
			this.Color = Color.White;
			this.Intensity = 1.0f;
			this.Type = type;
		}

		/// <summary>
		/// Renders the scene node.
		/// </summary>
		/// <param name="time">Time snapshot</param>
		/// <param name="renderer">The renderer that is used for drawing.</param>
		protected override void OnRender(GameTime time, SceneRenderer renderer)
		{
			renderer.Light(this.Node.Transform.WorldPosition, this);
		}

		/// <summary>
		/// Gets or sets the color of the light.
		/// </summary>
		[Uniform("Color")]
		public Color Color { get; set; }

		/// <summary>
		/// Gets or sets the intensity of the light.
		/// </summary>
		[Uniform("Intensity")]
		public float Intensity { get; set; }

		/// <summary>
		/// Gets the type of this light.
		/// </summary>
		public LightType Type { get; private set; }
	}

	/// <summary>
	/// Defines a point light.
	/// </summary>
	public sealed class PointLight : Light
	{
		/// <summary>
		/// Creates a new point light.
		/// </summary>
		public PointLight()
			: base(LightType.Point)
		{

		}
	}

	/// <summary>
	/// Defines a point light.
	/// </summary>
	public sealed class DirectionalLight : Light
	{
		/// <summary>
		/// Creates a new point light.
		/// </summary>
		public DirectionalLight()
			: base(LightType.Directional)
		{
			this.Direction = new Vector3(0.4f, -0.7f, 0.2f).Normalized();
		}

		/// <summary>
		/// Gets or sets the direction of the light.
		/// </summary>
		[Uniform("Direction")]
		public Vector3 Direction { get; set; }
	}
}
