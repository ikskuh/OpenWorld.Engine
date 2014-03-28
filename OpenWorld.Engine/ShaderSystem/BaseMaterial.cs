using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines a basic material.
	/// </summary>
	[UniformPrefix("mtl")]
	public abstract class BaseMaterial
	{
		/// <summary>
		/// Creates a new basic material
		/// </summary>
		protected BaseMaterial()
		{

		}

		/// <summary>
		/// Gets or sets if the material should be rendered in the translucent pass.
		/// </summary>
		public bool IsTranslucent { get; set; }

		/// <summary>
		/// Gets or sets a specific material shader.
		/// </summary>
		/// <remarks>If null, the default scene material will be used.</remarks>
		public Shader Shader { get; set; }
	}
}
