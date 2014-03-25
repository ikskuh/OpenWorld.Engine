using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines an object shader for 3d objects.
	/// </summary>
	public class ObjectShader : Shader
	{
		const string source = @"#version 330
uniform mat4 matWorld;
uniform mat4 matView;
uniform mat4 matProjection;
uniform sampler2D meshDiffuseTexture;

#ifdef __VertexShader
layout(location = 0) in vec3 vertexPosition;
layout(location = 2) in vec2 vertexUV;

out vec2 uv;

void main()
{
	vec4 pos = vec4(vertexPosition, 1);
	gl_Position = matProjection * matView * matWorld * pos;
	
	uv = vertexUV;
}
#endif
#ifdef __FragmentShader
layout(location = 0) out vec4 color;

in vec2 uv;

void main()
{
	color = texture(meshDiffuseTexture, uv);
	if(color.a < 0.5f) discard;
}
#endif";

		/// <summary>
		/// Instantiates a new object shader.
		/// </summary>
		public ObjectShader()
		{
			this.Compile(source);
		}
	}
}
