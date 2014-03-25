#version 410

uniform mat4 matWorld;
uniform mat4 matView;
uniform mat4 matProjection;

uniform sampler2D meshDiffuseTexture;
uniform sampler2D meshSpecularTexture;
uniform sampler2D renderDiffuseLightBuffer;
uniform sampler2D renderSpecularLightBuffer;

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

	vec2 ss = 0.5f + 0.5f * ssuv.xy / ssuv.w;

	color.rgb = diffuseColor.rgb * texture(renderDiffuseLightBuffer, ss).rgb;
	color.rgb += specularColor * texture(renderSpecularLightBuffer, ss).rgb;
	
	color.a = diffuseColor.a;
	//if(color.a < 0.5f) discard;
}

#endif