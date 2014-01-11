using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenWorld.Engine.UserInterface
{
	partial class Control
	{
		static ThreadLocal<Font> defaultFont = new ThreadLocal<Font>(() => { return new Font("micross.ttf", 12); });
		static Color defaultBackColor = Color.SlateGray;
		static Color defaultForeColor = Color.Black;

		/// <summary>
		/// Gets or sets the default back color.
		/// </summary>
		public static Color DefaultBackColor
		{
			get { return defaultBackColor; }
			set { defaultBackColor = value; }
		}

		/// <summary>
		/// Gets or sets the default fore color.
		/// </summary>
		public static Color DefaultForeColor
		{
			get { return defaultForeColor; }
			set { defaultForeColor = value; }
		}

		/// <summary>
		/// Gets or sets the default font.
		/// <remarks>Please note that this variable is thread local.</remarks>
		/// </summary>
		public static Font DefaultFont
		{
			get { return Control.defaultFont.Value; }
			set { Control.defaultFont.Value = value; }
		}
	}
}
