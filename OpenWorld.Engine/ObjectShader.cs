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
		const string source = @"shader:add
{
	type = ""global"",
	source = 
[[
	// Transformation matrices
	uniform mat4 matWorld;
	uniform mat4 matView;
	uniform mat4 matProjection;

	// Mesh textures
	uniform sampler2D meshDiffuseTexture;
	uniform sampler2D meshSpecularTexture;
	uniform sampler2D meshEmissiveTexture;

	// Deferred renderer uniforms
	uniform sampler2D renderDiffuseLightBuffer;
	uniform sampler2D renderSpecularLightBuffer;

	// Material Values
	uniform vec4 mtlDiffuse;
	uniform vec4 mtlSpecular;
	uniform vec4 mtlEmissive;
	uniform float mtlEmissiveScale;
]]
}

shader:add
{
	type = ""vertex"",
	input = ""mesh"",
	source = 
[[
	out vec3 position;
	out vec3 normal;
	out vec3 tangent;
	out vec3 bitangent;
	out vec2 uv;
	out vec4 screenSpaceUV;

	void main()
	{
		vec4 pos = vec4(vertexPosition, 1);
		gl_Position = matProjection * matView * matWorld * pos;

		position = (matWorld * pos).xyz;

		mat3 mv3x3 = mat3(matWorld);
		normal = normalize(mv3x3 * vertexNormal);
		tangent = normalize(mv3x3 * vertexTangent);
		bitangent = normalize(mv3x3 * vertexBiTangent);

		uv = vertexUV;
		screenSpaceUV = gl_Position;
	}
]]
}

shader:add
{
	type = ""fragment"",
	source = 
[[
	layout(location = 0) out vec4 color;
	in vec2 uv;
	void main()
	{
		color = texture(meshDiffuseTexture, uv);
		if(color.a < 0.5f) discard;
	}
]]
}

shader:add
{
	type = ""fragment"",
	class = ""DeferredRenderer"",
	source = 
[[
	in vec2 uv;
	in vec4 screenSpaceUV;

	layout(location = 0) out vec4 color;

	void main()
	{
		vec4 diffuseColor = texture(meshDiffuseTexture, uv);
		vec3 specularColor = texture(meshSpecularTexture, uv).rgb;
		vec3 emissiveColor = texture(meshEmissiveTexture, uv).rgb;

		vec2 ss = 0.5f + 0.5f * screenSpaceUV.xy / screenSpaceUV.w;

		color.rgb = mtlDiffuse.rgb * diffuseColor.rgb * textureLod(renderDiffuseLightBuffer, ss, 0.0f).rgb;
		color.rgb += mtlSpecular.rgb * specularColor * textureLod(renderSpecularLightBuffer, ss, 0.0f).rgb;
	
		color.rgb += emissiveColor * mtlEmissiveScale * mtlEmissive.rgb;

		color.a = diffuseColor.a;
		if(color.a < 0.5f) discard;
	}
]]
}";

		/// <summary>
		/// Instantiates a new object shader.
		/// </summary>
		public ObjectShader()
		{
			this.Load(source);
		}
	}
}
