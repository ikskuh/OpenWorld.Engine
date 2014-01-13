using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.UserInterface
{
	/// <summary>
	/// Defines a list box.
	/// </summary>
	[Renderer(typeof(OpenWorld.Engine.UserInterface.DefaultRenderers.ListBoxRenderer))]
	public sealed class ListBox : Control
	{
		List<object> listBox = new List<object>();
		VScrollBar scrollBar;

		/// <summary>
		/// Instantiates a new list box.
		/// </summary>
		public ListBox()
		{
			this.scrollBar = new VScrollBar();
			this.scrollBar.Left = new Scalar(1.0f, -21.0f);
			this.scrollBar.Width = new Scalar(0.0f, 21.0f);
			this.scrollBar.Minimum = 0;
			this.scrollBar.Maximum = 1.0f;
			this.scrollBar.Parent = this;
			this.Children.Lock();

			this.ItemHeight = 20;

			this.Width = new Scalar(0.0f, 160.0f);
			this.Height = new Scalar(0.0f, 200.0f);

			this.ClientBounds = new ScalarRectangle(
				new Scalar(0.0f, 0.0f), new Scalar(0.0f, 0.0f),
				new Scalar(1.0f, -20.0f), new Scalar(1.0f, 0.0f));
		}

		/// <summary>
		/// Gets the offset that the list box offset is scrolled.
		/// </summary>
		/// <returns></returns>
		public float GetScrollOffset()
		{
			return -Math.Max(
				0.0f,
				(this.ItemHeight * this.Items.Count - this.ScreenBounds.Height) * this.scrollBar.Value);
		}

		/// <summary>
		/// Gets the list items.
		/// </summary>
		public IList<object> Items { get { return this.listBox; } }

		/// <summary>
		/// Gets or sets the item height.
		/// </summary>
		public int ItemHeight { get; set; }
	}
}
