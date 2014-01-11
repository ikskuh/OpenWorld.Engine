using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Represents a context for loading assets.
	/// </summary>
	public sealed class AssetLoadContext
	{
		internal AssetLoadContext(AssetManager assetManager, string directory)
		{
			this.AssetManager = assetManager;
			this.Directory = directory;
		}

		/// <summary>
		/// Gets the virtual asset directory name.
		/// </summary>
		public string Directory { get; internal set; }

		/// <summary>
		/// Gets the asset manager.
		/// </summary>
		public AssetManager AssetManager { get; internal set; }
	}
}
