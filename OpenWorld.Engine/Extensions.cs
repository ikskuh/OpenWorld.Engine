﻿using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine
{
	/// <summary>
	/// A class with extension methods.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Checks if the position is in the box.
		/// </summary>
		/// <param name="box">The box</param>
		/// <param name="pos">The position</param>
		/// <returns>True if pos is in box.</returns>
		public static bool Contains(this Box2 box, Vector2 pos)
		{
			return box.Contains(pos.X, pos.Y);
		}

		/// <summary>
		/// Checks if xy is in the box.
		/// </summary>
		/// <param name="box">The box</param>
		/// <param name="x">x-coordinate of the point</param>
		/// <param name="y">y-coordinate of the point</param>
		/// <returns>True if (x,y) is in box.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
		public static bool Contains(this Box2 box, float x, float y)
		{
			return x >= box.Left && x <= box.Right && y >= box.Top && y <= box.Bottom;
		}
	}
}