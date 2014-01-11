using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine.UserInterface.DefaultRenderers
{
	/// <summary>
	/// A renderer that renders controls.
	/// </summary>
	sealed class ControlRenderer : GuiRenderer
	{
		/// <summary>
		/// Draws the control
		/// </summary>
		/// <param name="control"></param>
		protected internal override void Render(Control control)
		{
			if (control == null)
				throw new ArgumentNullException("control");
			Box2 bounds = control.ScreenBounds;

			this.Engine.FillRectangle(bounds, control.BackColor);
			this.Engine.DrawRectangle(bounds, Color.Black);

			this.Engine.DrawLine(bounds.Left, bounds.Top, bounds.Right, bounds.Bottom, control.ForeColor, 2.0f);
			this.Engine.DrawLine(bounds.Right, bounds.Top, bounds.Left, bounds.Bottom, control.ForeColor, 2.0f);
		}
	}
}
