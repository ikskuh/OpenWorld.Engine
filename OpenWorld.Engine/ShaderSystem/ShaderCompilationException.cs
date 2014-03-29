using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace OpenWorld.Engine.ShaderSystem
{
	/// <summary>
	/// An exception that is thrown when a shader compilation fails.
	/// </summary>
	[Serializable]
	public sealed class ShaderCompilationException : Exception
	{
		internal ShaderCompilationException()
		{

		}

		internal ShaderCompilationException(string uniformName)
			: this(uniformName, null)
		{

		}

		internal ShaderCompilationException(string infoLog, Exception innerException)
			: base(infoLog, innerException)
		{

		}

		private ShaderCompilationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{

		}
	}
}
