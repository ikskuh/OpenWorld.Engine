using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine.UserInterface.DefaultRenderers
{
	class ButtonRenderer : GuiRenderer<Button>
	{
		protected internal override void Render(Button button)
		{
			if (button == null)
				throw new ArgumentNullException("control");

			var bounds = button.ScreenBounds;

			Color backColor = button.BackColor;
			switch(button.State)
			{
				case ButtonState.Hovered:
					backColor *= 1.2f;
					break;
				case ButtonState.Pressed:
					backColor *= 0.9f;
					break;
			}

			this.Engine.FillRectangle(bounds, backColor);
			this.Engine.DrawRectangle(bounds, Color.Black);

			if (button.IsFocused)
			{
				this.Engine.DrawRectangle(bounds.Left + 2, bounds.Top + 2, bounds.Width - 4, bounds.Height - 4, Color.LightGray);
			}

			float x = -100;
			float y = -100;
			float padding = 4;
			switch (button.TextAlign)
			{
				case TextAlign.TopLeft:
					x = padding;
					y = 0;
					break;
				case TextAlign.TopCenter:
					x = 0.5f * bounds.Width;
					y = 0;
					break;
				case TextAlign.TopRight:
					x = bounds.Width - padding;
					y = 0;
					break;
				case TextAlign.MiddleLeft:
					x = padding;
					y = 0.5f * bounds.Height - 1;
					break;
				case TextAlign.MiddleCenter:
					x = 0.5f * bounds.Width;
					y = 0.5f * bounds.Height - 1;
					break;
				case TextAlign.MiddleRight:
					x = bounds.Width - 2;
					y = 0.5f * bounds.Height - 1;
					break;
				case TextAlign.BottomLeft:
					x = 2;
					y = bounds.Height - padding;
					break;
				case TextAlign.BottomCenter:
					x = 0.5f * bounds.Width;
					y = bounds.Height - padding;
					break;
				case TextAlign.BottomRight:
					x = bounds.Width - padding;
					y = bounds.Height - padding;
					break;
			}

			this.Engine.DrawString(
				button.Text,
				button.Font, 
				bounds.Left + x,
				bounds.Top + y,
				Color.Black,
				button.TextAlign);
		}
	}
}
