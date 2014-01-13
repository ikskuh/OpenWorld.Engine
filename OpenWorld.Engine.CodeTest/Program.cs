using OpenTK.Graphics.OpenGL;
using OpenWorld.Engine.SceneManagement;
using OpenWorld.Engine.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine.CodeTest
{
	class Program : Game
	{
		static void Main(string[] args)
		{
			using(Window window = new Window(1024, 768))
			{
				window.Game = new Program();
				window.Run(60, 60);
			}
		}

		Scene scene;

		protected override void OnLoad()
		{
			GL.ClearColor(0.3f, 0.3f, 0.3f, 1.0f);

			this.scene = new Scene();
		}
		protected override void OnUpdate(GameTime time)
		{
			this.scene.Update(time);
		}

		protected override void OnDraw(GameTime time)
		{
			// Clear the screen, as usual
			GL.Clear(ClearBufferMask.ColorBufferBit);

			this.scene.Draw(time);
		}
	}
}
