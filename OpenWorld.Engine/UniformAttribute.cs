using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines a shader uniform.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
	public sealed class UniformAttribute : Attribute
	{
		readonly string uniformName;

		/// <summary>
		/// Defines a new shader uniform.
		/// </summary>
		/// <param name="uniformName">Name of the uniform</param>
		public UniformAttribute(string uniformName)
		{
			this.uniformName = uniformName;
		}

		/// <summary>
		/// Gets the name of the uniform that should be assigned.
		/// </summary>
		public string UniformName
		{
			get { return uniformName; }
		}

		/// <summary>
		/// Gets or sets a value that indicates wheather a uniform matrix should be transposed or not.
		/// </summary>
		public bool Transpose { get; set; }
	}
}
