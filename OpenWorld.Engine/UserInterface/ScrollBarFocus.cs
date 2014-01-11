using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.UserInterface
{
	/// <summary>
	/// Specifies the focused element of a scroll bar.
	/// </summary>
	public enum ScrollBarFocus
	{
		/// <summary>
		/// No element is focused.
		/// </summary>
		None,

		/// <summary>
		/// The decrease button is focused.
		/// </summary>
		DecreaseButton,

		/// <summary>
		/// The increase button is focused.
		/// </summary>
		IncreaseButton,

		/// <summary>
		/// The slider knob is focused.
		/// </summary>
		SliderKnob,
	}
}
