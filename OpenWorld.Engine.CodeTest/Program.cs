using OpenTK.Graphics.OpenGL;
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

		Gui gui;

		protected override void OnLoad()
		{
			GL.ClearColor(0.3f, 0.3f, 0.3f, 1.0f);

			this.gui = new Gui();

			Form window = new CustomForm();
			window.Left = new Scalar(0.5f, -200);
			window.Top = new Scalar(0.5f, -150);
			window.Parent = this.gui;
		}
		protected override void OnUpdate(GameTime time)
		{
			// We need to give the Gui the mouse position and button states.
			// Why? You can also render a Gui system to a texture and use it in 3D space
			this.gui.SetMousePosition(
				this.Input.Mouse.X,
				this.Input.Mouse.Y);
			this.gui.SetMouseButtons(
				this.Input.Mouse[OpenTK.Input.MouseButton.Left],
				this.Input.Mouse[OpenTK.Input.MouseButton.Left]);

			// After that update the gui
			this.gui.Update(time);
		}

		protected override void OnDraw(GameTime time)
		{
			// Clear the screen, as usual
			GL.Clear(ClearBufferMask.ColorBufferBit);

			// Just draw the Gui system
			this.gui.Draw(time);
		}
	}
}
