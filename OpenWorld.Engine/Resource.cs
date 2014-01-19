using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Provides methods for loading embedded resources.
	/// </summary>
	public static class Resource
	{
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
