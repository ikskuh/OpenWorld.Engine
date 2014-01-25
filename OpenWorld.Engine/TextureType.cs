using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Specifies the type of a texture.
	/// </summary>
	public enum TextureType
	{
		/// <summary>
		/// A diffuse texture.
		/// </summary>
		Diffuse,

		/// <summary>
		/// A normal map. 
		/// </summary>
		NormalMap,

		/// <summary>
		/// A specular texture.
		/// </summary>
		Specular,
	}
}
