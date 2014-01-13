using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Provides data for the Update event.
	/// </summary>
	public class SceneNodeUpdateEventArgs : EventArgs
	{
		private GameTime time;

		internal SceneNodeUpdateEventArgs(GameTime time)
		{
			// TODO: Complete member initialization
			this.time = time;
		}

		/// <summary>
		/// Gets the time snapshot.
		/// </summary>
		public GameTime Time
		{
			get { return time; }
		}
	}
}
