using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines a method to set uniforms in a shader.
	/// </summary>
	public interface IShaderUniforms
	{
		/// <summary>
		/// Sets uniforms for a shader.
		/// </summary>
		/// <param name="shader">Target shader</param>
		/// <param name="textureOffset">Declares the first unused texture id.</param>
		void SetUniforms(Shader shader, ref int textureOffset);
	}
}
