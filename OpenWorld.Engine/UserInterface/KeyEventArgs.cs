using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.UserInterface
{
	/// <summary>
	/// Provides data for key events.
	/// </summary>
	public sealed class KeyEventArgs : EventArgs
	{
		/// <summary>
		/// Instantiates a new KeyEventArgs.
		/// </summary>
		public KeyEventArgs()
		{
			this.Key = OpenTK.Input.Key.Unknown;
		}

		/// <summary>
		/// Instantiates a new KeyEventArgs.
		/// <param name="key">The key that triggered the event.</param>
		/// </summary>
		public KeyEventArgs(Key key)
		{
			this.Key = key;
		}

		/// <summary>
		/// Gets the key that triggered the event.
		/// </summary>
		public Key Key { get; private set; }
	}
}
