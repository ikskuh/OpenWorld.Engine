using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.UserInterface.DefaultRenderers
{
	class PictureBoxRenderer : GuiRenderer<PictureBox>
	{
		protected internal override void Render(PictureBox control, OpenTK.Box2 localBounds)
		{
			if(control.Image == null)
				return;
			this.Engine.FillRectangle(localBounds, Color.White, control.Image);
		}
	}
}
