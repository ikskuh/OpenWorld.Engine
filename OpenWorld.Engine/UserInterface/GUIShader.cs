using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine.UserInterface
{
	class GUIShader : Shader
	{
		string source =
@"#version 330

uniform mat4 Transform;
uniform sampler2D uiTexture;

#ifdef __VertexShader

layout(location = 0) in vec2 vertexPosition;
layout(location = 1) in vec4 vertexColor;
layout(location = 2) in vec2 vertexUV;

out vec2 uv;
out vec4 color;

void main()
{
	gl_Position = Transform * vec4(vertexPosition, 0.0f, 1.0f);
	uv = vertexUV;
	color = vertexColor;
}
#endif
#ifdef __FragmentShader

layout(location = 0) out vec4 result;

in vec2 uv;
in vec4 color;

void main()
{
	result = color * texture(uiTexture, uv);
}
#endif";

		public GUIShader()
		{
			this.Compile(this.source);
		}

		[Uniform("Transform")]
		public Matrix4 Transform { get; set; }

		[Uniform("uiTexture")]
		public Texture2D Texture { get; set; }
	}
}
