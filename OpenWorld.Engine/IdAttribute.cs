using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines an id for a property.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property,  AllowMultiple=false, Inherited=false)]
	public sealed class IDAttribute : Attribute
	{
		string id;

		/// <summary>
		/// Defines an id for a property.
		/// </summary>
		/// <param name="id">The id of the property.</param>
		public IDAttribute(string id)
		{
			this.id = id;
		}

		/// <summary>
		/// Gets the id defined by this attribute
		/// </summary>
		public string ID { get { return this.id; } }
	}
}
