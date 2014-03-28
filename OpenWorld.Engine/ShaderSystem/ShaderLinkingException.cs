using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// An exception that is thrown when a shader linking error occurs.
	/// </summary>
	[Serializable]
	public sealed class ShaderLinkingException : Exception
	{
		internal ShaderLinkingException()
		{

		}

		internal ShaderLinkingException(string uniformName)
			: this(uniformName, null)
		{

		}

		internal ShaderLinkingException(string infoLog, Exception innerException)
			: base(infoLog, innerException)
		{

		}

		private ShaderLinkingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			
		}
	}
}
