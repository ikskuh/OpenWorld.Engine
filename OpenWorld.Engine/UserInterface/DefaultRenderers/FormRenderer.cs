using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.UserInterface.DefaultRenderers
{
	class FormRenderer : GuiRenderer<Form>
	{
		protected internal override void Render(Form control)
		{
			var bounds = control.ScreenBounds;
			var clientBounds = control.ScreenClientBounds;

			this.Engine.FillRectangle(bounds, Color.Silver);

			this.Engine.DrawRectangle(bounds, Color.Black);
			this.Engine.DrawLine(bounds.Left, bounds.Top + 20, bounds.Right, bounds.Top + 20, Color.Black);

			this.Engine.DrawString(control.Text, control.Font, bounds.Left + bounds.Width / 2, bounds.Top + 10, control.ForeColor, TextAlign.MiddleCenter);

			this.Engine.FillRectangle(clientBounds, control.BackColor);
		}
	}
}
