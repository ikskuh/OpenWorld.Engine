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
			private readonly Dictionary<Type, Dictionary<string, Asset>> cache;

			/// <summary>
			/// Creates a new asset cache.
			/// </summary>
			public AssetCache()
			{
				this.cache = new Dictionary<Type, Dictionary<string, Asset>>();
			}

			private Dictionary<string, Asset> GetCache<T>()
				where T : Asset
			{
				lock (this.cache)
				{
					if (!this.cache.ContainsKey(typeof(T)))
						this.cache.Add(typeof(T), new Dictionary<string, Asset>());
					return this.cache[typeof(T)];
				}
			}

			/// <summary>
			/// Checks if the cache contains an asset.
			/// </summary>
			/// <typeparam name="T"></typeparam>
			/// <param name="name"></param>
			/// <returns></returns>
			public bool Contains<T>(string name)
				where T : Asset
			{
				return this.GetCache<T>().ContainsKey(name);
			}

			/// <summary>
			/// Adds an asset to the cache.
			/// </summary>
			/// <typeparam name="T"></typeparam>
			/// <param name="name"></param>
			/// <param name="asset"></param>
			public void Add<T>(string name, Asset asset)
				where T : Asset
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
				where T : Asset
			{
				return (T)this.GetCache<T>()[name];
			}
		}
	}
}
