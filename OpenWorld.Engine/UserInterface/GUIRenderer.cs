using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.UserInterface
{
	/// <summary>
	/// Renders a Gui element
	/// </summary>
	public abstract class GuiRenderer
	{
		/// <summary>
		/// Renders the given control
		/// </summary>
		/// <param name="control"></param>
		protected internal abstract void Render(Control control);

		/// <summary>
		/// Gets the GuiRenderEngine 
		/// </summary>
		public GuiRenderEngine Engine { get; internal set; }
	}


	/// <summary>
	/// Renders a Gui element
	/// </summary>
	public abstract class GuiRenderer<TControl> : GuiRenderer
		where TControl: Control
	{
		/// <summary>
		/// Renders the given control
		/// </summary>
		/// <param name="control"></param>
		protected internal override void Render(Control control)
		{
			if(control == null)
				throw new ArgumentNullException("control");
			TControl tControl = control as TControl;
			if(tControl == null)
				throw new ArgumentException("control is not of type TControl", "control");

			this.Render(tControl);
		}

		/// <summary>
		/// Renders the given control
		/// </summary>
		/// <param name="control"></param>
		protected internal abstract void Render(TControl control);
	}
}
