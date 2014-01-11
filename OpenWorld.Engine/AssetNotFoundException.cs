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
		internal AssetNotFoundException()
			: base("An asset was not found")
		{

		}
		internal AssetNotFoundException(string name)
		: this(name, null)
		{

		}

		internal AssetNotFoundException(string name, Exception inner)
			: base("Asset '" + name + "' not found", inner)
		{

		}

		private AssetNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
