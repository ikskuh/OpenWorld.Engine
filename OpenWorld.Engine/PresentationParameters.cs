using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Provides properties that can be used for customizing game startup.
	/// </summary>
	public struct PresentationParameters
	{
		/// <summary>
		/// Gets or sets a value that defines the screen resolution.
		/// </summary>
		public Size Resolution;

		/// <summary>
		/// Gets or sets a value that determines if the game will be presented in fullscreen or not.
		/// </summary>
		public bool IsFullscreen;

		/// <summary>
		/// Gets or sets a value that defines the window title.
		/// </summary>
		public string Title;

		/// <summary>
		/// Gets or sets the display device that will be used for presentation.
		/// </summary>
		public DisplayDevice DisplayDevice;

		/// <summary>
		/// Gets or sets a value that defines if the game uses vsync or not.
		/// </summary>
		public bool VSync;

		/// <summary>
		/// Gets or sets the graphics mode.
		/// </summary>
		public OpenTK.Graphics.GraphicsMode GraphicsMode;
	}
}
