using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines an asset source that loads from a zip file.
	/// </summary>
	public sealed class ZipFileAssetSource : AssetSource
	{
		ZipFile zipFile;
		string password = null;

		/// <summary>
		/// Creates a new zip file asset source.
		/// </summary>
		/// <param name="zipFile">The zip file.</param>
		public ZipFileAssetSource(string zipFile)
		{
			this.zipFile = new ZipFile(zipFile);
		}

		/// <summary>
		/// Creates a new zip file asset source.
		/// </summary>
		/// <param name="zipFile">The zip file.</param>
		/// <param name="password">THe password of the zip file.</param>
		public ZipFileAssetSource(string zipFile, string password)
		{
			this.zipFile = new ZipFile(zipFile);
			this.password = password;
		}

		/// <summary>
		/// Opens a stream for an asset.
		/// </summary>
		/// <param name="assetName">The path to the asset with extension.</param>
		/// <returns>Stream that contains the asset.</returns>
		protected internal override Stream Open(string assetName)
		{
			if (this.password == null)
				return this.zipFile[assetName].OpenReader();
			else
				return this.zipFile[assetName].OpenReader(this.password);
		}

		/// <summary>
		/// Checks if an asset exists.
		/// </summary>
		/// <param name="assetName">The path to the asset with extension.</param>
		/// <returns>True if the asset exists.</returns>
		protected internal override bool Exists(string assetName)
		{
			return this.zipFile[assetName] != null;
		}
	}
}
