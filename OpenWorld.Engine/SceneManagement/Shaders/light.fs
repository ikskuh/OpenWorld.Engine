#version 330

//layout(pixel_center_integer) in vec4 gl_FragCoord;
in vec2 uv;

layout(location = 0) out vec4 lighting;

uniform float Radius;
uniform vec4 Color;
uniform vec3 LightCenter;
uniform vec3 LightDirection;

uniform sampler2D texturePosition;
uniform sampler2D textureNormal;

void main()
{
	vec3 pos = texelFetch(texturePosition, ivec2(gl_FragCoord), 0).xyz;
	vec3 normal = normalize(texelFetch(textureNormal, ivec2(gl_FragCoord), 0).xyz);
	vec3 dir = pos - LightCenter;

	lighting = 
		Color *
		clamp(dot(normalize(dir), -normal), 0.0f, 1.0f) * 
		max(0.0f, 1.0f - length(dir) / Radius);
}