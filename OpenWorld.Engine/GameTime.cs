using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Represents a game timing.
	/// </summary>
	public sealed class GameTime
	{
		internal GameTime()
		{

		}

		/// <summary>
		/// Gets the time in seconds that passed since the start of the game.
		/// </summary>
		public float TotalTime { get; internal set; }

		/// <summary>
		/// Gets the time in seconds that passed since the last frame.
		/// </summary>
		public float DeltaTime { get; internal set; }
	}
}
