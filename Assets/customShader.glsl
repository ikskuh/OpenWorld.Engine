#version 330 core

#extension GL_ARB_geometry_shader4 : enable

uniform mat4 World;
uniform mat4 View;
uniform mat4 Projection;
uniform sampler2D textureDiffuse;

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
#ifdef __GeometryShader
layout(triangles) in;
layout(triangle_strip, max_vertices=3) out;
 
void main()
{
	for(i = 0; i < gl_VerticesIn; i++)
	{
		gl_Position = gl_PositionIn[i];
		EmitVertex();
	}
	EmitPrimitive();
}
#endif
#ifdef __FragmentShader
layout(location = 0) out vec4 color;

void main()
{
	color = vec4(1, 0, 0, 1);
}
#endif