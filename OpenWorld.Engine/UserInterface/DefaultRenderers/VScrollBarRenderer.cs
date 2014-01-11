using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.UserInterface.DefaultRenderers
{
	class VScrollBarRenderer : GuiRenderer<VScrollBar>
	{
		protected internal override void Render(VScrollBar control)
		{
			var bounds = control.ScreenBounds;

			this.Engine.FillRectangle(bounds, control.BackColor);
			this.Engine.DrawRectangle(bounds, Color.Black);

			var upperButton = control.GetUpperButton();
			this.Engine.FillRectangle(upperButton, control.Focus == ScrollBarFocus.DecreaseButton ? Color.LightGray : Color.Gray);
			this.Engine.DrawRectangle(upperButton, Color.Black);

			this.Engine.DrawLine(
				upperButton.Left + 2,
				upperButton.Bottom - 2,
				upperButton.Left + upperButton.Width / 2,
				upperButton.Top + 2,
				Color.Black);
			this.Engine.DrawLine(
				upperButton.Right - 2,
				upperButton.Bottom - 2,
				upperButton.Left + upperButton.Width / 2,
				upperButton.Top + 2,
				Color.Black);
			this.Engine.DrawLine(
				upperButton.Left + 2,
				upperButton.Bottom - 2,
				upperButton.Right - 2,
				upperButton.Bottom - 2,
				Color.Black);

			var lowerButton = control.GetLowerButton();
			this.Engine.FillRectangle(lowerButton, control.Focus == ScrollBarFocus.IncreaseButton ? Color.LightGray : Color.Gray);
			this.Engine.DrawRectangle(lowerButton, Color.Black);

			this.Engine.DrawLine(
				lowerButton.Left + 2,
				lowerButton.Top + 2,
				lowerButton.Left + lowerButton.Width / 2,
				lowerButton.Bottom- 2,
				Color.Black);
			this.Engine.DrawLine(
				lowerButton.Right - 2,
				lowerButton.Top + 2,
				lowerButton.Left + lowerButton.Width / 2,
				lowerButton.Bottom- 2,
				Color.Black);
			this.Engine.DrawLine(
				lowerButton.Left + 2,
				lowerButton.Top + 2,
				lowerButton.Right - 2,
				lowerButton.Top + 2,
				Color.Black);

			var sliderButton = control.GetSliderKnob();
			this.Engine.FillRectangle(sliderButton, control.Focus == ScrollBarFocus.SliderKnob ? Color.LightGray : Color.Gray);
			this.Engine.DrawRectangle(sliderButton, Color.Black);
		}
	}
}
