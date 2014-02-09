using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	class UpdateEventArgs : EventArgs
	{
		public UpdateEventArgs(GameTime snapshot)
		{
			this.Time = snapshot;
		}

		public GameTime Time { get; private set; }
	}
}
