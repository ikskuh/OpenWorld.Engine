using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.PostProcessingShaders
{
	class HighPassShader : PostProcessingShader
	{
		static readonly string shaderSource =
@"uniform float bloomThreshold;
 
void main()
{
	fragment = max(vec4(0.0f), (texture(inputTexture, uv) - bloomThreshold)) * (1.0f / bloomThreshold);
	fragment.a = 1.0f;
}";

		/// <summary>
		/// Creates a new tonemapping shader.
		/// </summary>
		public HighPassShader()
			: base(shaderSource)
		{
			this.BloomThreshold = 0.95f;
		}

		/// <summary>
		/// Gets or sets the HDR exposure.
		/// </summary>
		[Uniform("bloomThreshold")]
		public float BloomThreshold { get; set; }
	}
}
