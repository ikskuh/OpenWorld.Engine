using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines a co-routine that is executed in the update thread.
	/// </summary>
	/// <returns>CoRoutineResult or null to stop the co-routine</returns>
	public delegate IEnumerable<CoRoutineResult> CoRoutine();

	/// <summary>
	/// Defines a deferred loading routine.
	/// </summary>
	public delegate void DeferredRoutine();

	/// <summary>
	/// Represents a co-routine result that defines the behaviour of the co-routine.
	/// </summary>
	public sealed class CoRoutineResult
	{
		/// <summary>
		/// Gets a co-routine result that waits for the next frame.
		/// </summary>
		public static readonly CoRoutineResult Frame = new CoRoutineResult(0);

		// Frame counter
		internal int frameCount = 0;

		private CoRoutineResult(int frameCount)
		{
			this.frameCount = frameCount;
		}
	}
}
