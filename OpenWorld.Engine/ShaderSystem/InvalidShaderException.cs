using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace OpenWorld.Engine.ShaderSystem
{
	/// <summary>
	/// An exception that is thrown when a shader is not valid.
	/// </summary>
	[Serializable]
	public sealed class InvalidShaderException : Exception
	{
		internal InvalidShaderException()
		{

		}

		internal InvalidShaderException(string uniformName)
			: this(uniformName, null)
		{

		}

		internal InvalidShaderException(string infoLog, Exception innerException)
			: base(infoLog, innerException)
		{

		}

		private InvalidShaderException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{

		}
	}
}
