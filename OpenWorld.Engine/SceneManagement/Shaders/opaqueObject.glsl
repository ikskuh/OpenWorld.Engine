#version 410

uniform mat4 matWorld;
uniform mat4 matView;
uniform mat4 matProjection;

uniform sampler2D meshDiffuseTexture;
uniform sampler2D meshSpecularTexture;
uniform sampler2D meshEmissiveTexture;

uniform sampler2D renderDiffuseLightBuffer;
uniform sampler2D renderSpecularLightBuffer;

uniform vec4 mtlDiffuse;
uniform vec4 mtlSpecular;
uniform vec4 mtlEmissive;
uniform float mtlEmissiveScale;

#ifdef __VertexShader

layout(location = 0) in vec3 vertexPosition;
layout(location = 1) in vec3 vertexNormal;
layout(location = 2) in vec2 vertexUV;

layout(location = 0) out vec2 uv;
layout(location = 1) out vec4 ssuv;
layout(location = 2) out vec3 normal;

void main()
{
	vec4 pos = vec4(vertexPosition, 1);
	gl_Position = ssuv = matProjection * matView * matWorld * pos;

	normal = (matWorld * vec4(vertexNormal, 0)).xyz; 
	uv = vertexUV;
}

#endif

#ifdef __FragmentShader

layout(location = 0) in vec2 uv;
layout(location = 1) in vec4 ssuv;

layout(location = 0) out vec4 color;

void main()
{
	vec4 diffuseColor = texture(meshDiffuseTexture, uv);
	vec3 specularColor = texture(meshSpecularTexture, uv).rgb;
	vec3 emissiveColor = texture(meshEmissiveTexture, uv).rgb;

	vec2 ss = 0.5f + 0.5f * ssuv.xy / ssuv.w;

	color.rgb = mtlDiffuse.rgb * diffuseColor.rgb * textureLod(renderDiffuseLightBuffer, ss, 0.0f).rgb;
	color.rgb += mtlSpecular.rgb * specularColor * textureLod(renderSpecularLightBuffer, ss, 0.0f).rgb;
	
	color.rgb += emissiveColor * mtlEmissiveScale * mtlEmissive.rgb;

	color.a = diffuseColor.a;
	//if(color.a < 0.5f) discard;
}

#endif