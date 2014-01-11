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
		private static readonly string vertexShader =
@"#version 330
layout(location = 0) in vec2 vertexPosition;

out vec2 uv;

void main()
{
	gl_Position = vec4(vertexPosition.xy, 0.0f, 1.0f);
	uv = 0.5f + 0.5f * vertexPosition.xy;
}";

		// If const is used and a newer version of the DLL is provided, this string won't change.
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1802:UseLiteralsWhereAppropriate")]
		private static readonly string defaultFragmentShader =
@"#version 330
layout(location = 0) out vec4 result;

uniform sampler2D backBuffer;

void main()
{
	result = texelFetch(backBuffer, ivec2(gl_FragCoord), 0);
}
";

		/// <summary>
		/// Instantiates a new post processing shader.
		/// </summary>
		public PostProcessingShader()
			: base()
		{
			this.Compile(vertexShader, defaultFragmentShader);
		}

		/// <summary>
		/// Instantiates a new post processing shader and compiles another fragment shader.
		/// <param name="fragmentShader">The fragment shader source for the effect.</param>
		/// </summary>
		public PostProcessingShader(string fragmentShader)
			: base()
		{
			this.Compile(vertexShader, fragmentShader);
		}

		/// <summary>
		/// Applies the effect parameters.
		/// </summary>
		protected override void OnApply()
		{
			base.OnApply();
			this.SetTexture("backBuffer", this.BackBuffer, 0);
		}

		/// <summary>
		/// Gets or sets the back buffer.
		/// </summary>
		public Texture BackBuffer { get; set; }
	}
}
