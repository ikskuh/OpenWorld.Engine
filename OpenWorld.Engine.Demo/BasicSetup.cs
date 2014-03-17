using System;
using OpenWorld.Engine;

namespace OpenWorld.Engine.Demo
{
	class BasicSetup : Game
	{
		protected override void OnLoad()
		{
			// You can use OpenGL commands in the engine.
			FrameBuffer.ClearColor = Color.SkyBlue;
		}

		protected override void OnUpdate(GameTime time)
		{
			// This method updates the game
		}

		protected override void OnDraw(GameTime time)
		{
			// Just clear the screen
			FrameBuffer.Clear();
		}
	}
}
