using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines a look-at view matrix.
	/// </summary>
	public sealed class LookAt : IViewMatrixSource
	{
		/// <summary>
		/// Creates a new look-at view matrix.
		/// </summary>
		public LookAt()
			: this(Vector3.Zero, Vector3.UnitZ)
		{

		}

		/// <summary>
		/// Creates a new look-at view matrix.
		/// </summary>
		/// <param name="position">The camera position.</param>
		/// <param name="target">The camera target.</param>
		public LookAt(Vector3 position, Vector3 target)
		{
			this.Position = position;
			this.Target = target;
		}

		/// <summary>
		/// Gets the view matrix.
		/// </summary>
		/// <param name="camera"></param>
		/// <returns></returns>
		public OpenTK.Matrix4 GetViewMatrix(Camera camera)
		{
			return Matrix4.LookAt(this.Position, this.Target, Vector3.UnitY);
		}

		/// <summary>
		/// Gets or sets the camera position.
		/// </summary>
		public Vector3 Position { get; set; }

		/// <summary>
		/// Gets or sets the camera target.
		/// </summary>
		public Vector3 Target { get; set; }
	}
}
