using OpenTK;
using OpenWorld.Engine.UserInterface.DefaultRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine.UserInterface
{
	/// <summary>
	/// A Gui system that is a container for Gui elements.
	/// </summary>
	[Renderer(typeof(OpenWorld.Engine.UserInterface.DefaultRenderers.BlankRenderer))]
	public sealed class Gui : Control
	{
		MouseEventArgs mouseState = null;
		int mouseX = 0, mouseY = 0;
		bool leftButton = false, rightButton = false;
		float mouseHoverTiming = 0.0f;
		Control focus = null;
		Control lastMouseFocus = null;

		/// <summary>
		/// Instantiates a new Gui system.
		/// </summary>
		public Gui()
		{
			this.Engine = new GuiRenderEngine(this);
			this.Bounds = ScalarRectangle.FullScreen;
			this.HoverTime = 0.5f;
			this.ScreenSize = new Vector2(Window.Current.ViewportWidth, Window.Current.ViewportHeight);
		}

		/// <summary>
		/// Updates the Gui system
		/// </summary>
		/// <param name="time">Update timing information</param>
		public new void Update(GameTime time)
		{
			if (this.mouseState == null)
			{
				this.mouseState = new MouseEventArgs()
				{
					X = this.mouseX,
					Y = this.mouseY,
					DeltaX = 0,
					DeltaY = 0,
					LeftButton = this.leftButton,
					RightButton = this.rightButton
				};
			}

			this.mouseState.DeltaX = this.mouseX - this.mouseState.X;
			this.mouseState.DeltaY = this.mouseY - this.mouseState.Y;
			this.mouseState.X = this.mouseX;
			this.mouseState.Y = this.mouseY;
			this.mouseState.Button = MouseButton.None;

			Control ctrl = this.GetControlFromPoint(this.mouseX, this.mouseY);
			if (ctrl != lastMouseFocus)
			{
				if (this.lastMouseFocus != null)
				{
					this.lastMouseFocus.OnMouseLeave(this.mouseState);
				}
				this.lastMouseFocus = ctrl;
				if (this.lastMouseFocus != null)
				{
					this.lastMouseFocus.OnMouseEnter(this.mouseState);
				}
			}
			
			if ((this.mouseState.DeltaX != 0 || this.mouseState.DeltaY != 0))
			{
				if(this.lastMouseFocus != null)
					this.lastMouseFocus.OnMouseMove(this.mouseState);
				mouseHoverTiming = 0.0f;
			}
			else
			{
				float previous = mouseHoverTiming;
				mouseHoverTiming += time.DeltaTime;
				if (mouseHoverTiming >= this.HoverTime && previous < this.HoverTime)
				{
					if (this.lastMouseFocus != null)
						this.lastMouseFocus.OnMouseHover(this.mouseState);
				}
			}
			if (this.lastMouseFocus != null)
			{
				if (this.mouseState.LeftButton != this.leftButton)
				{
					this.mouseState.Button = MouseButton.Left;
					if (this.leftButton)
						this.lastMouseFocus.OnMouseDown(this.mouseState);
					else
						this.lastMouseFocus.OnMouseUp(this.mouseState);
				}
				if (this.mouseState.RightButton != this.rightButton)
				{
					this.mouseState.Button = MouseButton.Right;
					if (this.rightButton)
						this.lastMouseFocus.OnMouseDown(this.mouseState);
					else
						this.lastMouseFocus.OnMouseUp(this.mouseState);
				}
				this.mouseState.Button = MouseButton.None;
			}

			this.mouseState.LeftButton = this.leftButton;
			this.mouseState.RightButton = this.rightButton;

			base.UpdateControl(time);
		}

		/// <summary>
		/// Triggers a KeyDown event.
		/// </summary>
		/// <param name="key">Key that triggers the event</param>
		public void TriggerKeyDown(OpenTK.Input.Key key)
		{
			if (this.focus == null) return;
			this.focus.OnKeyDown(new KeyEventArgs(key));
		}

		/// <summary>
		/// Triggers a KeyUp event.
		/// </summary>
		/// <param name="key">Key that triggers the event</param>
		public void TriggerKeyUp(OpenTK.Input.Key key)
		{
			if (this.focus == null) return;
			this.focus.OnKeyUp(new KeyEventArgs(key));
		}

		/// <summary>
		/// Triggers a KeyPress event
		/// </summary>
		/// <param name="keyChar">The char that is entered</param>
		public void TriggerKeyPress(char keyChar)
		{
			if (this.focus == null) return;
			this.focus.OnKeyPress(new KeyPressEventArgs(keyChar));
		}

		/// <summary>
		/// Draws the Gui system.
		/// </summary>
		/// <param name="time">Draw timing information</param>
		public void Draw(GameTime time)
		{
			this.Engine.SetGLStates();
			base.Draw(this.Engine, time, new Box2(0, 0, this.ScreenSize.X, this.ScreenSize.Y));
		}

		/// <summary>
		/// Sets the mouse position
		/// </summary>
		/// <param name="x">x position</param>
		/// <param name="y">y position</param>
		public void SetMousePosition(int x, int y)
		{
			this.mouseX = x;
			this.mouseY = y;
		}
		
		/// <summary>
		/// Sets the mouse buttons
		/// </summary>
		/// <param name="left">Left button state</param>
		/// <param name="right">Right button state</param>
		public void SetMouseButtons(bool left, bool right)
		{
			this.leftButton = left;
			this.rightButton = right;
		}

		/// <summary>
		/// Sets the focus to a control.
		/// </summary>
		/// <param name="control">The control to be focused.</param>
		public void SetFocus(Control control)
		{
			if (this.focus == control)
				return; // No triggering, focus stays.
			if (this.focus != null)
			{
				this.focus.IsFocused = false;
				this.focus.OnLeave(EventArgs.Empty);
			}
			this.focus = control;
			if (this.focus != null)
			{
				this.focus.IsFocused = true;
				this.focus.OnEnter(EventArgs.Empty);
			}
		}

		/// <summary>
		/// Gets the control from a screen point
		/// </summary>
		/// <param name="x">x position</param>
		/// <param name="y">y position</param>
		/// <returns>Control at point or null if none</returns>
		public Control GetControlFromPoint(int x, int y)
		{
			Control container = this;
			while (container.Children.Count > 0)
			{
				bool found = false;
				foreach (var child in container.Children)
				{
					var bounds = child.ScreenBounds;
					if (!bounds.Contains(x, y))
						continue;
					if (!child.Visible)
						continue;
					container = child;
					found = true;
					break;
				}
				if (!found)
					break;
			}
			if (container == this)
				return null;
			return container;
		}

		/// <summary>
		/// Gets the screen size
		/// </summary>
		/// <returns></returns>
		protected override Box2 OnGetBounds()
		{
			return new Box2(0, 0, this.ScreenSize.X, this.ScreenSize.Y);
		}

		/// <summary>
		/// Gets the rendering engine
		/// </summary>
		public GuiRenderEngine Engine { get; private set; }

		/// <summary>
		/// Gets or sets the hover time in seconds.
		/// </summary>
		public float HoverTime { get; set; }

		/// <summary>
		/// Gets the current mouse position.
		/// </summary>
		public Vector2 MousePosition { get { return new Vector2(this.mouseState.X, this.mouseState.Y); } }

		/// <summary>
		/// Gets the screen size.
		/// </summary>
		public Vector2 ScreenSize { get; private set; }
	}
}
