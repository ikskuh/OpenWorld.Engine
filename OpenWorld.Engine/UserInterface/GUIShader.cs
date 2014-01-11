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
		string vertexShader =
@"#version 330
layout(location = 0) in vec2 vertexPosition;
layout(location = 1) in vec4 vertexColor;
layout(location = 2) in vec2 vertexUV;

uniform mat4 Transform;

out vec2 uv;
out vec4 color;

void main()
{
	gl_Position = Transform * vec4(vertexPosition, 0.0f, 1.0f);
	uv = vertexUV;
	color = vertexColor;
}";

		string fragmentShader =
@"#version 330
layout(location = 0) out vec4 result;

in vec2 uv;
in vec4 color;

uniform sampler2D uiTexture;

void main()
{
	result = color * texture(uiTexture, uv);
}";

		public GUIShader()
		{
			this.Compile(this.vertexShader, this.fragmentShader);
		}

		public void Compile(string fragmentShader)
		{
			this.Compile(this.vertexShader, fragmentShader);
		}

		protected override void OnApply()
		{
			base.OnApply();

			this.SetUniform("Transform", this.Transform);
			this.SetTexture("uiTexture", this.Texture, 0);
		}

		public Matrix4 Transform { get; set; }

		public Texture2D Texture { get; set; }
	}
}
