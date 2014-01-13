using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.UserInterface
{
	/// <summary>
	/// Defines a control. A control is the base class of every Gui element.
	/// </summary>
	[Renderer(typeof(OpenWorld.Engine.UserInterface.DefaultRenderers.ControlRenderer))]
	public partial class Control : DiGraph<Control>
	{
		private ScalarRectangle bounds;
		private ScalarRectangle clientArea;
		private string text;
		private Color backColor;
		private Color foreColor;
		private Font font;
		private TextAlign textAlign;
		private bool enabled;
		private bool visible;

		#region Events

		/// <summary>
		/// Occurs when the control is clicked.
		/// </summary>
		public event EventHandler<EventArgs> Click;

		/// <summary>
		/// Occurs when the control gets focus.
		/// </summary>
		public event EventHandler<EventArgs> Enter;

		/// <summary>
		/// Occurs when the control gets updated.
		/// </summary>
		public event EventHandler<UpdateEventArgs> Update;

		/// <summary>
		/// Occurs when the control lost focus.
		/// </summary>
		public event EventHandler<EventArgs> Leave;

		/// <summary>
		/// Occurs when the mouse enters the control.
		/// </summary>
		public event EventHandler<MouseEventArgs> MouseEnter;

		/// <summary>
		/// Occurs when the mouse leaves the control.
		/// </summary>
		public event EventHandler<MouseEventArgs> MouseLeave;

		/// <summary>
		/// Occurs when the mouse is moved over control.
		/// </summary>
		public event EventHandler<MouseEventArgs> MouseMove;

		/// <summary>
		/// Occurs when the mouse stops some time over the control.
		/// </summary>
		public event EventHandler<MouseEventArgs> MouseHover;

		/// <summary>
		/// Occurs when a mouse button gets released over the control.
		/// </summary>
		public event EventHandler<MouseEventArgs> MouseDown;
		
		/// <summary>
		/// Occurs when a mouse button gets pressed over the control.
		/// </summary>
		public event EventHandler<MouseEventArgs> MouseUp;

		/// <summary>
		/// Occurs when the control gets clicked with the mouse.
		/// </summary>
		public event EventHandler<MouseEventArgs> MouseClick;

		/// <summary>
		/// Occurs when a key gets pressed and the control has focus.
		/// </summary>
		public event EventHandler<KeyEventArgs> KeyDown;

		/// <summary>
		/// Occurs when a key gets released and the control has focus.
		/// </summary>
		public event EventHandler<KeyEventArgs> KeyUp;

		/// <summary>
		/// Occurs when a character gets entered and the control has focus.
		/// </summary>
		public event EventHandler<KeyPressEventArgs> KeyPress;

		#endregion

		/// <summary>
		/// Instantiates a new control.
		/// </summary>
		public Control()
		{
			this.clientArea = ScalarRectangle.FullScreen;
			this.text = "";
			this.font = Control.DefaultFont;
			this.backColor = Control.DefaultBackColor;
			this.foreColor = Control.DefaultForeColor;
			this.textAlign = TextAlign.MiddleCenter;
			this.visible = true;
			this.enabled = true;
		}

		internal void UpdateControl(GameTime time)
		{
			if (!this.Enabled) return;
			if (!this.Visible) return;
			this.OnUpdate(new UpdateEventArgs(time));
			foreach (var child in this.Children)
				child.UpdateControl(time);
		}

		internal void Draw(GuiRenderEngine engine, GameTime time, Box2 parentBounds)
		{
			if (!this.Visible) return;
			var renderer = engine.GetRenderer(this.GetType());
			var screenBounds = this.ScreenBounds;

			if (screenBounds.Left >= this.Gui.ScreenSize.X || screenBounds.Right < 0)
				return;
			if (screenBounds.Top >= this.Gui.ScreenSize.Y || screenBounds.Bottom < 0)
				return;

			Box2 bounds;
			bounds.Left = Math.Max(screenBounds.Left, parentBounds.Left);
			bounds.Top = Math.Max(screenBounds.Top, parentBounds.Top);
			bounds.Right = Math.Min(screenBounds.Right, parentBounds.Right);
			bounds.Bottom = Math.Min(screenBounds.Bottom, parentBounds.Bottom);

			engine.SetArea(bounds, bounds.Left - screenBounds.Left, bounds.Top - screenBounds.Top);

			Box2 localBounds = new Box2(0, 0, screenBounds.Width, screenBounds.Height);

			renderer.Render(this, localBounds);

			foreach (var child in this.Children.Reverse())
				child.Draw(engine, time, bounds);
		}

		/// <summary>
		/// Moves the control to the begin of the update list.
		/// </summary>
		public void BringtToFront()
		{
			var parent = this.Parent;

			parent.Children.Remove(this);
			parent.Children.Insert(0, this);
		}

		/// <summary>
		/// Calculates the bounds of the control in screen space.
		/// </summary>
		/// <returns>Bounds in screen space.</returns>
		protected virtual Box2 OnGetBounds()
		{
			if (this.Parent == null)
				throw new InvalidOperationException("Failed to get control bounds: No parent found.");
			return this.Bounds.Transform(this.Parent.ScreenClientBounds);
		}

		/// <summary>
		/// Converts a global screen point to a local point.
		/// </summary>
		/// <param name="xy">Screen point.</param>
		/// <returns>Screen point in client coordinates</returns>
		public Vector2 PointToClient(Vector2 xy)
		{
			return this.PointToClient(xy.X, xy.Y);
		}

		/// <summary>
		/// Converts a global screen point to a local point.
		/// </summary>
		/// <param name="x">Screen point x.</param>
		/// <param name="y">Screen point y.</param>
		/// <returns>Screen point in client coordinates</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public Vector2 PointToClient(float x, float y)
		{
			Box2 screenBounds = this.ScreenBounds;
			Vector2 result = new Vector2();
			result.X = x - screenBounds.Left;
			result.Y = y - screenBounds.Top;
			return result;
		}

		/// <summary>
		/// Converts a local point to a global screen point.
		/// </summary>
		/// <param name="x">Local point x</param>
		/// <param name="y">Local point y</param>
		/// <returns>Global point</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
		public Vector2 PointToScreen(float x, float y)
		{
			Box2 localBounds = this.ScreenBounds;
			Vector2 result = new Vector2();
			result.X = x + localBounds.Left;
			result.Y = y + localBounds.Top;
			return result;
		}

		/// <summary>
		/// Focuses this control.
		/// </summary>
		public void Focus()
		{
			var gui = this.Gui;
			if (gui != null)
				gui.SetFocus(this);
		}

		/// <summary>
		/// Gets the bounds in screen space.
		/// </summary>
		/// <returns>Client bounds in screen space.</returns>
		public Box2 ScreenBounds
		{
			get
			{
				return this.OnGetBounds();
			}
		}

		/// <summary>
		/// Gets the client bounds in screen space.
		/// </summary>
		/// <returns>Client bounds in screen space.</returns>
		public Box2 ScreenClientBounds
		{
			get
			{
				return this.ClientBounds.Transform(this.ScreenBounds);
			}
		}

		/// <summary>
		/// Gets or sets the bounds of the control.
		/// </summary>
		public ScalarRectangle Bounds
		{
			get { return this.bounds; }
			set { this.bounds = value; }
		}

		/// <summary>
		/// Gets or sets the distance to the left screen border.
		/// </summary>
		public Scalar Left
		{
			get { return this.bounds.Left; }
			set { this.bounds.Left = value; }
		}

		/// <summary>
		/// Gets or sets the distance to the upper screen border.
		/// </summary>
		public Scalar Top
		{
			get { return this.bounds.Top; }
			set { this.bounds.Top = value; }
		}

		/// <summary>
		/// Gets or sets the width.
		/// </summary>
		public Scalar Width
		{
			get { return this.bounds.Width; }
			set { this.bounds.Width = value; }
		}

		/// <summary>
		/// Gets or sets the height.
		/// </summary>
		public Scalar Height
		{
			get { return this.bounds.Height; }
			set { this.bounds.Height = value; }
		}

		/// <summary>
		/// Gets or sets the client bounds of the control.
		/// </summary>
		public ScalarRectangle ClientBounds
		{
			get { return clientArea; }
			set { clientArea = value; }
		}

		/// <summary>
		/// Gets the containing GUI.
		/// </summary>
		/// <returns>Containng GUI</returns>
		public Gui Gui
		{
			get
			{
				Control topContainer = this.Parent;
				if (this is Gui)
					return this as Gui;
				if (topContainer == null)
					return null;
				while (topContainer.Parent != null) topContainer = topContainer.Parent;
				return topContainer as Gui;
			}
		}

		/// <summary>
		/// Gets or sets a user object.
		/// </summary>
		public object Tag { get; set; }

		/// <summary>
		/// Gets a value that determines wheather the control is focused or not.
		/// </summary>
		public bool IsFocused { get; internal set; }

		/// <summary>
		/// Gets or sets the back color.
		/// </summary>
		public virtual Color BackColor
		{
			get { return this.backColor; }
			set { this.backColor = value; }
		}

		/// <summary>
		/// Gets or sets the fore color.
		/// </summary>
		public virtual Color ForeColor
		{
			get { return this.foreColor; }
			set { this.foreColor = value; }
		}

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		public virtual string Text
		{
			get { return this.text; }
			set { this.text = value; }
		}

		/// <summary>
		/// Gets or sets the font.
		/// </summary>
		public virtual Font Font
		{
			get { return this.font; }
			set { this.font = value; }
		}

		/// <summary>
		/// Gets or sets the text align.
		/// </summary>
		public virtual TextAlign TextAlign
		{
			get { return this.textAlign; }
			set { this.textAlign = value; }
		}

		/// <summary>
		/// Gets or sets a value that determines if the control receives events.
		/// </summary>
		public virtual bool Enabled
		{
			get { return enabled; }
			set { enabled = value; }
		}

		/// <summary>
		/// Gets or sets a value that determines if the control is drawn and receives events.
		/// </summary>
		public virtual bool Visible
		{
			get { return visible; }
			set { visible = value; }
		}


		#region Event Handle Methods

		bool leftClickBegin = false;
		bool rightClickBegin = false;

		/// <summary>
		/// Raises the Click event.
		/// </summary>
		/// <param name="e"></param>
		protected internal virtual void OnClick(EventArgs e)
		{
			this.Focus();
			if (this.Click != null)
				this.Click(this, e);
		}
		
		/// <summary>
		/// Raises the Enter event.
		/// </summary>
		/// <param name="e"></param>
		protected internal virtual void OnEnter(EventArgs e)
		{
			if (this.Enter != null)
				this.Enter(this, EventArgs.Empty);
		}

		/// <summary>
		/// Raises the Leave event.
		/// </summary>
		/// <param name="e"></param>
		protected internal virtual void OnLeave(EventArgs e)
		{
			if (this.Leave != null)
				this.Leave(this, EventArgs.Empty);
		}

		/// <summary>
		/// Raises the MouseEnter event.
		/// </summary>
		/// <param name="e"></param>
		protected internal virtual void OnMouseEnter(MouseEventArgs e)
		{
			this.leftClickBegin = false;
			this.rightClickBegin = false;
			if (this.MouseEnter != null)
				this.MouseEnter(this, e);
		}

		/// <summary>
		/// Raises the MouseLeave event.
		/// </summary>
		/// <param name="e"></param>
		protected internal virtual void OnMouseLeave(MouseEventArgs e)
		{
			this.leftClickBegin = false;
			this.rightClickBegin = false;
			if (this.MouseLeave != null)
				this.MouseLeave(this, e);
		}

		/// <summary>
		/// Raises the MouseMove event.
		/// </summary>
		/// <param name="e"></param>
		protected internal virtual void OnMouseMove(MouseEventArgs e)
		{
			if (this.MouseMove != null)
				this.MouseMove(this, e);
		}

		/// <summary>
		/// Raises the MouseHover event.
		/// </summary>
		/// <param name="e"></param>
		protected internal virtual void OnMouseHover(MouseEventArgs e)
		{
			if (this.MouseHover != null)
				this.MouseHover(this, e);
		}

		/// <summary>
		/// Raises the MouseDown event.
		/// </summary>
		/// <param name="e"></param>
		protected internal virtual void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButton.Left)
				this.leftClickBegin = true;
			if (e.Button == MouseButton.Right)
				this.rightClickBegin = true;
			if (this.MouseDown != null)
				this.MouseDown(this, e);
		}

		/// <summary>
		/// Raises the MouseUp event.
		/// </summary>
		/// <param name="e"></param>
		protected internal virtual void OnMouseUp(MouseEventArgs e)
		{
			if (this.leftClickBegin && e.Button == MouseButton.Left)
			{
				this.OnMouseClick(e);
				this.OnClick(e);
			}
			if (this.rightClickBegin && e.Button == MouseButton.Right)
			{
				this.OnMouseClick(e);
			}
			this.leftClickBegin = false;
			this.rightClickBegin = false;

			if (this.MouseUp != null)
				this.MouseUp(this, e);
		}

		/// <summary>
		/// Raises the MouseClick event.
		/// </summary>
		/// <param name="e"></param>
		protected internal virtual void OnMouseClick(MouseEventArgs e)
		{
			if (this.MouseClick != null)
				this.MouseClick(this, e);
		}

		/// <summary>
		/// Raises the KeyDown event.
		/// </summary>
		/// <param name="e"></param>
		protected internal virtual void OnKeyDown(KeyEventArgs e)
		{
			if (this.KeyDown != null)
				this.KeyDown(this, e);
		}

		/// <summary>
		/// Raises the KeyUp event.
		/// </summary>
		/// <param name="e"></param>
		protected internal virtual void OnKeyUp(KeyEventArgs e)
		{
			if (this.KeyUp != null)
				this.KeyUp(this, e);
		}

		/// <summary>
		/// Raises the KeyPress event.
		/// </summary>
		/// <param name="e"></param>
		protected internal virtual void OnKeyPress(KeyPressEventArgs e)
		{
			if (this.KeyPress != null)
				this.KeyPress(this, e);
		}

		/// <summary>
		/// Gets called every frame.
		/// </summary>
		/// <param name="e"></param>
		protected internal virtual void OnUpdate(UpdateEventArgs e)
		{
			if (this.Update != null)
				this.Update(this, e);
		}

		#endregion
	}
}
