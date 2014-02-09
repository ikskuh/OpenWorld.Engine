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
		private Matrix4 localMatrix;


		/// <summary>
		/// Creates a new transform.
		/// </summary>
		internal Transform(SceneNode node)
		{
			this.parent = node;
			this.localMatrix = Matrix4.Identity;
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
			this.localMatrix = Matrix4.CreateTranslation(translation) * this.localMatrix;
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
			this.localMatrix = this.localMatrix * rot;
		}

		/// <summary>
		/// Transforms the rotation to let the node target the given position.
		/// </summary>
		public void LookAt(Vector3 target)
		{
			this.WorldTransform = Matrix4.Invert(Matrix4.LookAt(this.LocalPosition, target, Vector3.UnitY));
		}

		/// <summary>
		/// Gets or sets the local position of the transform.
		/// </summary>
		public Vector3 LocalPosition
		{
			get { return this.localMatrix.Row3.Xyz; }
			set { this.localMatrix.Row3.Xyz = value; }
		}

		/// <summary>
		/// Gets or sets the local position of the transform.
		/// </summary>
		public Vector3 WorldPosition
		{
			get { return this.WorldTransform.Row3.Xyz; }
		}

		/// <summary>
		/// Gets the global forward direction of the node.
		/// </summary>
		public Vector3 Forward
		{
			get
			{
				return -this.WorldTransform.Row2.Xyz;
			}
		}

		/// <summary>
		/// Gets or sets the local transform.
		/// </summary>
		public Matrix4 LocalTransform
		{
			get { return this.localMatrix; }
			set { this.localMatrix = value; }
		}

		/// <summary>
		/// Gets or sets the global transform.
		/// </summary>
		public Matrix4 WorldTransform
		{
			get
			{
				var node = this.parent;
				if (node.Parent == null)
					return node.Transform.localMatrix;
				else
					return node.Transform.localMatrix * node.Parent.Transform.WorldTransform;
			}
			set
			{
				var node = this.parent;
				if (node.Parent == null)
					node.Transform.localMatrix = value;
				else
					node.Transform.localMatrix = value * Matrix4.Invert(node.Parent.Transform.WorldTransform);
				if (float.IsNaN(node.Transform.localMatrix.Row3.X))
					return;
			}
		}
	}
}
