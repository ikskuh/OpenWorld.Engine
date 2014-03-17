using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Provides a set of utility functions.
	/// </summary>
	public sealed class GameUtilities
	{
		private readonly Game game;

		internal GameUtilities(Game game)
		{
			this.game = game;
		}


		/// <summary>
		/// Draws a textured quad on the screen.
		/// </summary>
		/// <param name="box2">Area to be drawn in.</param>
		/// <param name="texture2D">Texture to be drawn.</param>
		public void DrawQuad(Box2 box2, Texture2D texture2D)
		{

		}
	}
}
