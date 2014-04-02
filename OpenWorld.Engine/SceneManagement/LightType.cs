using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Specifies a light type.
	/// </summary>
	public enum LightType
	{
		/// <summary>
		/// A point light
		/// </summary>
		Point,

		/// <summary>
		/// A spot light
		/// </summary>
		Spot,

		/// <summary>
		/// A direction light
		/// </summary>
		Directional
	}
}
