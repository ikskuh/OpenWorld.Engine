using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Provides methods to load assets.
	/// </summary>
	public sealed partial class AssetManager
	{
		private readonly AssetCache cache = new AssetCache();
		private readonly ICollection<AssetSource> sources = new List<AssetSource>();

		/// <summary>
		/// Loads an asset.
		/// </summary>
		/// <typeparam name="T">Type of the asset.</typeparam>
		/// <param name="name">Name of the asset. Can contain path information.</param>
		/// <returns>Loaded asset.</returns>
		public T Load<T>(string name)
			where T : IAsset
		{
			return this.Load<T>(name, false);
		}

		/// <summary>
		/// Loads an asset.
		/// </summary>
		/// <typeparam name="T">Type of the asset.</typeparam>
		/// <param name="name">Name of the asset. Can contain path information.</param>
		/// <param name="loadNew">If true, the asset will be loaded non-cached.</param>
		/// <returns>Loaded asset.</returns>
		public T Load<T>(string name, bool loadNew)
			where T : IAsset
		{
			string selectedExtension;

			if (!this.IsCaseSensitive)
				name = name.ToLower();

			if (!loadNew && this.cache.Contains<T>(name))
				return this.cache.Get<T>(name);

			T asset = Activator.CreateInstance<T>();
			using (var stream = this.OpenAssetStream<T>(name, out selectedExtension))
			{
				// Explicit cast to get also hidden interface implementations
				IAsset a = (IAsset)asset;
				a.Load(new AssetLoadContext(this, Path.GetDirectoryName(name) + "/"), stream, selectedExtension);
			}
			this.cache.Add<T>(name, asset);
			return asset;
		}

		/// <summary>
		/// Opens a stream for an asset.
		/// </summary>
		/// <typeparam name="T">Type of the asset to be loaded.</typeparam>
		/// <param name="name">Name of the asset. Can contain path information.</param>
		/// <param name="selectedExtension">The extension that the asset stream has.</param>
		/// <returns>Opened stream to load asset from.</returns>
		protected Stream OpenAssetStream<T>(string name, out string selectedExtension)
			where T : IAsset
		{
			var extensions = GetValidExtensions<T>();
			
			selectedExtension = null;
			AssetSource assetSource = null;
			foreach (var source in this.Sources)
			{
				foreach (var extension in extensions)
				{
					if(!source.Exists(name + extension))
						continue;
					assetSource = source;
					selectedExtension = extension;
					break;
				}
				if (selectedExtension != null)
					break;
			}
			if (selectedExtension == null)
				throw new AssetNotFoundException(name);

			return assetSource.Open(name + selectedExtension);
		}

		private static string[] GetValidExtensions<T>()
			where T : IAsset
		{
			HashSet<string> validExtensions = new HashSet<string>();
			var attribs = typeof(T).GetCustomAttributes(typeof(AssetExtensionAttribute), false);
			foreach (AssetExtensionAttribute attrib in attribs)
			{
				foreach (var extension in attrib.GetExtensions())
					validExtensions.Add(extension);
			}
			return validExtensions.ToArray();
		}

		/// <summary>
		/// Gets the asset cache.
		/// </summary>
		private AssetCache Cache
		{
			get { return cache; }
		}

		/// <summary>
		/// Gets or sets a value that indicates wheather the asset
		/// manager uses case sensitive asset names or not.
		/// </summary>
		public bool IsCaseSensitive { get; set; }

		/// <summary>
		/// Gets a collection of asset sources.
		/// </summary>
		public ICollection<AssetSource> Sources
		{
			get { return sources; }
		}
	}
}
