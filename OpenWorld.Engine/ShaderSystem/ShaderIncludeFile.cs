using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.ShaderSystem
{
	/// <summary>
	/// Defines a shader include file (*.shi).
	/// </summary>
	[AssetExtension(".shi")]
	public sealed class ShaderIncludeFile : Asset
	{
		/// <summary>
		/// Loads the content of the file.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="stream"></param>
		/// <param name="extensionHint"></param>
		protected override void Load(AssetLoadContext context, System.IO.Stream stream, string extensionHint)
		{
			using(var sr = new StreamReader(stream))
			{
				this.Content = sr.ReadToEnd();
			}
		}

		/// <summary>
		/// Gets the content of the file.
		/// </summary>
		public string Content { get; private set; }
	}
}
