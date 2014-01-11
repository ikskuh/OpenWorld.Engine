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
	[AssetExtension(".shader")]
	public class ObjectShader : Shader, IAsset
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
		/// Applies the shader parameters.
		/// </summary>
		protected override void OnApply()
		{
			this.SetUniform("World", this.World, false);
			this.SetUniform("View", this.View, false);
			this.SetUniform("Projection", this.Projection, false);
			this.SetTexture("textureDiffuse", this.DiffuseTexture, 0);

			base.OnApply();
		}

		void IAsset.Load(AssetLoadContext context, System.IO.Stream stream, string extensionHint)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(ShaderSource));
			ShaderSource source = serializer.Deserialize(stream) as ShaderSource;
			this.Compile(source.VertexShader, source.FragmentShader);
		}

		/// <summary>
		/// Gets or sets the world transformation matrix.
		/// <remarks>Shader uniform: mat4 World</remarks>
		/// </summary>
		public Matrix4 World { get; set; }

		/// <summary>
		/// Gets or sets the view matrix.
		/// <remarks>Shader uniform: mat4 View</remarks>
		/// </summary>
		public Matrix4 View { get; set; }

		/// <summary>
		/// Gets or sets the projection matrix.
		/// <remarks>Shader uniform: mat4 Projection</remarks>
		/// </summary>
		public Matrix4 Projection { get; set; }

		/// <summary>
		/// Gets or sets the diffuse texture.
		/// </summary>
		public Texture DiffuseTexture { get; set; }

		/// <summary>
		/// Defines a serializable shader code container.
		/// </summary>
		[Serializable]
		public class ShaderSource
		{
			/// <summary>
			/// Gets or sets vertex shader source
			/// </summary>
			public string VertexShader { get; set; }
			
			/// <summary>
			/// Gets or sets fragment shader source
			/// </summary>
			public string FragmentShader { get; set; }
		}
	}
}
