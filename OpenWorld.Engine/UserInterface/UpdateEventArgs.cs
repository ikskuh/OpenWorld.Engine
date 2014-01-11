using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.UserInterface
{
	/// <summary>
	/// Provides datan for the Update event.
	/// </summary>
	public sealed class UpdateEventArgs : EventArgs
	{
		/// <summary>
		/// Instantiates a new UpdateEventArgs.
		/// </summary>
		/// <param name="time"></param>
		public UpdateEventArgs(GameTime time)
		{
			this.Time = time;
		}

		/// <summary>
		/// Gets the game time.
		/// </summary>
		public GameTime Time { get; private set; }
	}
}
