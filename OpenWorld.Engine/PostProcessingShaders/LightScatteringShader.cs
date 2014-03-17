using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.PostProcessingShaders
{
	/// <summary>
	/// Provides a light scattering shader (also known as 'Godray Shader').
	/// </summary>
	public sealed class LightScatteringShader : PostProcessingShader
	{
		const string shaderSource =
@"uniform float exposure;
uniform float decay;
uniform float density;
uniform float weight;
uniform vec2 lightPositionOnScreen;
uniform sampler2D occlusionBuffer;

const int NUM_SAMPLES = 150;

out vec4 color;

void main()
{	
 	vec2 deltaTextCoord = vec2(uv - lightPositionOnScreen.xy);
 	deltaTextCoord *= 1.0 /  float(NUM_SAMPLES) * density;
 	float illuminationDecay = 1.0;

	vec3 source = texture(backBuffer, uv).rgb;
	
	vec2 texCoord = uv;
	color = vec4(0.0f, 0.0f, 0.0f, 1.0f);
 	for(int i=0; i < NUM_SAMPLES ; i++)
  	{
    	texCoord -= deltaTextCoord;
    	vec3 smpl = texture(backBuffer, texCoord).rgb - 4.0f;
		vec3 normal = texture(occlusionBuffer, texCoord).rgb;
		if(length(normal) > 0.05f)
			smpl = vec3(0.0f);
    	smpl *= illuminationDecay * weight;
    	color.rgb += smpl;
    	illuminationDecay *= decay;
 	}
 	color.rgb *= exposure;

	// Fake additive blending
	color.rgb = source + max(vec3(0.0f), color.rgb);
}";

		/// <summary>
		/// Creates a new light scattering shader.
		/// </summary>
		public LightScatteringShader()
			: base(shaderSource)
		{
			this.Exposure = 0.005f;
			this.Decay = 0.98f;
			this.Density = 1.0f;
			this.Weight = 1.0f / 16.0f;
		}

		/// <summary>
		/// Gets or sets the occlusion buffer.
		/// </summary>
		[Uniform("occlusionBuffer")]
		public Texture OcclusionBuffer { get; set; }

		/// <summary>
		/// Gets or sets the decay. The higher this value is, the longer the rays will be.
		/// </summary>
		[Uniform("decay")]
		public float Decay { get; set; }

		/// <summary>
		/// Gets or sets the exposure. Ray brightness will be scaled by the exposure.
		/// <remarks>Usually about 0.005</remarks>
		/// </summary>
		[Uniform("exposure")]
		public float Exposure { get; set; }

		/// <summary>
		/// Gets or sets the density. Ray density defines how long a ray is max.
		/// </summary>
		[Uniform("density")]
		public float Density { get; set; }

		/// <summary>
		/// Gets or sets the weight. The weight defines how strong every sample on the ray will be taken into account.
		/// </summary>
		[Uniform("weight")]
		public float Weight { get; set; }

		/// <summary>
		/// Gets or sets the screen space light position.
		/// </summary>
		[Uniform("lightPositionOnScreen")]
		public Vector2 LightPosition { get; set; }
	}
}
