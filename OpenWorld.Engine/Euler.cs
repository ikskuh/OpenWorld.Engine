using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Represents an euler angle.
	/// </summary>
	public struct Euler : IEquatable<Euler>
	{
		/// <summary>
		/// Gets a euler angle with no rotation.
		/// </summary>
		public static Euler Zero { get { return new Euler(0, 0, 0); } }

		private float pan;
		private float tilt;
		private float roll;


		/// <summary>
		/// Instantiates a new euler angle.
		/// </summary>
		/// <param name="pan"></param>
		/// <param name="tilt"></param>
		public Euler(float pan, float tilt)
			: this(pan, tilt, 0)
		{

		}

		/// <summary>
		/// Instantiates a new euler angle.
		/// </summary>
		/// <param name="pan"></param>
		/// <param name="tilt"></param>
		/// <param name="roll"></param>
		public Euler(float pan, float tilt, float roll)
		{
			this.pan = pan;
			this.tilt = tilt;
			this.roll = roll;
		}

		#region Methods

		/// <summary>
		/// Converts the angle into a direction.
		/// </summary>
		/// <returns>Direction of the angle.</returns>
		public Vector3 ToDirection()
		{
			var dir = new Vector3();
			dir.X = GameMath.Sin(this.pan) * GameMath.Cos(this.tilt);
			dir.Y = GameMath.Sin(this.tilt);
			dir.Z = GameMath.Cos(this.pan) * GameMath.Cos(this.tilt);
			return dir;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			if (obj.GetType() != typeof(Euler)) return false;
			return this.Equals((Euler)obj);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(Euler other)
		{
			return this == other;
		}

		/// <summary>
		/// Checks for equality with a tolerance.
		/// </summary>
		/// <param name="other">The Euler angle.</param>
		/// <param name="tolerance">The tolerance per component.</param>
		/// <returns></returns>
		public bool Equals(Euler other, float tolerance)
		{
			if (Math.Abs(this.pan - other.pan) > tolerance)
				return false;
			if (Math.Abs(this.tilt - other.tilt) > tolerance)
				return false;
			if (Math.Abs(this.roll - other.roll) > tolerance)
				return false;
			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return this.pan.GetHashCode() ^ this.tilt.GetHashCode() ^ this.roll.GetHashCode();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0} {1} {2}", this.pan, this.tilt, this.roll);
		}

		/// <summary>
		/// Gets or sets the rotation around the y-axis.
		/// </summary>
		public float Pan
		{
			get { return pan; }
			set { pan = value; }
		}

		/// <summary>
		/// Gets or sets the rotation around the z-axis.
		/// </summary>
		public float Tilt
		{
			get { return tilt; }
			set { tilt = value; }
		}

		/// <summary>
		/// Gets or sets the rotation around the x-axis.
		/// </summary>
		public float Roll
		{
			get { return roll; }
			set { roll = value; }
		}

		#endregion

		#region Operators

		/// <summary>
		/// 
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator ==(Euler left, Euler right)
		{
			if (left.Pan != right.Pan)
				return false;
			if (left.Tilt != right.Tilt)
				return false;
			if (left.Roll != right.Roll)
				return false;
			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator !=(Euler left, Euler right)
		{
			return !(left == right);
		}

		#endregion

		#region Static Part

		/// <summary>
		/// Converts an angle to a quaternion.
		/// </summary>
		/// <param name="pan"></param>
		/// <param name="tilt"></param>
		/// <returns></returns>
		public static Quaternion ToQuaternion(float pan, float tilt)
		{
			return Euler.ToQuaternion(pan, tilt, 0);
		}

		/// <summary>
		/// Converts an angle to a quaternion.
		/// </summary>
		/// <param name="pan"></param>
		/// <param name="tilt"></param>
		/// <param name="roll"></param>
		/// <returns></returns>
		public static Quaternion ToQuaternion(float pan, float tilt, float roll)
		{
			return
				Quaternion.FromAxisAngle(Vector3.UnitY, GameMath.ToRadians(pan)) *
				Quaternion.FromAxisAngle(Vector3.UnitX, GameMath.ToRadians(tilt)) *
				Quaternion.FromAxisAngle(Vector3.UnitZ, GameMath.ToRadians(roll));
		}

		/// <summary>
		/// Converts an angle to a rotation matrix.
		/// </summary>
		/// <param name="euler"></param>
		/// <returns></returns>
		public static Matrix4 ToMatrix(Euler euler)
		{
			return Euler.ToMatrix(euler.Pan, euler.Tilt, euler.Roll);
		}

		/// <summary>
		/// Converts an angle to a rotation matrix.
		/// </summary>
		/// <param name="pan"></param>
		/// <param name="tilt"></param>
		/// <returns></returns>
		public static Matrix4 ToMatrix(float pan, float tilt)
		{
			return Euler.ToMatrix(pan, tilt, 0);
		}

		/// <summary>
		/// Converts an angle to a rotation matrix.
		/// </summary>
		/// <param name="pan"></param>
		/// <param name="tilt"></param>
		/// <param name="roll"></param>
		/// <returns></returns>
		public static Matrix4 ToMatrix(float pan, float tilt, float roll)
		{
			return Matrix4.Rotate(Euler.ToQuaternion(pan, tilt, roll));
		}

		#endregion
	}
}
