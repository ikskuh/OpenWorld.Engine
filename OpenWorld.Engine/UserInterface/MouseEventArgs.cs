using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.UserInterface
{
	/// <summary>
	/// Provides data for the mouse events.
	/// </summary>
	public sealed class MouseEventArgs : EventArgs
	{
		/// <summary>
		/// X coordinate of the mouse.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X")]
		public int X { get; internal set; }

		/// <summary>
		/// Y coordinate of the mouse.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y")]
		public int Y { get; internal set; }

		/// <summary>
		/// X movement of the mouse.
		/// </summary>
		public int DeltaX { get; internal set; }

		/// <summary>
		/// Y movement of the mouse.
		/// </summary>
		public int DeltaY { get; internal set; }

		/// <summary>
		/// True when the left mouse button is pressed.
		/// </summary>
		public bool LeftButton { get; internal set; }

		/// <summary>
		/// True when the right mouse button is pressed.
		/// </summary>
		public bool RightButton { get; internal set; }

		/// <summary>
		/// Gets the mouse button that triggered the event.
		/// </summary>
		public MouseButton Button { get; internal set; }
	}
}
