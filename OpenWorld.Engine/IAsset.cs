using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines an asset.
	/// </summary>
	public interface IAsset
	{
		/// <summary>
		/// Loads the asset.
		/// </summary>
		/// <param name="context">The loading context.</param>
		/// <param name="stream">The stream that contains the asset.</param>
		/// <param name="extensionHint">Contains the extension of the asset file.</param>
		void Load(AssetLoadContext context, Stream stream, string extensionHint);
	}
}
