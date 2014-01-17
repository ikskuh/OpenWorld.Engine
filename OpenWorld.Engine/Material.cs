using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines a material.
	/// </summary>
	public sealed class Material
	{
		Jitter.Dynamics.Material jitterMaterial = new Jitter.Dynamics.Material();

		/// <summary>
		/// Gets or sets the material shader.
		/// </summary>
		public ObjectShader Shader { get; set; }

		/// <summary>
		/// Gets or sets a value that indicates wheather this material is translucent or solid.
		/// </summary>
		public bool IsTranslucent { get; set; }

		/// <summary>
		/// Gets or sets the kinetic friction.
		/// </summary>
		public float KineticFriction
		{
			get { return this.jitterMaterial.StaticFriction; }
			set { this.jitterMaterial.StaticFriction = value; }
		}

		/// <summary>
		/// Gets or sets the restitution.
		/// </summary>
		public float Restitution
		{
			get { return this.jitterMaterial.StaticFriction; }
			set { this.jitterMaterial.StaticFriction = value; }
		}

		/// <summary>
		/// Gets or sets the static friction.
		/// </summary>
		public float StaticFriction
		{
			get { return this.jitterMaterial.StaticFriction; }
			set { this.jitterMaterial.StaticFriction = value; }
		}

		internal Jitter.Dynamics.Material JitterMaterial { get { return this.jitterMaterial; } }
	}
}
