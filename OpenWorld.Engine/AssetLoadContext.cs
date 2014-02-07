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
		internal AssetLoadContext(AssetManager assetManager, string assetName, string directory)
		{
			this.AssetManager = assetManager;
			this.Directory = directory;
			this.Name = assetName;
		}

		/// <summary>
		/// Gets the virtual asset directory name.
		/// </summary>
		public string Directory { get; private set; }

		/// <summary>
		/// Gets the asset manager.
		/// </summary>
		public AssetManager AssetManager { get; private set; }

		/// <summary>
		/// Gets the name of the asset that is loaded.
		/// </summary>
		public string Name { get; private set; }


		/// <summary>
		/// Converts the load context to a string.
		/// </summary>
		public override string ToString()
		{
			return "Asset: " + this.Name;
		}
	}
}
