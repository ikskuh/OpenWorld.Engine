using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// An Exception that is thrown if an asset was not found.
	/// </summary>
	[Serializable]
	public sealed class AssetNotFoundException : Exception
	{
		/// <summary>
		/// Creates a new AssetNotFoundException
		/// </summary>
		public AssetNotFoundException()
			: base("An asset was not found")
		{

		}

		/// <summary>
		/// Creates a new AssetNotFoundException
		/// </summary>
		/// <param name="name">Name of the asset</param>
		public AssetNotFoundException(string name)
		: this(name, null)
		{

		}

		/// <summary>
		/// Creates a new AssetNotFoundException
		/// </summary>
		/// <param name="name">Name of the asset</param>
		/// <param name="inner">Inner exception</param>
		public AssetNotFoundException(string name, Exception inner)
			: base("Asset '" + name + "' not found", inner)
		{

		}

		private AssetNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
