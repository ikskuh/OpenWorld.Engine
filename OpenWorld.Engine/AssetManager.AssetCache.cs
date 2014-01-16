using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	partial class AssetManager
	{
		/// <summary>
		/// Defines an asset cache.
		/// </summary>
		private class AssetCache
		{
			private readonly Dictionary<Type, Dictionary<string, IAsset>> cache;

			/// <summary>
			/// Creates a new asset cache.
			/// </summary>
			public AssetCache()
			{
				this.cache = new Dictionary<Type, Dictionary<string, IAsset>>();
			}

			private Dictionary<string, IAsset> GetCache<T>()
			{
				if (!this.cache.ContainsKey(typeof(T)))
					this.cache.Add(typeof(T), new Dictionary<string, IAsset>());
				return this.cache[typeof(T)];
			}

			/// <summary>
			/// Checks if the cache contains an asset.
			/// </summary>
			/// <typeparam name="T"></typeparam>
			/// <param name="name"></param>
			/// <returns></returns>
			public bool Contains<T>(string name)
			{
				return this.GetCache<T>().ContainsKey(name);
			}

			/// <summary>
			/// Adds an asset to the cache.
			/// </summary>
			/// <typeparam name="T"></typeparam>
			/// <param name="name"></param>
			/// <param name="asset"></param>
			public void Add<T>(string name, IAsset asset)
			{
				this.GetCache<T>().Add(name, asset);
			}

			/// <summary>
			/// Gets an asset from the cache.
			/// </summary>
			/// <typeparam name="T"></typeparam>
			/// <param name="name"></param>
			/// <returns></returns>
			public T Get<T>(string name)
			{
				return (T)this.GetCache<T>()[name];
			}
		}
	}
}
