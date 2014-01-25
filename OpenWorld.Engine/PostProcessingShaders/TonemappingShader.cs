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
		static readonly string shaderSource = @"#version 410
layout(location = 0) out vec4 result;

uniform float hdrExposure;
uniform sampler2D backBuffer;

void main()
{
	result = texelFetch(backBuffer, ivec2(gl_FragCoord), 0);

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
		/// Applies the effect parameters.
		/// </summary>
		protected override void OnApply()
		{
			base.OnApply();
			this.SetUniform("hdrExposure", this.HdrExposure);
		}

		/// <summary>
		/// Gets or sets the HDR exposure.
		/// </summary>
		public float HdrExposure { get; set; }
	}
}
