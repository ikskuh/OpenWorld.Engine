using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine.UserInterface
{
	/// <summary>
	/// Defines a button.
	/// </summary>
	[Renderer(typeof(OpenWorld.Engine.UserInterface.DefaultRenderers.ButtonRenderer))]
	public class Button : Control
	{
		/// <summary>
		/// Instantiates a new button.
		/// </summary>
		public Button()
		{
			this.State = ButtonState.None;
			this.Width = new Scalar(0.0f, 80.0f);
			this.Height = new Scalar(0.0f, 25.0f);
		}

		/// <summary>
		/// Gets called when the mouse enters the button.
		/// </summary>
		/// <param name="e">Event Args</param>
		protected override void OnMouseEnter(MouseEventArgs e)
		{
			if (e == null)
				throw new ArgumentNullException("e");
			this.State = ButtonState.Hovered;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseLeave(MouseEventArgs e)
		{
			if (e == null)
				throw new ArgumentNullException("e");
			this.State = ButtonState.None;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e == null)
				throw new ArgumentNullException("e");
			if (e.Button == MouseButton.Left)
				this.State = ButtonState.Pressed;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (e == null)
				throw new ArgumentNullException("e");
			if (e.Button == MouseButton.Left)
				this.State = ButtonState.Hovered;
		}

		/// <summary>
		/// Gets the state of the button
		/// </summary>
		public ButtonState State { get; private set; }
	}

	/// <summary>
	/// Specifies the state of a button
	/// </summary>
	public enum ButtonState
	{
		/// <summary>
		/// Button is in default state.
		/// </summary>
		None,

		/// <summary>
		/// Button is hovered by the mouse
		/// </summary>
		Hovered,

		/// <summary>
		/// Button is pressed
		/// </summary>
		Pressed
	}
}
