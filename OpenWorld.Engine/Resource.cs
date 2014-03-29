using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Provides methods for loading embedded resources.
	/// </summary>
	public class Resource 
	{
		static Resource engine;

		/// <summary>
		/// Gets the engine resources.
		/// </summary>
		public static Resource Engine
		{
			get
			{
				if (engine == null)
					engine = new Resource();
				return engine;
			}
		}

		private Resource()
		{
			this.MissingTexture = new Texture2D(Resource.Open("OpenWorld.Engine.Resources.missing.png"));
			this.MissingTexture.Filter = Filter.Nearest;
		}
		
		/// <summary>
		/// Gets the texture that is assigned if something is missing.
		/// </summary>
		public Texture2D MissingTexture { get; private set; }

		/// <summary>
		/// Opens a resource stream for an embedded resource in the current assembly.
		/// </summary>
		/// <param name="name">The name of the resource.</param>
		/// <returns></returns>
		public static Stream Open(string name)
		{
			return Assembly.GetCallingAssembly().GetManifestResourceStream(name);
		}

		/// <summary>
		/// Gets an embedded resource as a string.
		/// </summary>
		/// <param name="name">The name of the resource.</param>
		/// <returns></returns>
		public static string GetString(string name)
		{
			return GetString(name, Encoding.Default);
		}

		/// <summary>
		/// Gets an embedded resource as a string.
		/// </summary>
		/// <param name="name">The name of the resource.</param>
		/// <param name="encoding">The encoding of the resource.</param>
		/// <returns></returns>
		public static string GetString(string name, Encoding encoding)
		{
			using (var stream = Open(name))
			{
				return new StreamReader(stream, encoding).ReadToEnd();
			}
		}
	}
}
