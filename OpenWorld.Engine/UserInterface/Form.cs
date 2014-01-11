using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.UserInterface
{
	/// <summary>
	/// Defines a gui window.
	/// </summary>
	[Renderer(typeof(OpenWorld.Engine.UserInterface.DefaultRenderers.FormRenderer))]
	public class Form : Control
	{
		float mouseX, mouseY;
		bool dragging = false;

		/// <summary>
		/// Instantiates a new window
		/// </summary>
		public Form()
		{
			this.Width = new Scalar(0.0f, 400.0f);
			this.Height = new Scalar(0.0f, 300.0f);
			this.ClientBounds = new ScalarRectangle(
				new Scalar(0.0f, 1.0f), new Scalar(0.0f, 20.0f),
				new Scalar(1.0f, -1.0f), new Scalar(1.0f, -21.0f));
		}

		/// <summary>
		/// Closes the form.
		/// </summary>
		public void Close()
		{
			this.Visible = false;
		}

		/// <summary>
		/// Hides the form.
		/// </summary>
		public void Hide()
		{
			this.Visible = false;
		}

		/// <summary>
		/// Shows the form.
		/// </summary>
		public void Show()
		{
			this.Visible = true;
		}

		private bool CheckForTitleBarArea(MouseEventArgs e)
		{
			var bounds = this.ScreenBounds;
			bounds.Bottom = bounds.Top + 20;
			return bounds.Contains(e.X, e.Y);
		}
		private bool CheckForClientArea(MouseEventArgs e)
		{
			return this.ScreenClientBounds.Contains(e.X, e.Y);
		}

		/// <summary>
		/// Raises the MouseDown event.
		/// </summary>
		/// <param name="e"></param>
		protected internal override void OnMouseDown(MouseEventArgs e)
		{
			this.BringtToFront();
			if (this.CheckForTitleBarArea(e) && e.Button == MouseButton.Left)
			{
				this.mouseX = e.X;
				this.mouseY = e.Y;
				this.dragging = true;
			}
			if (this.CheckForClientArea(e))
				base.OnMouseDown(e);
		}

		/// <summary>
		/// Raises the MouseUp event.
		/// </summary>
		/// <param name="e"></param>
		protected internal override void OnMouseUp(MouseEventArgs e)
		{
			if (e.Button == MouseButton.Left)
				this.dragging = false;
			if (this.CheckForClientArea(e))
				base.OnMouseUp(e);
		}

		/// <summary>
		/// Raises the MouseClick event.
		/// </summary>
		/// <param name="e"></param>
		protected internal override void OnMouseClick(MouseEventArgs e)
		{
			if (this.CheckForClientArea(e))
				base.OnMouseClick(e);
		}

		/// <summary>
		/// Raises the MouseEnter event.
		/// </summary>
		/// <param name="e"></param>
		protected internal override void OnMouseEnter(MouseEventArgs e)
		{
			if (this.CheckForClientArea(e))
				base.OnMouseEnter(e);
		}

		/// <summary>
		/// Raises the MouseHover event.
		/// </summary>
		/// <param name="e"></param>
		protected internal override void OnMouseHover(MouseEventArgs e)
		{
			if (this.CheckForClientArea(e))
				base.OnMouseHover(e);
		}

		/// <summary>
		/// Raises the MouseLeave event.
		/// </summary>
		/// <param name="e"></param>
		protected internal override void OnMouseLeave(MouseEventArgs e)
		{
			if (this.CheckForClientArea(e))
				base.OnMouseLeave(e);
		}

		/// <summary>
		/// Raises the MouseMove event.
		/// </summary>
		/// <param name="e"></param>
		protected internal override void OnMouseMove(MouseEventArgs e)
		{
			if (this.CheckForClientArea(e))
				base.OnMouseMove(e);
		}

		/// <summary>
		/// Raises the Update event.
		/// </summary>
		/// <param name="e"></param>
		protected internal override void OnUpdate(UpdateEventArgs e)
		{
			if(this.dragging)
			{
				this.Left += new Scalar(0.0f, this.Gui.MousePosition.X - this.mouseX);
				this.Top += new Scalar(0.0f, this.Gui.MousePosition.Y - this.mouseY);
				this.mouseX = this.Gui.MousePosition.X;
				this.mouseY = this.Gui.MousePosition.Y;
			}
			base.OnUpdate(e);
		}
	}
}
