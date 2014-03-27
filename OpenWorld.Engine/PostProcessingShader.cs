using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Represents a post processing shader.
	/// </summary>
	public class PostProcessingShader : Shader
	{
		// If const is used and a newer version of the DLL is provided, this string won't change.
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1802:UseLiteralsWhereAppropriate")]
		private static readonly string shaderStart =
@"#version 330
#ifdef __VertexShader
layout(location = 0) in vec2 vertexPosition;
uniform int invertY;
out vec2 uv;

void main()
{
	gl_Position = vec4(vertexPosition.xy, 0.0f, 1.0f);
	if(invertY != 0)
		uv = 0.5f + 0.5f * vertexPosition.xy;
	else
		uv = vec2(0, 1) + vec2(1, -1) * (0.5f + 0.5f * vertexPosition.xy);
}
#endif
#ifdef __FragmentShader
in vec2 uv;
uniform sampler2D inputTexture;
layout(location = 0) out vec4 fragment;";


		// If const is used and a newer version of the DLL is provided, this string won't change.
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1802:UseLiteralsWhereAppropriate")]
		private static readonly string shaderEnd = "#endif";

		// If const is used and a newer version of the DLL is provided, this string won't change.
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1802:UseLiteralsWhereAppropriate")]
		private static readonly string defaultFragmentShader =
@"void main()
{
	fragment = texture(inputTexture, uv);
}";

		/// <summary>
		/// Instantiates a new post processing shader.
		/// </summary>
		public PostProcessingShader()
			: base()
		{
			this.Compile(BuildSource(defaultFragmentShader));
		}

		/// <summary>
		/// Instantiates a new post processing shader and compiles another fragment shader.
		/// <param name="fragmentShader">The fragment shader source for the effect.</param>
		/// </summary>
		public PostProcessingShader(string fragmentShader)
			: base()
		{
			this.Compile(BuildSource(fragmentShader));
		}

		private static string BuildSource(string fragment)
		{
			return shaderStart + "\n" + fragment + "\n" + shaderEnd;
		}

		/// <summary>
		/// Gets or sets if the shader should invert the rendering on the y-axis.
		/// </summary>
		[Uniform("invertY")]
		public bool InvertY { get; set; }
	}
}
