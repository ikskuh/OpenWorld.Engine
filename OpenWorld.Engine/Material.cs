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
			this.IsTranslucent = false;
			this.Surface = Color.White;
			this.Emissive = Color.Black;
		}

		/// <summary>
		/// Gets or sets the emissive scale.
		/// </summary>
		/// <remarks>Scales the emissive color for bloom effects.</remarks>
		public float EmissiveScale { get; set; }

		/// <summary>
		/// Gets or sets the surface color tinting.
		/// </summary>
		[Uniform("Diffuse")]
		public Color Surface { get; set; }

		/// <summary>
		/// Gets or sets the emissive color tinting.
		/// </summary>
		public Color Emissive { get; set; }
	}
}
