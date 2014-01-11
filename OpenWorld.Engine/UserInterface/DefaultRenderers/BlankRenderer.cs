using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine.UserInterface.DefaultRenderers
{
	/// <summary>
	/// Represents a GuiRenderer that renders nothing.
	/// </summary>
	public sealed class BlankRenderer : GuiRenderer
	{
		/// <summary>
		/// Renders the control.
		/// </summary>
		/// <param name="control">The control to be rendered.</param>
		protected internal override void Render(Control control)
		{
			
		}
	}
}
