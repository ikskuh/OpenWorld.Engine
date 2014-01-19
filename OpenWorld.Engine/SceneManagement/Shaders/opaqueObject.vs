#version 330
layout(location = 0) in vec3 vertexPosition;
layout(location = 1) in vec3 vertexNormal;
layout(location = 2) in vec2 vertexUV;

uniform mat4 World;
uniform mat4 View;
uniform mat4 Projection;

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