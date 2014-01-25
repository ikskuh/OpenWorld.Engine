#version 410

//layout(pixel_center_integer) in vec4 gl_FragCoord;
in vec2 uv;

layout(location = 0) out vec4 diffuse;
layout(location = 1) out vec4 specular;

uniform float Radius;
uniform vec4 Color;
uniform vec3 LightCenter;
uniform vec3 LightDirection;

uniform vec3 ViewPosition;

uniform sampler2D texturePosition;
uniform sampler2D textureNormal;

void main()
{
	vec3 pos = texelFetch(texturePosition, ivec2(gl_FragCoord), 0).xyz;
	vec4 normal = texelFetch(textureNormal, ivec2(gl_FragCoord), 0);
	normal.xyz = normalize(normal.xyz);
	vec3 distance = pos - LightCenter;
	vec3 dir = normalize(distance);

	float falloff = max(0.0f, 1.0f - length(distance) / Radius);

	diffuse = 
		Color *
		clamp(dot(dir, -normal.xyz), 0.0f, 1.0f) * 
		falloff;

	specular = 
		Color *
		pow(clamp(dot(reflect(dir, normalize(pos - ViewPosition)), -normal.xyz), 0.0f, 1.0f), normal.w) * 
		falloff;
}