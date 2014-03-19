using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines a material.
	/// </summary>
	public sealed class Material
	{
		/// <summary>
		/// Creates a new material.
		/// </summary>
		public Material()
		{
			this.SpecularPower = 16;
			this.EmissiveScale = 10.0f;
			this.IsTranslucent = false;
			this.Surface = Color.White;
			this.Emissive = Color.Black;
		}

		/// <summary>
		/// Gets or sets the material shader.
		/// </summary>
		public ObjectShader Shader { get; set; }

		/// <summary>
		/// Gets or sets a value that indicates wheather this material is translucent or solid.
		/// </summary>
		public bool IsTranslucent { get; set; }

		/// <summary>
		/// Gets or sets the specular power.
		/// </summary>
		public float SpecularPower { get; set; }

		/// <summary>
		/// Gets or sets the emissive scale.
		/// </summary>
		/// <remarks>Scales the emissive color for bloom effects.</remarks>
		public float EmissiveScale { get; set; }

		/// <summary>
		/// Gets or sets the surface color tinting.
		/// </summary>
		public Color Surface { get; set; }

		/// <summary>
		/// Gets or sets the emissive color tinting.
		/// </summary>
		public Color Emissive { get; set; }
	}
}
