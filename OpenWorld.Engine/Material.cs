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
		/// Gets or sets the material shader.
		/// </summary>
		public ObjectShader Shader { get; set; }

		/// <summary>
		/// Gets or sets a value that indicates wheather this material is translucent or solid.
		/// </summary>
		public bool IsTranslucent { get; set; }
	}
}
