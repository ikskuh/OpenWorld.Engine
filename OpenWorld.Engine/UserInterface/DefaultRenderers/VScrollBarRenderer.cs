using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.UserInterface.DefaultRenderers
{
	class VScrollBarRenderer : GuiRenderer<VScrollBar>
	{
		protected internal override void Render(VScrollBar control, Box2 bounds)
		{
			this.Engine.FillRectangle(bounds, control.BackColor);
			this.Engine.DrawRectangle(bounds, Color.Black);

			var upperButton = control.GetUpperButton().Translate(control.PointToClient(0, 0));
			this.Engine.FillRectangle(upperButton, control.FocusedElement == ScrollBarFocus.DecreaseButton ? Color.LightGray : Color.Gray);
			this.Engine.DrawRectangle(upperButton, Color.Black);

			this.Engine.DrawLine(
				upperButton.Left + 2,
				upperButton.Bottom - 3,
				upperButton.Left + upperButton.Width / 2,
				upperButton.Top + 1,
				Color.Black);
			this.Engine.DrawLine(
				upperButton.Right - 2,
				upperButton.Bottom - 3,
				upperButton.Left + upperButton.Width / 2,
				upperButton.Top + 1,
				Color.Black);
			this.Engine.DrawLine(
				upperButton.Left + 2,
				upperButton.Bottom - 3,
				upperButton.Right - 2,
				upperButton.Bottom - 3,
				Color.Black);

			var lowerButton = control.GetLowerButton().Translate(control.PointToClient(0, 0));
			this.Engine.FillRectangle(lowerButton, control.FocusedElement == ScrollBarFocus.IncreaseButton ? Color.LightGray : Color.Gray);
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

			var sliderButton = control.GetSliderKnob().Translate(control.PointToClient(0, 0));
			this.Engine.FillRectangle(sliderButton, control.FocusedElement == ScrollBarFocus.SliderKnob ? Color.LightGray : Color.Gray);
			this.Engine.DrawRectangle(sliderButton, Color.Black);
		}
	}
}
