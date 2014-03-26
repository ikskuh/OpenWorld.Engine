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

		/// <summary>
		/// Gets or sets the default color of a texture uniform.
		/// </summary>
		public string DefaultColor { get; set; }

		/// <summary>
		/// Gets or sets wheather the default texture is in sRGB space or not.
		/// </summary>
		public SRGBType SRGB { get; set; }
	}

	/// <summary>
	/// Enumeration for different SRGB types.
	/// </summary>
	public enum SRGBType
	{
		/// <summary>
		/// Uses Texture.UseSRGB
		/// </summary>
		Default = 0,

		/// <summary>
		/// Texture is sRGB
		/// </summary>
		Yes,

		/// <summary>
		/// Texture is linear.
		/// </summary>
		No
	}

	/// <summary>
	/// Defines a shader uniform.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	public sealed class UniformPrefixAttribute : Attribute
	{
		readonly string prefix;

		/// <summary>
		/// Defines a new shader uniform.
		/// </summary>
		/// <param name="prefix">Name of the uniform</param>
		public UniformPrefixAttribute(string prefix)
		{
			this.prefix = prefix;
		}

		/// <summary>
		/// Gets the name of the uniform that should be assigned.
		/// </summary>
		public string Prefix
		{
			get { return prefix; }
		}
	}
}
