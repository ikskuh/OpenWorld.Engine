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
	[UniformPrefix("time")]
	public sealed class GameTime
	{
		/// <summary>
		/// Defines a GameTime with no time elapsed.
		/// </summary>
		public static readonly GameTime Zero = new GameTime(0.0f, 0.0f);

		internal GameTime(float totalTime, float deltaTime)
		{
			this.TotalTime = totalTime;
			this.DeltaTime = deltaTime;
		}

		/// <summary>
		/// Gets the time in seconds that passed since the start of the game.
		/// </summary>
		[Uniform("Total")]
		public float TotalTime { get; private set; }

		/// <summary>
		/// Gets the time in seconds that passed since the last frame.
		/// </summary>
		[Uniform("Delta")]
		public float DeltaTime { get; private set; }
	}
}
