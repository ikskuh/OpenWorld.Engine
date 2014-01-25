using BulletSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Defines a sphere shape.
	/// </summary>
	public sealed class SphereShape : Shape
	{
		BulletSharp.SphereShape sphere;

		/// <summary>
		/// Creates a new sphere shape.
		/// </summary>
		public SphereShape()
		{
			this.Radius = 0.5f;
		}

		/// <summary>
		/// Starts the component.
		/// </summary>
		/// <param name="time">Time snapshot</param>
		protected override void OnStart(GameTime time)
		{
			base.OnStart(time);

			this.sphere = new BulletSharp.SphereShape(this.Radius);
		}

		/// <summary>
		/// Gets the Jitter shape fitting this shape component.
		/// </summary>
		/// <returns>A Jitter shape.</returns>
		protected internal override CollisionShape GetShape()
		{
			return this.sphere;
		}

		/// <summary>
		/// Gets or sets the radius of the sphere.
		/// </summary>
		public float Radius { get;set; }
	}
}
