using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.UserInterface
{
	/// <summary>
	/// Defines a vertical scroll bar.
	/// </summary>
	[Renderer(typeof(OpenWorld.Engine.UserInterface.DefaultRenderers.VScrollBarRenderer))]
	public class VScrollBar : Control
	{
		float value;

		float mousePos = 0;
		bool dragging = false;


		/// <summary>
		/// Instantiates a new vertical scrollbar.
		/// </summary>
		public VScrollBar()
		{
			this.Width = new Scalar(0.0f, 21.0f);
			this.Height = new Scalar(1.0f, 0.0f);
			this.Minimum = 0.0f;
			this.Maximum = 1.0f;
			this.Value = 0.0f;
			this.Children.Lock();
		}

		/// <summary>
		/// Gets the screen area of the upper button.
		/// </summary>
		/// <returns></returns>
		public Box2 GetUpperButton()
		{
			var bounds = this.ScreenBounds;
			bounds.Bottom = bounds.Top + bounds.Width;
			return bounds;
		}

		/// <summary>
		/// Gets the screen area of the lower button.
		/// </summary>
		/// <returns></returns>
		public Box2 GetLowerButton()
		{
			var bounds = this.ScreenBounds;
			bounds.Top = bounds.Bottom - bounds.Width;
			return bounds;
		}

		/// <summary>
		/// Gets the screen area of the slider button.
		/// </summary>
		/// <returns></returns>
		public Box2 GetSliderKnob()
		{
			var bounds = this.ScreenBounds;
			bounds.Top += bounds.Width;
			bounds.Bottom -= 2 * bounds.Width;

			bounds.Top += ((this.Value - this.Minimum) / (this.Maximum - this.Minimum)) * bounds.Height;
			bounds.Bottom = bounds.Top + bounds.Width;

			return bounds;

		}

		/// <summary>
		/// Raises the MouseClick event.
		/// </summary>
		/// <param name="e"></param>
		protected internal override void OnMouseClick(MouseEventArgs e)
		{
			var upperButton = this.GetUpperButton();
			var lowerButton = this.GetLowerButton();

			if (upperButton.Contains(e.X, e.Y))
				this.Value = GameMath.Clamp(this.value - 0.1f * (this.Maximum - this.Minimum), this.Minimum, this.Maximum);
			else if (lowerButton.Contains(e.X, e.Y))
				this.Value = GameMath.Clamp(this.value + 0.1f * (this.Maximum - this.Minimum), this.Minimum, this.Maximum);
			base.OnMouseClick(e);
		}

		/// <summary>
		/// Raises the MouseMove event.
		/// </summary>
		/// <param name="e"></param>
		protected internal override void OnMouseMove(MouseEventArgs e)
		{
			var upperButton = this.GetUpperButton();
			var lowerButton = this.GetLowerButton();
			var sliderKnob = this.GetSliderKnob();

			if (upperButton.Contains(e.X, e.Y))
				this.FocusedElement = ScrollBarFocus.DecreaseButton;
			else if (lowerButton.Contains(e.X, e.Y))
				this.FocusedElement = ScrollBarFocus.IncreaseButton;
			else if (sliderKnob.Contains(e.X, e.Y))
				this.FocusedElement = ScrollBarFocus.SliderKnob;
			else
				this.FocusedElement = ScrollBarFocus.None;

			base.OnMouseMove(e);
		}

		/// <summary>
		/// Raises the MouseDown event.
		/// </summary>
		/// <param name="e"></param>
		protected internal override void OnMouseDown(MouseEventArgs e)
		{
			var sliderKnob = this.GetSliderKnob();
			if (sliderKnob.Contains(e.X, e.Y) && e.Button == MouseButton.Left)
			{
				this.dragging = true;
				this.mousePos = e.Y;
			}
			base.OnMouseDown(e);
		}

		/// <summary>
		/// Raises the MouseUp event.
		/// </summary>
		/// <param name="e"></param>
		protected internal override void OnMouseUp(MouseEventArgs e)
		{
			if(e.Button == MouseButton.Left)
				this.dragging = false;
			base.OnMouseUp(e);
		}

		/// <summary>
		/// Raises the MouseLeave event.
		/// </summary>
		/// <param name="e"></param>
		protected internal override void OnMouseLeave(MouseEventArgs e)
		{
			this.FocusedElement = ScrollBarFocus.None;
			this.dragging = false;
			base.OnMouseLeave(e);
		}

		/// <summary>
		/// Raises the Update event.
		/// </summary>
		/// <param name="e"></param>
		protected internal override void OnUpdate(UpdateEventArgs e)
		{
			if (this.dragging)
			{
				float delta = this.Gui.MousePosition.Y - this.mousePos;
				float pixelDelta = (this.Maximum - this.Minimum) / (this.ScreenBounds.Height - 2 * this.ScreenBounds.Width);

				this.value = GameMath.Clamp(
					this.value + pixelDelta * delta,
					this.Minimum,
					this.Maximum);

				this.mousePos = this.Gui.MousePosition.Y;
			}
			base.OnUpdate(e);
		}

		/// <summary>
		/// Gets or sets the minimum value of the scrollbar.
		/// </summary>
		public float Minimum { get; set; }

		/// <summary>
		/// Gets or sets the maximum value of the scrollbar.
		/// </summary>
		public float Maximum { get; set; }

		/// <summary>
		/// Gets or sets the value of the scrollbar.
		/// </summary>
		public float Value
		{
			get { return this.value; }
			set
			{
				if (value < this.Minimum)
					throw new InvalidOperationException("Value must between Minimum and Maximum.");
				if (value > this.Maximum)
					throw new InvalidOperationException("Value must between Minimum and Maximum.");
				this.value = value;
			}
		}

		/// <summary>
		/// Gets which element of the scroll bar is focused.
		/// </summary>
		public ScrollBarFocus FocusedElement { get; private set; }
	}
}
