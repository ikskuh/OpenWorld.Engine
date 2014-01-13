using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.UserInterface
{
	/// <summary>
	/// Defines a picture box.
	/// </summary>
	[Renderer(typeof(OpenWorld.Engine.UserInterface.DefaultRenderers.PictureBoxRenderer))]
	public sealed class PictureBox : Control
	{
		/// <summary>
		/// Creates a new picture box.
		/// </summary>
		public PictureBox()
		{
			this.Width = new Scalar(0.0f, 400.0f);
			this.Height = new Scalar(0.0f, 300.0f);
			this.Children.Lock();
		}

		/// <summary>
		/// Gets or sets the image that the picture box should show.
		/// </summary>
		public Texture2D Image { get; set; }
	}
}
