#version 330

in vec3 position;
in vec3 normal;

layout(location = 0) out vec4 positionOut;
layout(location = 1) out vec4 normalOut;

uniform sampler2D textureDiffuse;
uniform sampler2D textureLighting;

void main()
{
	positionOut = vec4(position, 1);
	normalOut = vec4(normal, 1);
}