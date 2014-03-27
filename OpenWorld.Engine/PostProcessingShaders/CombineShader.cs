using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.PostProcessingShaders
{
	class CombineShader : PostProcessingShader
	{
		static readonly string shaderSource =
@"uniform sampler2D originalMap;
 
void main()
{
	fragment = texture(inputTexture, uv);


	fragment += texture(originalMap, uv);
	fragment.a = 1.0f;
}";

		/// <summary>
		/// Creates a new tonemapping shader.
		/// </summary>
		public CombineShader()
			: base(shaderSource)
		{
		}


		[Uniform("originalMap")]
		public Texture2D OriginalMap { get; set; }
	}
}
