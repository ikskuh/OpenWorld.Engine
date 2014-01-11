using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// An exception that is thrown if a uniform was not found.
	/// </summary>
	[Serializable]
	public sealed class UniformNotFoundException : Exception
	{
		internal UniformNotFoundException()
			: base("A uniform was not found")
		{

		}

		internal UniformNotFoundException(string uniformName)
			: this(uniformName, null)
		{

		}

		internal UniformNotFoundException(string uniformName, Exception innerException)
			: base("The uniform '" + uniformName + "' was not found.", innerException)
		{

		}

		private UniformNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{

		}
	}
}
