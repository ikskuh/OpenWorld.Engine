using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine.UserInterface
{
	/// <summary>
	/// A scalar value. Consists of a relative value and an absolute value.
	/// </summary>
	public struct Scalar : IEquatable<Scalar>
	{
		float relative, absolute;

		/// <summary>
		/// Creates a new scalar.
		/// </summary>
		/// <param name="relative">Relative value.</param>
		/// <param name="absolute">Absolute value.</param>
		public Scalar(float relative, float absolute)
		{
			this.relative = relative;
			this.absolute = absolute;
		}

		/// <summary>
		/// Gets or sets the relative value.
		/// </summary>
		public float Relative
		{
			get { return relative; }
			set { relative = value; }
		}

		/// <summary>
		/// Gets or sets the absolute value.
		/// </summary>
		public float Absolute
		{
			get { return absolute; }
			set { absolute = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			if(obj.GetType() != typeof(Scalar)) return false;
			return this.Equals((Scalar)obj);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(Scalar other)
		{
			return this.relative == other.relative && this.absolute == other.absolute;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator ==(Scalar left, Scalar right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator !=(Scalar left, Scalar right)
		{
			return !left.Equals(right);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return this.relative.GetHashCode() ^ this.absolute.GetHashCode();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			if(this.absolute < 0)
				return this.relative.ToString(System.Globalization.CultureInfo.InvariantCulture) + " - " + (-this.absolute).ToString(System.Globalization.CultureInfo.InvariantCulture);
			else
				return this.relative.ToString(System.Globalization.CultureInfo.InvariantCulture) + " + " + this.absolute.ToString(System.Globalization.CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="left"></param>
		/// <param name="ente"></param>
		/// <returns></returns>
		public static Scalar operator +(Scalar left, Scalar ente)
		{
			return Add(left, ente);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static Scalar operator -(Scalar left, Scalar right)
		{
			return Subtract(left, right);
		}

		/// <summary>
		/// Adds two scalar values
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static Scalar Add(Scalar left, Scalar right)
		{
			return new Scalar(left.relative + right.relative, left.absolute + right.absolute);
		}

		/// <summary>
		/// Subtracts two scalar values
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static Scalar Subtract(Scalar left, Scalar right)
		{
			return new Scalar(left.relative - right.relative, left.absolute - right.absolute);
		}
	}	
}
