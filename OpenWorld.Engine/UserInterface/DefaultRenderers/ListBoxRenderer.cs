using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.UserInterface.DefaultRenderers
{
	class ListBoxRenderer : GuiRenderer<ListBox>
	{
		protected internal override void Render(ListBox control, Box2 bounds)
		{
			bounds.Right -= 20.0f; // Subtract scroll bar

			this.Engine.FillRectangle(bounds, control.BackColor);
			this.Engine.DrawRectangle(bounds, Color.Black);

			float offset = control.GetScrollOffset() -2;
			foreach (var item in control.Items)
			{
				if (item != null)
				{
					this.Engine.DrawString(
						item.ToString(),
						control.Font,
						2,
						control.ItemHeight / 2 + offset,
						control.ForeColor,
						TextAlign.MiddleLeft);
				}
				offset += control.ItemHeight;
			}
		}
	}
}
