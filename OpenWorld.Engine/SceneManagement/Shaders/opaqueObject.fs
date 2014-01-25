#version 410

//layout(pixel_center_integer) in vec4 gl_FragCoord;
layout(location = 0) in vec2 uv;
layout(location = 1) in vec4 ssuv;

layout(location = 0) out vec4 color;

uniform sampler2D textureDiffuse;
uniform sampler2D textureLighting;

void main()
{
	color = texture(textureDiffuse, uv);
	color.rgb *= texture(textureLighting, 0.5f + 0.5f * ssuv.xy / ssuv.w).xyz;
}