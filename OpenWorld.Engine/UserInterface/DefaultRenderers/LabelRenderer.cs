using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine.UserInterface.DefaultRenderers
{
	class LabelRenderer : GuiRenderer<Label>
	{
		protected internal override void Render(Label label, Box2 bounds)
		{
			if (label == null)
				throw new ArgumentNullException("control");
			this.Engine.DrawString(label.Text, label.Font, bounds.Left, bounds.Top, label.ForeColor, label.TextAlign);
		}
	}
}
