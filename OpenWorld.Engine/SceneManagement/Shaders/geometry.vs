#version 410
layout(location = 0) in vec3 vertexPosition;
layout(location = 1) in vec3 vertexNormal;
layout(location = 2) in vec2 vertexUV;
layout(location = 4) in vec3 vertexTangent;
layout(location = 5) in vec3 vertexBiTangent;

uniform mat4 World;
uniform mat4 View;
uniform mat4 Projection;

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