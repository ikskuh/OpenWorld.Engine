using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine.UserInterface
{
	/// <summary>
	/// Defines a label.
	/// </summary>
	[Renderer(typeof(OpenWorld.Engine.UserInterface.DefaultRenderers.LabelRenderer))]
	public sealed class Label : Control
	{
		/// <summary>
		/// Instantiates a label.
		/// </summary>
		public Label()
		{
			this.BackColor = Color.Transparent;
			this.TextAlign = UserInterface.TextAlign.TopLeft;
			this.Width = new Scalar(0.0f, 100.0f); 
			this.Height = new Scalar(0.0f, 20.0f);
			this.Children.Lock();
		}
	}
}
