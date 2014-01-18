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
		Jitter.Collision.Shapes.BoxShape boxShape;

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
		protected override void OnStart()
		{
			this.boxShape = new Jitter.Collision.Shapes.BoxShape(
				this.Length,
				this.Height,
				this.Width);
		}


		/// <summary>
		/// Gets the Jitter shape fitting this shape component.
		/// </summary>
		/// <returns>A Jitter shape.</returns>
		protected internal override Jitter.Collision.Shapes.Shape GetShape()
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
