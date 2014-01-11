using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Provides methods for game input.
	/// </summary>
	public sealed class InputManager
	{
		private KeyboardDevice keyboardDevice;
		private MouseDevice mouseDevice;
		private JoystickDevice[] joysticks;

		internal InputManager(KeyboardDevice keyboardDevice, MouseDevice mouseDevice, JoystickDevice[] joystickDevice)
		{
			this.keyboardDevice = keyboardDevice;
			this.mouseDevice = mouseDevice;
			this.joysticks = joystickDevice;
		}

		/// <summary>
		/// Gets a joystick.
		/// </summary>
		/// <param name="id">Id of the joystick.</param>
		/// <returns>Joystick with ID.</returns>
		public JoystickDevice GetJoystick(int id)
		{
			if(id < 0)
				return null;
			if(id >= this.joysticks.Length)
				return null;
			return this.joysticks[id];
		}

		/// <summary>
		/// Gets the keyboard.
		/// </summary>
		public KeyboardDevice Keyboard { get { return this.keyboardDevice; } }

		/// <summary>
		/// Gets the mouse.
		/// </summary>
		public MouseDevice Mouse { get { return this.mouseDevice; } }
	}
}
