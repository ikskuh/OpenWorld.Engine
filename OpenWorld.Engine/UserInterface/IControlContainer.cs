using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.UserInterface
{
	/// <summary>
	/// Defines a container for controls.
	/// </summary>
	public interface IControlContainer
	{
		/// <summary>
		/// Gets the control container.
		/// </summary>
		Container Controls { get; }
	}
}
