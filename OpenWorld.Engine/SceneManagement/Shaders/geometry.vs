#version 410
layout(location = 0) in vec3 vertexPosition;
layout(location = 1) in vec3 vertexNormal;
layout(location = 2) in vec2 vertexUV;

uniform mat4 World;
uniform mat4 View;
uniform mat4 Projection;

out vec3 position;
out vec3 normal;

void main()
{
	vec4 pos = vec4(vertexPosition, 1);
	gl_Position = Projection * View * World * pos;
	
	position = (World * pos).xyz;
	normal = (World * vec4(vertexNormal, 0)).xyz;
}