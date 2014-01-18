using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Specifies that a component needs another component to function.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public sealed class RequiredComponentAttribute : Attribute
	{
		readonly Type requiredType;

		/// <summary>
		/// Specifies the component requirement.
		/// </summary>
		/// <param name="requiredType">Type of the component required.</param>
		public RequiredComponentAttribute(Type requiredType)
		{
			this.requiredType = requiredType;
		}

		/// <summary>
		/// Gets the required component type.
		/// </summary>
		public Type RequiredType
		{
			get { return requiredType; }
		}
	}
}
