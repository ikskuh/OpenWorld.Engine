#version 410

uniform mat4 World;
uniform mat4 View;
uniform mat4 Projection;

uniform sampler2D textureNormalMap;
uniform float specularPower;

#ifdef __VertexShader

layout(location = 0) in vec3 vertexPosition;
layout(location = 1) in vec3 vertexNormal;
layout(location = 2) in vec2 vertexUV;
layout(location = 4) in vec3 vertexTangent;
layout(location = 5) in vec3 vertexBiTangent;

out vec3 position;
out vec3 normal;
out vec3 tangent;
out vec3 bitangent;
out vec2 uv;

void main()
{
	vec4 pos = vec4(vertexPosition, 1);
	gl_Position = Projection * View * World * pos;
	
	position = (World * pos).xyz;

	mat3 mv3x3 = mat3(World);
	normal = normalize(mv3x3 * vertexNormal);
	tangent = normalize(mv3x3 * vertexTangent);
	bitangent = normalize(mv3x3 * vertexBiTangent);

	uv = vertexUV;
}

#endif

#ifdef __FragmentShader

#define WITH_NORMALMAP_UNSIGNED
#define USE_NORMALMAPPING

in vec3 position;
in vec3 normal;
in vec3 tangent;
in vec3 bitangent;
in vec2 uv;

layout(location = 0) out vec4 positionOut;
layout(location = 1) out vec4 normalOut;

void main()
{
	positionOut = vec4(position, 1);

	vec3 bump = normalize(2.0f * texture(textureNormalMap, uv).xyz - 1.0f);
    normalOut.xyz = mat3(tangent, bitangent, normal) * bump;
	normalOut.w = specularPower;
}

#endif