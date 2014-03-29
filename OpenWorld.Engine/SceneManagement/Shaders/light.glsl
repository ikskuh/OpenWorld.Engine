shader:add
{
	type = "global",
	source = 
[[
	uniform mat4 matWorld;
	uniform mat4 matView;
	uniform mat4 matProjection;

	uniform float lightRadius;
	uniform vec4 lightColor;
	uniform vec3 lightPosition;
	uniform vec3 lightLightDirection;

	uniform vec3 lightViewPosition;

	uniform sampler2D renderPositionBuffer;
	uniform sampler2D renderNormalBuffer;
]]
}

shader:add
{
	type = "vertex",
	input = "mesh",
	source = 
[[
	void main()
	{
		vec4 pos = vec4(vertexPosition, 1);
		gl_Position = matProjection * matView * matWorld * pos;
	}
]]
}
shader:add
{
	type = "fragment",
	source = 
[[
	in vec2 uv;

	layout(location = 0) out vec4 diffuse;
	layout(location = 1) out vec4 specular;

	void main()
	{
		vec3 pos = texelFetch(renderPositionBuffer, ivec2(gl_FragCoord), 0).xyz;
		vec4 normal = texelFetch(renderNormalBuffer, ivec2(gl_FragCoord), 0);
		normal.xyz = normalize(normal.xyz);
		vec3 distance = pos - lightPosition;
		vec3 dir = normalize(distance);

		float falloff = clamp(1.0f - length(distance) / lightRadius, 0.0f, 1.0f);

		float diffuseStrength = clamp(dot(dir, -normal.xyz), 0.0f, 1.0f);

		diffuse = 
			lightColor *
			diffuseStrength * 
			falloff;

		if(diffuseStrength > 0) {
			specular = 
				lightColor *
				pow(clamp(-dot(reflect(dir, normalize(pos - lightViewPosition)), normal.xyz), 0.0f, 1.0f), normal.w) * 
				falloff;
		} else {
			specular = vec4(0.0f);
		}
	}
]]
}