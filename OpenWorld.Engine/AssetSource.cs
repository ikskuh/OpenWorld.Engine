using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines an asset source.
	/// </summary>
	public abstract class AssetSource
	{
		/// <summary>
		/// Opens a stream for an asset.
		/// </summary>
		/// <param name="assetName">The path to the asset with extension.</param>
		/// <returns>Stream that contains the asset.</returns>
		protected internal abstract Stream Open(string assetName);

		/// <summary>
		/// Checks if an asset exists.
		/// </summary>
		/// <param name="assetName">The path to the asset with extension.</param>
		/// <returns>True if the asset exists.</returns>
		protected internal abstract bool Exists(string assetName);
	}
}
