using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.PostProcessingShaders
{
	/// <summary>
	/// Provides a dithering shader that lets the screen flicker at an unnoticable level.
	/// </summary>
	public sealed class DitheringShader : PostProcessingShader
	{
		static readonly string shaderSource = 
@"out vec4 color;
uniform float time;
uniform float strength;

// A single iteration of Bob Jenkins' One-At-A-Time hashing algorithm.
uint hash( uint x ) {
    x += ( x << 10u );
    x ^= ( x >>  6u );
    x += ( x <<  3u );
    x ^= ( x >> 11u );
    x += ( x << 15u );
    return x;
}

// Compound versions of the hashing algorithm I whipped together.
uint hash( uvec2 v ) { return hash( v.x ^ hash(v.y)                         ); }
uint hash( uvec3 v ) { return hash( v.x ^ hash(v.y) ^ hash(v.z)             ); }
uint hash( uvec4 v ) { return hash( v.x ^ hash(v.y) ^ hash(v.z) ^ hash(v.w) ); }



// Construct a float with half-open range [0:1] using low 23 bits.
// All zeroes yields 0.0, all ones yields the next smallest representable value below 1.0.
float floatConstruct( uint m ) {
    const uint ieeeMantissa = 0x007FFFFFu; // binary32 mantissa bitmask
    const uint ieeeOne      = 0x3F800000u; // 1.0 in IEEE binary32

    m &= ieeeMantissa;                     // Keep only mantissa bits (fractional part)
    m |= ieeeOne;                          // Add fractional part to 1.0

    float  f = uintBitsToFloat( m );       // Range [1:2]
    return f - 1.0;                        // Range [0:1]
}



// Pseudo-random value in half-open range [0:1].
float random( float x ) { return floatConstruct(hash(floatBitsToUint(x))); }
float random( vec2  v ) { return floatConstruct(hash(floatBitsToUint(v))); }
float random( vec3  v ) { return floatConstruct(hash(floatBitsToUint(v))); }
float random( vec4  v ) { return floatConstruct(hash(floatBitsToUint(v))); }

vec3 rand3(vec2 v)
{
	vec3 r;
	r.x = random(v + vec2(133123.21f, 1.124343f));
	r.y = random(v + vec2(4698.8374f, 1.19799f));
	r.z = random(v + vec2(64698.6f, 1.3376f));
	return r;
}

void main()
{
	color = texture(backBuffer, uv);
	vec3  inputs = vec3( gl_FragCoord.xy, time ); // Spatial and temporal inputs
    float rand   = random( inputs );              // Random per-pixel value

	color.rgb += strength * rand3(gl_FragCoord.xy + vec2(time, 1.2f * time));
}";

		float time = 0;

		/// <summary>
		/// Creates a new dithering shader.
		/// </summary>
		public DitheringShader()
			: base(shaderSource)
		{
			this.Strength = 1.0f / 64.0f;
		}

		/// <summary>
		/// Applies the effect parameters.
		/// </summary>
		protected override void OnApply()
		{
			base.OnApply();

			this.SetUniform("time", time);

			time += 1.0f;
			if (time > 100.0f)
				time -= 100.0f;
		}

		/// <summary>
		/// Gets or sets the strength of the dithering.
		/// </summary>
		[Uniform("strength")]
		public float Strength { get;set; }
	}
}
