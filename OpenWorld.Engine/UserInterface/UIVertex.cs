using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.UserInterface
{
	/// <summary>
	/// Represents a vertex used in UI rendering.
	/// </summary>
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct UIVertex : IEquatable<UIVertex>
	{
		/// <summary>
		/// Gets or sets the position of the vertex.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public Vector2 Position;

		/// <summary>
		/// Gets or sets the color of the vertex.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public Color Color;

		/// <summary>
		/// Gets or sets the uv of the vertex.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public Vector2 UV;

		/// <summary>
		/// Compares the vertex to an object.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			if (obj.GetType() != typeof(UIVertex)) return false;
			return this.Equals((UIVertex)obj);
		}

		/// <summary>
		/// Compares the vertex to another vertex.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(UIVertex other)
		{
			return
				this.Position == other.Position &&
				this.UV == other.UV &&
				this.Color == other.Color;
		}

		/// <summary>
		/// Gets the hash code for the vertex.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return this.Position.GetHashCode() ^ this.Color.GetHashCode() ^ this.UV.GetHashCode();
		}

		/// <summary>
		/// Converts the vertex to a string.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return "Position: " + this.Position.ToString() + " Color: " + this.Color.ToString() + " UV: " + this.UV.ToString();
		}

		/// <summary>
		/// Compares for equality.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator ==(UIVertex left, UIVertex right)
		{
			return left.Equals(right);
		}
		/// <summary>
		/// Compares for inequality.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator !=(UIVertex left, UIVertex right)
		{
			return !left.Equals(right);
		}

		/// <summary>
		/// Gets the byte size of a vertex.
		/// </summary>
		public static int Size { get { return System.Runtime.InteropServices.Marshal.SizeOf(typeof(UIVertex)); } }
	}
}
