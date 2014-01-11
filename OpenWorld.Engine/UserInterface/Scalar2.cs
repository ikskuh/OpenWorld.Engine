using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine.UserInterface
{
	/// <summary>
	/// Represents a 2-dimensional scalar.
	/// </summary>
	public struct Scalar2 : IEquatable<Scalar2>
	{
		/// <summary>
		/// Gets or sets the x component.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public Scalar X;

		/// <summary>
		/// Gets or sets the y component.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public Scalar Y;

		/// <summary>
		/// Instantiates a new Scalar2.
		/// </summary>
		/// <param name="x">The x component.</param>
		/// <param name="y">The y component.</param>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public Scalar2(Scalar x, Scalar y)
		{
			this.X = x;
			this.Y = y;
		}

		/// <summary>
		/// Checks for equality.
		/// </summary>
		/// <param name="obj">The object to be compared.</param>
		/// <returns>True if obj is equal to the Scalar2.</returns>
		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			if (obj.GetType() != typeof(Scalar2)) return false;
			return this.Equals((Scalar2)obj);
		}

		/// <summary>
		/// Checks for equality.
		/// </summary>
		/// <param name="other">The object to be compared.</param>
		/// <returns>True if other is equal to the Scalar2.</returns>
		public bool Equals(Scalar2 other)
		{
			return
				this.X == other.X &&
				this.Y == other.Y;
		}

		/// <summary>
		/// Returns the hash code of the Scalar2.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ this.Y.GetHashCode();
		}

		/// <summary>
		/// Converts the Scalar2 to a string.
		/// </summary>
		/// <returns>String representing the value.</returns>
		public override string ToString()
		{
			return this.X.ToString() + " " + this.Y.ToString();
		}

		/// <summary>
		/// Compares for equality.
		/// </summary>
		/// <param name="left">First comparison object.</param>
		/// <param name="right">Second comparison object.</param>
		/// <returns>True if left equals right.</returns>
		public static bool operator ==(Scalar2 left, Scalar2 right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// Compares for inequality.
		/// </summary>
		/// <param name="left">First comparison object.</param>
		/// <param name="right">Second comparison object.</param>
		/// <returns>True if left does not equals right.</returns>
		public static bool operator !=(Scalar2 left, Scalar2 right)
		{
			return !left.Equals(right);
		}
	}
}
