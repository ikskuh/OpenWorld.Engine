using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines an object shader for 3d objects.
	/// </summary>
	public class ObjectShader : Shader
	{
		/// <summary>
		/// Applies the shader parameters.
		/// </summary>
		protected override void OnApply()
		{
			this.SetUniform("World", this.World, false);
			this.SetUniform("View", this.View, false);
			this.SetUniform("Projection", this.Projection, false);

			base.OnApply();
		}

		/// <summary>
		/// Gets or sets the world transformation matrix.
		/// <remarks>Shader uniform: mat4 World</remarks>
		/// </summary>
		public Matrix4 World { get; set; }

		/// <summary>
		/// Gets or sets the view matrix.
		/// <remarks>Shader uniform: mat4 View</remarks>
		/// </summary>
		public Matrix4 View { get; set; }

		/// <summary>
		/// Gets or sets the projection matrix.
		/// <remarks>Shader uniform: mat4 Projection</remarks>
		/// </summary>
		public Matrix4 Projection { get; set; }
	}
}
