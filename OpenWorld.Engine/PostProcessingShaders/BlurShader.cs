using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.PostProcessingShaders
{
	/// <summary>
	/// Blurs the source image.
	/// </summary>
	public sealed class BlurShader : PostProcessingShader
	{
		static readonly string shaderSource =
@"uniform vec2 blurStrength;

const int gaussRadius = 11;
const float gaussFilter[gaussRadius] = float[gaussRadius](
	0.0402,0.0623,0.0877,0.1120,0.1297,0.1362,0.1297,0.1120,0.0877,0.0623,0.0402
);
 
void main()
{
	fragment.rgb = vec3(0.0, 0.0, 0.0);
	for (int x = 0; x < gaussRadius; ++x)
	{
		for (int y = 0; y < gaussRadius; ++y)
		{
			vec2 uvx = uv;
			uvx.x += blurStrength.x * (float(x) / float(gaussRadius) - 0.5f);
			uvx.y += blurStrength.y * (float(y) / float(gaussRadius) - 0.5f);
			fragment.rgb += gaussFilter[x] * gaussFilter[y] * texture2D(inputTexture, uvx).rgb;
		}
	}
	//fragment.rgb *= 1.0f / blurArea;
	fragment.a = 1.0f;
}";

		/// <summary>
		/// Creates a new tonemapping shader.
		/// </summary>
		public BlurShader()
			: base(shaderSource)
		{
			this.BlurStrength = new Vector2(0.05f, 0.025f);
		}

		/// <summary>
		/// Gets or sets the HDR exposure.
		/// </summary>
		[Uniform("blurStrength")]
		public Vector2 BlurStrength { get; set; }
	}
}
