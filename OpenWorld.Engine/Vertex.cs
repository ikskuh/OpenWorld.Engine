using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines the model vertex type
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct Vertex : IEquatable<Vertex>
	{
		// Suppress message because a public struct can be accessed like this: Vertex.Position.x

		/// <summary>
		/// Gets or sets the position of the vertex.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public Vector3 Position;

		/// <summary>
		/// Gets or sets the normal of the vertex.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public Vector3 Normal;

		/// <summary>
		/// Gets or sets the first uv set of the vertex.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public Vector2 UV;

		/// <summary>
		/// Gets or sets the second uv set of the vertex.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public Vector2 UV2;

		/// <summary>
		/// Instantiates a new vertex
		/// </summary>
		/// <param name="position"></param>
		public Vertex(Vector3 position)
			: this(position, Vector3.Zero, Vector2.Zero, Vector2.Zero)
		{
			
		}

		/// <summary>
		/// Instantiates a new vertex
		/// </summary>
		/// <param name="position"></param>
		/// <param name="normal"></param>
		public Vertex(Vector3 position, Vector3 normal)
			: this(position, normal, Vector2.Zero, Vector2.Zero)
		{

		}

		/// <summary>
		/// Instantiates a new vertex
		/// </summary>
		/// <param name="position"></param>
		/// <param name="normal"></param>
		/// <param name="uv"></param>
		public Vertex(Vector3 position, Vector3 normal, Vector2 uv)
			: this(position, normal, uv, Vector2.Zero)
		{

		}

		/// <summary>
		/// Instantiates a new vertex
		/// </summary>
		/// <param name="position"></param>
		/// <param name="normal"></param>
		/// <param name="uv"></param>
		/// <param name="uv2"></param>
		public Vertex(Vector3 position, Vector3 normal, Vector2 uv, Vector2 uv2)
		{
			this.Position = position;
			this.Normal = normal;
			this.UV = uv;
			this.UV2 = uv2;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			if (obj.GetType() != typeof(Vertex)) return false;
			return this.Equals((Vertex)obj);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(Vertex other)
		{
			return
				this.Position == other.Position &&
				this.UV == other.UV &&
				this.UV2 == other.UV2 &&
				this.Normal == other.Normal;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return this.Position.GetHashCode() ^ this.Normal.GetHashCode() ^ this.UV.GetHashCode() ^ this.UV2.GetHashCode();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return "Position: " + this.Position.ToString() + " Normal: " + this.Normal.ToString() + " UV: " + this.UV.ToString();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator ==(Vertex left, Vertex right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator !=(Vertex left, Vertex right)
		{
			return !left.Equals(right);
		}

		/// <summary>
		/// Gets the size of a Vertex in bytes.
		/// </summary>
		public static int Size { get { return Marshal.SizeOf(typeof(Vertex)); } }
	}
}
