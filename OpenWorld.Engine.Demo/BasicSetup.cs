using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine.Demo
{
	class BasicSetup : Game
	{
		protected override void OnLoad()
		{
			// You can use OpenGL commands in the engine.
			GL.ClearColor(0.2f, 0.2f, 0.9f, 1.0f);
		}

		protected override void OnUpdate(GameTime time)
		{
			// This method updates the game
		}

		protected override void OnDraw(GameTime time)
		{
			// Just clear the screen, back buffer swapping is done by the engine.
			GL.Clear(ClearBufferMask.ColorBufferBit);
		}
	}
}
