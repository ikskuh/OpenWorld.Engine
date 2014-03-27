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
@"uniform float hdrExposure;
void main()
{
	fragment = texture(inputTexture, uv);

	//Adjust color from HDR
	//fragment.rgb = 1.0 - exp(fragment.rgb * -hdrExposure);	

	float wp = 5.0f;
	
	vec3 tonemap = fragment.rgb;
	tonemap = ((((tonemap) * (0.193f * (tonemap) + 0.070f * 0.196f) + 0.330f * 0.007f) / ((tonemap) * (0.193f * (tonemap) + 0.196f)+ 0.330 * 0.111f)) - 0.007f / 0.111f); 
	tonemap /= (((wp*(0.193*wp+0.070*0.196) + 0.330f * 0.007f) / (wp * (0.193f * wp + 0.196f) + 0.330f * 0.111f)) - 0.007f / 0.111f); 
	fragment.rgb = tonemap;
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
