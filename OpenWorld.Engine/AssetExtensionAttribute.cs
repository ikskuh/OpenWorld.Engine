using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine
{
	/// <summary>
	/// An attribute that defines the extenions allowed for the asset type.
	/// <remarks>Should be attached to custom IAsset implementations.</remarks>
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple=false, Inherited=false)]
	public sealed class AssetExtensionAttribute : Attribute
	{
		private readonly string[] extensions;

		/// <summary>
		/// Instantiates a new asset extension.
		/// </summary>
		/// <param name="extensions"></param>
		public AssetExtensionAttribute(params string[] extensions)
		{
			this.extensions = extensions;
		}

		/// <summary>
		/// Gets the extensions specified.
		/// </summary>
		/// <returns></returns>
		public string[] GetExtensions() { return this.extensions; }
	}
}
