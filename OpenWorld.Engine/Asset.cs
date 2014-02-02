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
		/// <summary>
		/// Loads the asset.
		/// </summary>
		/// <param name="context">The loading context.</param>
		/// <param name="stream">The stream that contains the asset.</param>
		/// <param name="extensionHint">Contains the extension of the asset file.</param>
		protected abstract void Load(AssetLoadContext context, Stream stream, string extensionHint);

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
				});
			return a;
		}

		/// <summary>
		/// Gets a value that indicates wheather the asset is currently loading.
		/// </summary>
		public bool IsLoading { get; private set; }

		/// <summary>
		/// Gets a value that indicates wheather the asset is loaded or not.
		/// </summary>
		public bool IsLoaded { get; private set; }
	}
}
