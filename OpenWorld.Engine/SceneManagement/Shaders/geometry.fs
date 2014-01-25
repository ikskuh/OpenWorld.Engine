#version 410

#define WITH_NORMALMAP_UNSIGNED
#define USE_NORMALMAPPING

in vec3 position;
in vec3 normal;
in vec3 tangent;
in vec3 bitangent;
in vec2 uv;

layout(location = 0) out vec4 positionOut;
layout(location = 1) out vec4 normalOut;

uniform sampler2D textureNormalMap;
uniform vec3 viewPosition;

void main()
{
	positionOut = vec4(position, 1);

	//vec3 bump = normalize((255.0f / 127.0f) * texture(textureNormalMap, uv).xyz - (127.0f / 255.0f));
	vec3 bump = normalize(2.0f * texture(textureNormalMap, uv).xyz - 1.0f);
    normalOut.xyz = mat3(tangent, bitangent, normal) * bump;
	normalOut.w = 1.0f;
}