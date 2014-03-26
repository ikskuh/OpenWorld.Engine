using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines a material.
	/// </summary>
	public sealed class Material : BaseMaterial
	{
		/// <summary>
		/// Creates a new material.
		/// </summary>
		public Material()
		{
			this.EmissiveScale = 10.0f;
			this.SpecularPower = 16.0f;
			this.IsTranslucent = false;
			this.Diffuse = Color.White;
			this.Specular = Color.Black;
			this.Emissive = Color.White;
		}

		/// <summary>
		/// Gets or sets the emissive scale.
		/// </summary>
		/// <remarks>Scales the emissive color for bloom effects.</remarks>
		[Uniform("EmissiveScale")]
		public float EmissiveScale { get; set; }

		/// <summary>
		/// Gets or sets the diffuse color tinting.
		/// </summary>
		[Uniform("Diffuse")]
		public Color Diffuse { get; set; }

		/// <summary>
		/// Gets or sets the specular color tinting.
		/// </summary>
		[Uniform("Specular")]
		public Color Specular { get; set; }

		/// <summary>
		/// Gets or sets the emissive color tinting.
		/// </summary>
		[Uniform("Emissive")]
		public Color Emissive { get; set; }

		/// <summary>
		/// Gets or sets the specular power.
		/// </summary>
		[Uniform("SpecularPower")]
		public float SpecularPower { get; set; }
	}
}
