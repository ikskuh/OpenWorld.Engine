using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.UserInterface.DefaultRenderers
{
	class FormRenderer : GuiRenderer<Form>
	{
		protected internal override void Render(Form control, Box2 bounds)
		{
			var clientBounds = control.ScreenClientBounds.Translate(-control.PointToScreen(0, 0));

			this.Engine.FillRectangle(bounds, Color.Silver);

			this.Engine.DrawRectangle(bounds, Color.Black);
			this.Engine.DrawLine(bounds.Left, bounds.Top + 20, bounds.Right, bounds.Top + 20, Color.Black);

			this.Engine.DrawString(control.Text, control.Font, bounds.Left + bounds.Width / 2, bounds.Top + 8, control.ForeColor, TextAlign.MiddleCenter);

			this.Engine.FillRectangle(clientBounds, control.BackColor);
		}
	}
}
