using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines a camera that is composed of two matrix sources.
	/// </summary>
	public sealed class CompositeCamera : Camera
	{
		/// <summary>
		/// Gets or sets the view matrix source.
		/// </summary>
		public IViewMatrixSource ViewMatrixSource { get; set; }

		/// <summary>
		/// Gets or sets the projection matrix source.
		/// </summary>
		public IProjectionMatrixSource ProjectionMatrixSource { get; set; }

		/// <summary>
		/// Gets the cameras view matrix.
		/// </summary>
		public override OpenTK.Matrix4 ViewMatrix
		{
			get { return this.ViewMatrixSource.GetViewMatrix(this); }
		}

		/// <summary>
		/// Gets the cameras projection matrix.
		/// </summary>
		public override OpenTK.Matrix4 ProjectionMatrix
		{
			get { return this.ProjectionMatrixSource.GetProjectionMatrix(this); }
		}
	}
}
