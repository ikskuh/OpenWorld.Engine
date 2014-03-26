using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines a 2d box with integer sizes.
	/// </summary>
	public struct Box2i
	{
		int left;
		int top;
		int right;
		int bottom;

		/// <summary>
		/// Creates a new Box2i
		/// </summary>
		/// <param name="left"></param>
		/// <param name="top"></param>
		/// <param name="right"></param>
		/// <param name="bottom"></param>
		public Box2i(int left, int top, int right, int bottom)
		{
			this.left = left;
			this.top = top;
			this.right = right;
			this.bottom = bottom;
		}

		/// <summary>
		/// Gets or sets distance to left border.
		/// </summary>
		public int Left
		{
			get { return left; }
			set { left = value; }
		}

		/// <summary>
		/// Gets or sets distance to top border.
		/// </summary>
		public int Top
		{
			get { return top; }
			set { top = value; }
		}

		/// <summary>
		/// Gets or sets distance to right border.
		/// </summary>
		public int Right
		{
			get { return right; }
			set { right = value; }
		}

		/// <summary>
		/// Gets or sets distance to bottom border.
		/// </summary>
		public int Bottom
		{
			get { return bottom; }
			set { bottom = value; }
		}

		/// <summary>
		/// Gets the width.
		/// </summary>
		public int Width { get { return this.right - this.left; } }

		/// <summary>
		/// Gets the height.
		/// </summary>
		public int Height { get { return this.bottom - this.top; } }
	}
}
