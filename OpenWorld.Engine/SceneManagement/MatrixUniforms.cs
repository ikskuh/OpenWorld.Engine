using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Defines the basic transformation matrices.
	/// </summary>
	[UniformPrefix("mat")]
	public sealed class MatrixUniforms
	{
		/// <summary>
		/// Gets or sets the world matrix.
		/// </summary>
		[Uniform("World")]
		public Matrix4 World { get; set; }

		/// <summary>
		/// Gets or sets the view matrix.
		/// </summary>
		[Uniform("View")]
		public Matrix4 View { get; set; }

		/// <summary>
		/// Gets or sets the projection matrix.
		/// </summary>
		[Uniform("Projection")]
		public Matrix4 Projection { get; set; }
	}
}
