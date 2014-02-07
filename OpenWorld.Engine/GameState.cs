using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines a game state.
	/// </summary>
	public class GameState
	{
		internal void Enter()
		{
			this.OnEnter();
		}

		internal void Update(GameTime time)
		{
			this.OnUpdate(time);
		}

		internal void Draw(GameTime time)
		{
			this.OnDraw(time);
		}

		internal void Leave()
		{
			this.OnLeave();
		}

		/// <summary>
		/// Enters the game state.
		/// </summary>
		protected virtual void OnEnter() { }

		/// <summary>
		/// Updates the game state.
		/// </summary>
		/// <param name="time"></param>
		protected virtual void OnUpdate(GameTime time) { }

		/// <summary>
		/// Draws the game state.
		/// </summary>
		/// <param name="time"></param>
		protected virtual void OnDraw(GameTime time) { }

		/// <summary>
		/// Leaves the game state.
		/// </summary>
		protected virtual void OnLeave() { }

		/// <summary>
		/// Gets the game that this game state uses.
		/// </summary>
		public Game Game { get; internal set; }
	}
}
