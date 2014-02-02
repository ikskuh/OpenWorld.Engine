using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines a host for co-routines.
	/// </summary>
	public sealed class CoRoutineHost
	{
		class CoRoutineState
		{
			public IEnumerator<CoRoutineResult> Executor { get; set; }

			public bool IsFinished { get; set; }
		}

		List<CoRoutineState> runningRoutines = new List<CoRoutineState>();

		/// <summary>
		/// Starts a new co-routine
		/// </summary>
		/// <param name="routine">The routine to start.</param>
		public void Start(CoRoutine routine)
		{
			if (routine == null)
				return;

			var enumerable = routine();
			if (enumerable == null)
				return;

			this.runningRoutines.Add(new CoRoutineState()
			{
				IsFinished = false,
				Executor = enumerable.GetEnumerator()
			});
		}

		/// <summary>
		/// Executes the co-routines for one tick.
		/// </summary>
		public void Step()
		{
			foreach(var host in this.runningRoutines)
			{
				host.IsFinished = !host.Executor.MoveNext();
			}
			this.runningRoutines.RemoveAll((r) => r.IsFinished);
		}
	}
}
