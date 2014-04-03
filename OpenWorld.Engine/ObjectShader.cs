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
		const string source = @"
shader:include(""transform"")
shader:include(""mesh"")
shader:include(""deferred"")
shader:include(""material"")

shader:addDefault(""vertex"")
shader:addDefault(""fragment"")

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
