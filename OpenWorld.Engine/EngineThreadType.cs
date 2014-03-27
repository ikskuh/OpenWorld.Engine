using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines the type for an engine thread.
	/// </summary>
	public enum EngineThreadType
	{
		/// <summary>
		/// The thread is not an engine thread.
		/// </summary>
		None,
		/// <summary>
		/// The thread updates the game.
		/// </summary>
		Update,
		/// <summary>
		/// The thread renders the game.
		/// </summary>
		Render,
		/// <summary>
		/// The thread defers routine calls.
		/// </summary>
		Deferral
	}
}
