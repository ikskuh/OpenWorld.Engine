using BulletSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Defines a box shape.
	/// </summary>
	public sealed class BoxShape : Shape
	{
		BulletSharp.BoxShape boxShape;

		/// <summary>
		/// Creates a new box shape.
		/// </summary>
		public BoxShape()
		{
			this.Width = 1.0f;
			this.Length = 1.0f;
			this.Height = 1.0f;
		}

		/// <summary>
		/// Starts the component.
		/// </summary>
		protected override void OnStart(GameTime time)
		{
			this.boxShape = new BulletSharp.BoxShape(
				0.5f * this.Width,
				0.5f * this.Height,
				0.5f * this.Length);
			this.boxShape.UserObject = this;
		}


		/// <summary>
		/// Gets the Jitter shape fitting this shape component.
		/// </summary>
		/// <returns>A Jitter shape.</returns>
		protected internal override CollisionShape GetShape()
		{
			return this.boxShape;
		}

		/// <summary>
		/// Gets or sets the length of the box.
		/// </summary>
		public float Length { get; set; }

		/// <summary>
		/// Gets or sets the height of the box.
		/// </summary>
		public float Height { get; set; }

		/// <summary>
		/// Gets or sets the width of the box.
		/// </summary>
		public float Width { get; set; }
	}
}
