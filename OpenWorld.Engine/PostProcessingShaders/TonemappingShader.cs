using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.PostProcessingShaders
{
	/// <summary>
	/// Provides a simple tonemapping shader.
	/// </summary>
	public sealed class TonemappingShader : PostProcessingShader
	{
		static readonly string shaderSource =
@"layout(location = 0) out vec4 result;
uniform float hdrExposure;
void main()
{
	result = texture(backBuffer, uv);

	//Adjust color from HDR
	result.rgb = 1.0 - exp(result.rgb * -hdrExposure);	
}";

		/// <summary>
		/// Creates a new tonemapping shader.
		/// </summary>
		public TonemappingShader()
			: base(shaderSource)
		{
			this.HdrExposure = 1.25f;
		}

		/// <summary>
		/// Gets or sets the HDR exposure.
		/// </summary>
		[Uniform("hdrExposure")]
		public float HdrExposure { get; set; }
	}
}
