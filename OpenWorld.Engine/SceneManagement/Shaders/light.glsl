#version 410

uniform mat4 World;
uniform mat4 View;
uniform mat4 Projection;

uniform float Radius;
uniform vec4 Color;
uniform vec3 LightCenter;
uniform vec3 LightDirection;

uniform vec3 ViewPosition;

uniform sampler2D texturePosition;
uniform sampler2D textureNormal;

#ifdef __VertexShader

layout(location = 0) in vec3 vertexPosition;
layout(location = 1) in vec3 vertexNormal;
layout(location = 2) in vec2 vertexUV;

void main()
{
	vec4 pos = vec4(vertexPosition, 1);
	gl_Position = Projection * View * World * pos;
}

#endif

#ifdef __FragmentShader

in vec2 uv;

layout(location = 0) out vec4 diffuse;
layout(location = 1) out vec4 specular;

void main()
{
	vec3 pos = texelFetch(texturePosition, ivec2(gl_FragCoord), 0).xyz;
	vec4 normal = texelFetch(textureNormal, ivec2(gl_FragCoord), 0);
	normal.xyz = normalize(normal.xyz);
	vec3 distance = pos - LightCenter;
	vec3 dir = normalize(distance);

	float falloff = max(0.0f, 1.0f - length(distance) / Radius);

	float diffuseStrength = clamp(dot(dir, -normal.xyz), 0.0f, 1.0f);

	diffuse = 
		Color *
		diffuseStrength * 
		falloff;

	if(diffuseStrength >= 0) {
		specular = 
			Color *
			pow(clamp(dot(reflect(dir, normalize(pos - ViewPosition)), -normal.xyz), 0.0f, 1.0f), normal.w) * 
			falloff;
	} else {
		specular = vec4(0);
	}
}

#endif