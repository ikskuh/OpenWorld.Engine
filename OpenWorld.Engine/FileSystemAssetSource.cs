using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines a an asset source that loads from the file system.
	/// </summary>
	public sealed class FileSystemAssetSource : AssetSource
	{
		/// <summary>
		/// Creates a new file system asset source.
		/// </summary>
		public FileSystemAssetSource()
		{
			this.RootDirectory = "./";
		}

		/// <summary>
		/// Creates a new file system asset source.
		/// </summary>
		public FileSystemAssetSource(string rootDirectory)
		{
			this.RootDirectory = rootDirectory + "/";
		}

		/// <summary>
		/// Opens a stream for an asset.
		/// </summary>
		/// <param name="assetName">The path to the asset.</param>
		/// <returns>Stream that contains the asset.</returns>
		protected internal override Stream Open(string assetName)
		{
			return File.Open(this.GetFileName(assetName), FileMode.Open);
		}

		/// <summary>
		/// Checks if an asset exists.
		/// </summary>
		/// <param name="assetName">The path to the asset.</param>
		/// <returns>True if the asset exists.</returns>
		protected internal override bool Exists(string assetName)
		{
			return File.Exists(this.GetFileName(assetName));
		}

		private string GetFileName(string assetName)
		{
			return this.RootDirectory + assetName;
		}

		/// <summary>
		/// Gets or sets the root directory of this source.
		/// </summary>
		public string RootDirectory { get; set; }
	}
}
