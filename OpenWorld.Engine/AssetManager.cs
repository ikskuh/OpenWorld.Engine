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
	public class AssetManager
	{
		/// <summary>
		/// Instantiates a new asset manager.
		/// </summary>
		public AssetManager()
		{
			this.RootDirectory = "./";
		}

		/// <summary>
		/// Instantiates a new asset manager with root directory.
		/// </summary>
		/// <param name="rootDirectory">The root directory of the asset manager.</param>
		public AssetManager(string rootDirectory)
		{
			this.RootDirectory = rootDirectory;
		}

		/// <summary>
		/// Loads an asset.
		/// </summary>
		/// <typeparam name="T">Type of the asset.</typeparam>
		/// <param name="name">Name of the asset. Can contain path information.</param>
		/// <returns>Loaded asset.</returns>
		public T Load<T>(string name)
			where T : IAsset
		{
			var extensions = GetValidExtensions<T>();

			string fileName = null;
			string selectedExtension = null;
			foreach(var extension in extensions)
			{
				fileName = this.RootDirectory + "/" + name + extension;
				if (!File.Exists(fileName))
					continue;
				selectedExtension = extension;
				break;
			}
			if (selectedExtension == null)
				throw new AssetNotFoundException(name);

			T asset = Activator.CreateInstance<T>();
			using (var stream = File.Open(fileName, FileMode.Open))
			{
				// Explicit cast to get also hidden interface implementations
				IAsset a = (IAsset)asset;
				a.Load(new AssetLoadContext(this, Path.GetDirectoryName(name) + "/"), stream, selectedExtension);
			}
			return asset;
		}

		private static string[] GetValidExtensions<T>() where T : IAsset
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
		/// Gets or sets the root directory.
		/// </summary>
		public string RootDirectory { get; set; }
	}
}
