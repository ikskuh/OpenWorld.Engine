#version 410

uniform mat4 World;
uniform mat4 View;
uniform mat4 Projection;

uniform sampler2D textureDiffuseColor;
uniform sampler2D textureSpecularColor;
uniform sampler2D textureDiffuseLighting;
uniform sampler2D textureSpecularLighting;

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
	gl_Position = ssuv = Projection * View * World * pos;

	normal = (World * vec4(vertexNormal, 0)).xyz; 
	uv = vertexUV;
}

#endif

#ifdef __FragmentShader

layout(location = 0) in vec2 uv;
layout(location = 1) in vec4 ssuv;

layout(location = 0) out vec4 color;

void main()
{
	vec4 diffuseColor = texture(textureDiffuseColor, uv);
	vec3 specularColor = texture(textureSpecularColor, uv).rgb;

	vec2 ss = 0.5f + 0.5f * ssuv.xy / ssuv.w;

	color.rgb = diffuseColor.rgb * texture(textureDiffuseLighting, ss).rgb;
	color.rgb += specularColor * texture(textureSpecularLighting, ss).rgb;
	
	color.a = diffuseColor.a;
}

#endif