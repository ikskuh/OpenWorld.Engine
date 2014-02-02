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
		const string defaultVertexShader = @"#version 330
layout(location = 0) in vec3 vertexPosition;
layout(location = 2) in vec2 vertexUV;

uniform mat4 World;
uniform mat4 View;
uniform mat4 Projection;

out vec2 uv;

void main()
{
	vec4 pos = vec4(vertexPosition, 1);
	gl_Position = Projection * View * World * pos;
	
	uv = vertexUV;
}";
		const string defaultFragmentShader = @"#version 330
layout(location = 0) out vec4 color;

in vec2 uv;

uniform sampler2D textureDiffuse;

void main()
{
	color = texture(textureDiffuse, uv);
}";
		/// <summary>
		/// Instantiates a new object shader.
		/// </summary>
		public ObjectShader()
		{
			this.Compile(defaultVertexShader, defaultFragmentShader);
		}

		/// <summary>
		/// Gets or sets the world transformation matrix.
		/// <remarks>Shader uniform: mat4 World</remarks>
		/// </summary>
		[Uniform("World")]
		public Matrix4 World { get; set; }

		/// <summary>
		/// Gets or sets the view matrix.
		/// <remarks>Shader uniform: mat4 View</remarks>
		/// </summary>
		[Uniform("View")]
		public Matrix4 View { get; set; }

		/// <summary>
		/// Gets or sets the projection matrix.
		/// <remarks>Shader uniform: mat4 Projection</remarks>
		/// </summary>
		[Uniform("Projection")]
		public Matrix4 Projection { get; set; }

		/// <summary>
		/// Gets or sets the diffuse texture.
		/// </summary>
		[Uniform("textureDiffuse")]
		public Texture DiffuseTexture { get; set; }
	}
}
