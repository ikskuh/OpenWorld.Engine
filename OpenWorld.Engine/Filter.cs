using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Specifies texture filtering.
	/// </summary>
	public enum Filter
	{
		/// <summary>
		/// Filtering takes the nearest pixel.
		/// </summary>
		Nearest,

		/// <summary>
		/// Filtering interpolates pixel values linear.
		/// </summary>
		Linear,

		/// <summary>
		/// Filtering interpolates pixel values linear with use of mip maps.
		/// </summary>
		LinearMipMapped
	}
}
