using OpenTK.Graphics.OpenGL;
using OpenWorld.Engine.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine.Demo
{
	class GuiDemo : Game
	{
		Gui gui;

		protected override void OnLoad()
		{
			GL.ClearColor(0.3f, 0.3f, 0.3f, 1.0f);
			this.gui = new Gui();

			// Gui Controls work like System.Windows.Forms API controls:
			Button buttonA = new Button();
			buttonA.Text = "Red Screen";
			buttonA.Left = new Scalar(0.0f, 10.0f); // 10 Pixel left distance
			buttonA.Top = new Scalar(0.0f, 10.0f); // 10 Pixel top distance
			buttonA.Click += buttonA_Click;
			this.gui.Controls.Add(buttonA);

			Button buttonB = new Button();
			buttonB.Text = "Gray Screen";
			buttonB.Left = new Scalar(0.0f, 10.0f); // 10 Pixel left distance
			buttonB.Top = new Scalar(0.0f, buttonA.Top.Absolute + buttonA.Height.Absolute + 10); // 10 Pixel distance to buttonA
			buttonB.Click += buttonB_Click;
			buttonB.Parent = this.gui; // You can either add to controls or set a parent.

			Label centerScreenLabel = new Label();
			centerScreenLabel.Text = "Center";
			centerScreenLabel.ForeColor = Color.White;
			centerScreenLabel.Left = new Scalar(0.5f, 0.0f);
			centerScreenLabel.Top = new Scalar(0.5f, 0.0f);
			centerScreenLabel.TextAlign = TextAlign.MiddleCenter;
			centerScreenLabel.Parent = this.gui;
		}

		void buttonA_Click(object sender, EventArgs e)
		{
			GL.ClearColor(1.0f, 0.0f, 0.0f, 1.0f);
		}

		void buttonB_Click(object sender, EventArgs e)
		{
			GL.ClearColor(0.3f, 0.3f, 0.3f, 1.0f);
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
