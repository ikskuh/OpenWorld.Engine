using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine.UserInterface
{
	/// <summary>
	/// Represents a rectangle with Scalar as coordinate type.
	/// </summary>
	public struct ScalarRectangle : IEquatable<ScalarRectangle>
	{
		/// <summary>
		/// Gets a rectangle that fills the whole screen.
		/// </summary>
		public static ScalarRectangle FullScreen
		{
			get
			{
				return new ScalarRectangle(new Scalar(0.0f, 0.0f), new Scalar(0.0f, 0.0f),
				new Scalar(1.0f, 0.0f), new Scalar(1.0f, 0.0f));
			}
		}

		/// <summary>
		/// Left border of the rectangle.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public Scalar Left;

		/// <summary>
		/// Top border of the rectangle.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public Scalar Top;

		/// <summary>
		/// Width of the rectangle.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public Scalar Width;

		/// <summary>
		/// Height of the rectangle.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public Scalar Height;

		/// <summary>
		/// Instantiates a new ScalarRectangle.
		/// </summary>
		/// <param name="left">Left border</param>
		/// <param name="top">Top border</param>
		/// <param name="width">Width</param>
		/// <param name="height">Height</param>
		public ScalarRectangle(Scalar left, Scalar top, Scalar width, Scalar height)
		{
			this.Left = left;
			this.Top = top;
			this.Width = width;
			this.Height = height;
		}

		/// <summary>
		/// Transforms the ScalarRectangle to a Box2.
		/// </summary>
		/// <param name="container">The container that defines the bounds for the relative values and offsets.</param>
		/// <returns>Transformed rectangle.</returns>
		public Box2 Transform(Box2 container)
		{
			var box = new Box2();

			box.Left = container.Left + container.Width * this.Left.Relative + this.Left.Absolute;
			box.Top = container.Top + container.Height * this.Top.Relative + this.Top.Absolute;

			box.Right = box.Left + container.Width * this.Width.Relative + this.Width.Absolute;
			box.Bottom = box.Top + container.Height * this.Height.Relative + this.Height.Absolute;

			return box;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			if (obj.GetType() != typeof(ScalarRectangle)) return false;
			return this.Equals((ScalarRectangle)obj);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(ScalarRectangle other)
		{
			return
				this.Left == other.Left &&
				this.Top == other.Top &&
				this.Width == other.Width &&
				this.Height == other.Height;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return this.Left.GetHashCode() ^ this.Top.GetHashCode() ^this.Width.GetHashCode() ^ this.Height.GetHashCode();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return "Left: " + this.Left.ToString() + " Top: " + this.Top.ToString() + " Width: " + this.Width.ToString() + " Height: " + this.Height.ToString();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator ==(ScalarRectangle left, ScalarRectangle right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator !=(ScalarRectangle left, ScalarRectangle right)
		{
			return !left.Equals(right);
		}
	}
}
