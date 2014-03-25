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
	public abstract class Asset
	{
		internal string assetName;

		/// <summary>
		/// Creates an asset.
		/// </summary>
		protected Asset()
		{
			this.assetName = this.GetType().Name;
		}

		/// <summary>
		/// Gets fired when the asset is loaded.
		/// </summary>
		public event EventHandler Loaded;

		/// <summary>
		/// Loads the asset.
		/// </summary>
		/// <param name="context">The loading context.</param>
		/// <param name="stream">The stream that contains the asset.</param>
		/// <param name="extensionHint">Contains the extension of the asset file.</param>
		protected abstract void Load(AssetLoadContext context, Stream stream, string extensionHint);

		/// <summary>
		/// Unloads the asset.
		/// </summary>
		public void Unload()
		{
			if (!this.IsLoaded)
				throw new InvalidOperationException("Cannot unload a not loaded resource.");
			this.IsLoaded = false;
			this.OnUnload();
		}

		/// <summary>
		/// Unloads the asset.
		/// </summary>
		protected virtual void OnUnload()
		{

		}

		/// <summary>
		/// Gets a value that indicates wheather the asset is currently loading.
		/// </summary>
		public bool IsLoading { get; private set; }

		/// <summary>
		/// Gets a value that indicates wheather the asset is loaded or not.
		/// </summary>
		public bool IsLoaded { get; protected set; }

		/// <summary>
		/// Gets the name of the asset.
		/// </summary>
		public string AssetName { get { return this.assetName; } }

		/// <summary>
		/// Loads a new asset.
		/// </summary>
		/// <typeparam name="T">Type of the asset to be loaded</typeparam>
		/// <param name="context">The load context.</param>
		/// <param name="stream">The stream that is used for loading the asset</param>
		/// <param name="extensionHint">The extension of the asset to be loaded.</param>
		/// <remarks>The asset is not instantly loaded. Wait for Asset.IsLoaded to be true.</remarks>
		/// <returns>New asset.</returns>
		public static T Load<T>(AssetLoadContext context, Stream stream, string extensionHint)
			where T : Asset
		{
			T a = Activator.CreateInstance<T>();
			a.IsLoading = true;
			Game.Current.DeferRoutine(() =>
			{
				a.Load(context, stream, extensionHint);
				a.IsLoading = false;
				a.IsLoaded = true;
				if (stream != null)
					stream.Close();
				if (a.Loaded != null)
					a.Loaded(a, EventArgs.Empty);
			});
			return a;
		}

		/// <summary>
		/// Loads a new asset synchronous
		/// </summary>
		/// <typeparam name="T">Type of the asset to be loaded</typeparam>
		/// <param name="context">The load context.</param>
		/// <param name="stream">The stream that is used for loading the asset</param>
		/// <param name="extensionHint">The extension of the asset to be loaded.</param>
		/// <remarks>The asset is not instantly loaded. Wait for Asset.IsLoaded to be true.</remarks>
		/// <returns>New asset.</returns>
		public static T LoadSync<T>(AssetLoadContext context, Stream stream, string extensionHint)
			where T : Asset
		{
			T a = Activator.CreateInstance<T>();
			a.IsLoading = true;
			a.Load(context, stream, extensionHint);
			a.IsLoading = false;
			a.IsLoaded = true;
			if (stream != null)
				stream.Close();
			if (a.Loaded != null)
				a.Loaded(a, EventArgs.Empty);
			return a;
		}
	}
}
