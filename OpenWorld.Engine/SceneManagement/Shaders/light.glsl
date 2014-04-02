shader:add
{
	type = "global",
	source = 
[[
	struct BrdfResult
	{
		vec3 diffuse;
		vec3 specular;
	};

	uniform mat4 matWorld;
	uniform mat4 matView;
	uniform mat4 matProjection;

	uniform float lightRange;
	uniform float lightIntensity;
	uniform vec4 lightColor;
	uniform vec3 lightPosition;
	uniform vec3 lightDirection;
	uniform vec3 lightViewPosition;

	uniform sampler2D renderPositionBuffer;
	uniform sampler2D renderNormalBuffer;

	// Simple lambertian lighting
	float diffuse_term(vec3 direction, vec3 normal)
	{
		return clamp(dot(direction, -normal.xyz), 0.0f, 1.0f);
	}

	// Simple phong shading
	float specular_term(vec3 direction, vec3 viewDir, vec3 normal, float specPower)
	{
		vec3 ref = reflect(viewDir, normal);
		float linear = clamp(-dot(ref, -normal.xyz), 0.0f, 1.0f);
		return pow(linear, specPower);
	}
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

-- Directional brdf
shader:add
{
	type = "global",
	class = "DirectionalLight",
	source = 
[[
	BrdfResult brdf(vec3 pos, vec3 normal, vec3 viewDir, float specPower)
	{
		BrdfResult result;

		result.diffuse  = vec3(1.0f) * diffuse_term(lightDirection, normal);
		result.specular = vec3(1.0f) * specular_term(lightDirection, viewDir, normal, specPower);

		return result;
	}
]]
}

-- Point light brdf
shader:add
{
	type = "global",
	class = "PointLight",
	source = 
[[
	BrdfResult brdf(vec3 pos, vec3 normal, vec3 viewDir, float specPower)
	{
		BrdfResult result;

		vec3 distance = pos - lightPosition;
		vec3 lightDir = normalize(distance);

		float falloff = 1.0f / (length(distance) * length(distance));

		result.diffuse  = vec3(falloff) * diffuse_term(lightDir, normal);
		result.specular = vec3(falloff) * specular_term(lightDir, viewDir, normal, specPower);

		return result;
	}
]]
}

-- Fragment shader
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

		vec3 viewDir = normalize(pos - lightViewPosition);

		BrdfResult result = brdf(pos, normal.xyz, viewDir, normal.w);

		diffuse.rgb = 
			lightColor.rgb *
			result.diffuse * 
			lightIntensity;
		diffuse.a = 1.0f;

		specular.rgb = 
			lightColor.rgb *
			result.specular *
			lightIntensity;
		specular.a = 1.0f;
	}
]]
}