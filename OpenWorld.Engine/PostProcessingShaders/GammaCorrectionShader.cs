using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.PostProcessingShaders
{
	/// <summary>
	/// Provides a gamma correction shader that can be used in HDR rendering to get a correct gamma.
	/// </summary>
	public sealed class GammaCorrectionShader : PostProcessingShader
	{
		static readonly string shaderSource = @"#version 410
uniform sampler2D backBuffer;
uniform float gamma;

out vec4 color;

void main()
{
	color = texelFetch(backBuffer, ivec2(gl_FragCoord), 0);
	color.rgb = pow(color.rgb, vec3(1.0f) / gamma); // Gamma correction
}";

		/// <summary>
		/// Creates a new gamma correction shader.
		/// </summary>
		public GammaCorrectionShader()
			: base(shaderSource)
		{
			this.Gamma = 2.2f;
		}

		/// <summary>
		/// Gets or sets the gamma value that is used for gamma correction.
		/// </summary>
		[Uniform("gamma")]
		public float Gamma { get; set; }
	}
}
