<?xml version="1.0"?>
<Shader xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
	<VertexShader>#version 330
layout(location = 0) in vec3 vertexPosition;
layout(location = 1) in vec3 vertexNormal;

uniform mat4 World;
uniform mat4 View;
uniform mat4 Projection;

out vec3 normal;

void main()
{
	vec4 pos = vec4(vertexPosition, 1);
	gl_Position = Projection * View * World * pos;
	normal = normalize(World * vec4(vertexNormal, 0));
}
	</VertexShader>
	<FragmentShader>#version 330
out vec4 color;

in vec3 normal;

void main()
{
	color.rgb = 0.5f + 0.5f * normal;
	color.a = 1.0f;
}</FragmentShader>
</Shader>