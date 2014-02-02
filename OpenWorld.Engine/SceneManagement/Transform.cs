using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Defines a transformation.
	/// </summary>
	public class Transform
	{
		private readonly SceneNode parent;
		private Matrix4 matrix;

		/// <summary>
		/// Creates a new transform.
		/// </summary>
		internal Transform(SceneNode node)
		{
			this.parent = node;
			this.matrix = Matrix4.Identity;
		}

		/// <summary>
		/// Translates the transform.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public void Translate(float x, float y, float z)
		{
			this.Translate(new Vector3(x, y, z));
		}

		/// <summary>
		/// Translates the transform.
		/// </summary>
		/// <param name="translation"></param>
		public void Translate(Vector3 translation)
		{
			this.matrix = Matrix4.CreateTranslation(translation) * this.matrix;
		}

		/// <summary>
		/// Rotates the transform.
		/// </summary>
		/// <param name="x">Rotation around the x axis in degrees</param>
		/// <param name="y">Rotation around the y axis in degrees</param>
		/// <param name="z">Rotation around the z axis in degrees</param>
		public void Rotate(float x, float y, float z)
		{
			var rot =
				Matrix4.CreateFromAxisAngle(Vector3.UnitX, GameMath.ToRadians(x)) *
				Matrix4.CreateFromAxisAngle(Vector3.UnitY, GameMath.ToRadians(y)) *
				Matrix4.CreateFromAxisAngle(Vector3.UnitZ, GameMath.ToRadians(z));
			this.matrix = rot * this.matrix;
		}

		/// <summary>
		/// Transforms the rotation to let the node target the given position.
		/// </summary>
		/// <param name="target">The local(!) target to look at.</param>
		public void LookAt(Vector3 target)
		{
			this.matrix = Matrix4.Invert(Matrix4.LookAt(this.LocalPosition, target, Vector3.UnitY));
		}

		/// <summary>
		/// Returns the global transformation matrix.
		/// </summary>
		/// <returns></returns>
		public Matrix4 GetGlobalMatrix()
		{
			Matrix4 matrix = Matrix4.Identity;
			var node = this.parent;
			while(node != null)
			{
				matrix = matrix * node.Transform.GetMatrix();
				node = node.Parent;
			}
			return matrix;
		}

		/// <summary>
		/// Returns the local transformation matrix.
		/// </summary>
		/// <returns></returns>
		public Matrix4 GetMatrix()
		{
			return this.matrix;
		}

		/// <summary>
		/// Sets the local transformation matrix.
		/// </summary>
		/// <param name="matrix4"></param>
		public void SetMatrix(Matrix4 matrix4)
		{
			this.matrix = matrix4;
		}

		/// <summary>
		/// Gets or sets the local position of the transform.
		/// </summary>
		public Vector3 LocalPosition
		{
			get { return this.matrix.Row3.Xyz; }
			set { this.matrix.Row3.Xyz = value; }
		}

		/// <summary>
		/// Gets or sets the local position of the transform.
		/// </summary>
		public Vector3 WorldPosition
		{
			get { return this.GetGlobalMatrix().Row3.Xyz; }
		}

		/// <summary>
		/// Gets the global forward direction of the node.
		/// </summary>
		public Vector3 Forward
		{
			get
			{
				return -this.GetGlobalMatrix().Row2.Xyz;
			}
		}
	}
}
