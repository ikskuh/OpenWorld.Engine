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
	public partial class Control : IControlContainer
	{
		private readonly Container controls;
		private ScalarRectangle bounds;
		private ScalarRectangle clientArea;
		internal Control parent;
		private string text;
		private Color backColor;
		private Color foreColor;
		private Font font;
		private TextAlign textAlign;

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
			this.controls = new Container(this);
			this.clientArea = ScalarRectangle.FullScreen;
			this.text = "";
			this.font = Control.DefaultFont;
			this.backColor = Control.DefaultBackColor;
			this.foreColor = Control.DefaultForeColor;
			this.textAlign = TextAlign.MiddleCenter;
		}

		internal void Update(GameTime time)
		{
			this.OnUpdate(time);
			foreach (var child in this.controls)
				child.Update(time);
		}

		internal void Draw(GuiRenderEngine engine, GameTime time)
		{
			var renderer = engine.GetRenderer(this.GetType());
			renderer.Render(this);

			foreach (var child in this.controls)
				child.Draw(engine, time);
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
		/// Calculates the bounds of the control in screen space.
		/// </summary>
		/// <returns>Bounds in screen space.</returns>
		protected virtual Box2 OnGetBounds()
		{
			if (this.parent == null)
				throw new InvalidOperationException("Failed to get control bounds: No parent found.");
			return this.Bounds.Transform(this.parent.ScreenClientBounds);
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
		/// Gets or sets the parent of this control.
		/// </summary>
		public Control Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				if (this.parent == value)
					return; // Nothing to do here

				// this.parent gets set in Controls.Remove and Controls.Add
				if (this.parent != null)
					this.parent.Controls.Remove(this);
				if (value != null)
					value.Controls.Add(this);
			}
		}

		/// <summary>
		/// Gets a container that contains the children of the control.
		/// </summary>
		public Container Controls
		{
			get { return this.controls; }
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
				Control topContainer = this.parent;
				if (topContainer == null)
					return null;
				while (topContainer.parent != null) topContainer = topContainer.parent;
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

		#region Event Raise Methods

		bool leftClickBegin = false;
		bool rightClickBegin = false;

		internal void RaiseClick()
		{
			this.Focus();
			this.OnClick(EventArgs.Empty);
			if (this.Click != null)
				this.Click(this, EventArgs.Empty);
		}

		internal void RaiseEnter()
		{
			this.OnEnter(EventArgs.Empty);
			if (this.Enter != null)
				this.Enter(this, EventArgs.Empty);
		}

		internal void RaiseLeave()
		{
			this.OnLeave(EventArgs.Empty);
			if (this.Leave != null)
				this.Leave(this, EventArgs.Empty);
		}

		internal void RaiseMouseLeave(MouseEventArgs e)
		{
			this.OnMouseLeave(e);
			this.leftClickBegin = false;
			this.rightClickBegin = false;
			if (this.MouseLeave != null)
				this.MouseLeave(this, e);
		}

		internal void RaiseMouseEnter(MouseEventArgs e)
		{
			this.OnMouseEnter(e);
			this.leftClickBegin = false;
			this.rightClickBegin = false;
			if (this.MouseEnter != null)
				this.MouseEnter(this, e);
		}

		internal void RaiseMouseMove(MouseEventArgs e)
		{
			this.OnMouseMove(e);
			if (this.MouseMove != null)
				this.MouseMove(this, e);
		}

		internal void RaiseMouseUp(MouseEventArgs e)
		{
			this.OnMouseUp(e);
			if (this.leftClickBegin && e.Button == MouseButton.Left)
			{
				this.RaiseMouseClick(e);
				this.RaiseClick();
			}
			if (this.rightClickBegin && e.Button == MouseButton.Right)
			{
				this.RaiseMouseClick(e);
			}
			this.leftClickBegin = false;
			this.rightClickBegin = false;

			if (this.MouseUp != null)
				this.MouseUp(this, e);
		}

		internal void RaiseMouseDown(MouseEventArgs e)
		{
			this.OnMouseDown(e);
			if (e.Button == MouseButton.Left)
				this.leftClickBegin = true;
			if (e.Button == MouseButton.Right)
				this.rightClickBegin = true;
			if (this.MouseDown != null)
				this.MouseDown(this, e);
		}

		internal void RaiseMouseClick(MouseEventArgs e)
		{
			this.OnMouseClick(e);
			if (this.MouseClick != null)
				this.MouseClick(this, e);
		}

		internal void RaiseMouseHover(MouseEventArgs e)
		{
			this.OnMouseHover(e);
			if (this.MouseHover != null)
				this.MouseHover(this, e);
		}

		internal void RaiseKeyDown(KeyEventArgs e)
		{
			this.OnKeyDown(e);
			if (this.KeyDown != null)
				this.KeyDown(this, e);
		}

		internal void RaiseKeyUp(KeyEventArgs e)
		{
			this.OnKeyUp(e);
			if (this.KeyUp != null)
				this.KeyUp(this, e);
		}

		internal void RaiseKeyPress(KeyPressEventArgs e)
		{
			this.OnKeyPress(e);
			if (this.KeyPress != null)
				this.KeyPress(this, e);
		}

		#endregion

		#region Event Handle Methods

		/// <summary>
		/// Gets called when the Click event occurs.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnClick(EventArgs e) { }
		
		/// <summary>
		/// Gets called when the Enter event occurs.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnEnter(EventArgs e) { }

		/// <summary>
		/// Gets called when the Leave event occurs.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnLeave(EventArgs e) { }

		/// <summary>
		/// Gets called when the MouseEnter event occurs.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnMouseEnter(MouseEventArgs e) { }

		/// <summary>
		/// Gets called when the MouseLeave event occurs.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnMouseLeave(MouseEventArgs e) { }

		/// <summary>
		/// Gets called when the MouseMove event occurs.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnMouseMove(MouseEventArgs e) { }

		/// <summary>
		/// Gets called when the MouseHover event occurs.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnMouseHover(MouseEventArgs e) { }

		/// <summary>
		/// Gets called when the MouseDown event occurs.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnMouseDown(MouseEventArgs e) { }

		/// <summary>
		/// Gets called when the MouseUp event occurs.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnMouseUp(MouseEventArgs e) { }

		/// <summary>
		/// Gets called when the MouseClick event occurs.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnMouseClick(MouseEventArgs e) { }

		/// <summary>
		/// Gets called when the KeyDown event occurs.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnKeyDown(KeyEventArgs e) { }

		/// <summary>
		/// Gets called when the KeyUp event occurs.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnKeyUp(KeyEventArgs e) { }

		/// <summary>
		/// Gets called when the KeyPress event occurs.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnKeyPress(KeyPressEventArgs e) { }

		/// <summary>
		/// Gets called every frame.
		/// </summary>
		/// <param name="time"></param>
		protected virtual void OnUpdate(GameTime time) { }

		#endregion
	}
}
