#version 410

//layout(pixel_center_integer) in vec4 gl_FragCoord;
layout(location = 0) in vec2 uv;
layout(location = 1) in vec4 ssuv;

layout(location = 0) out vec4 color;

uniform sampler2D textureDiffuseColor;
uniform sampler2D textureSpecularColor;
uniform sampler2D textureDiffuseLighting;
uniform sampler2D textureSpecularLighting;

void main()
{
	vec4 diffuseColor = texture(textureDiffuseColor, uv);
	vec3 specularColor = texture(textureSpecularColor, uv).rgb;

	vec2 ss = 0.5f + 0.5f * ssuv.xy / ssuv.w;

	color.rgb = diffuseColor.rgb * texture(textureDiffuseLighting, ss).rgb;
	color.rgb += specularColor * texture(textureSpecularLighting, ss).rgb;
	
	color.a = diffuseColor.a;
}