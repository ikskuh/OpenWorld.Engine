using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine.UserInterface
{
	/// <summary>
	/// Defines a default renderer for a control.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class RendererAttribute : Attribute
	{
		readonly Type type;

		/// <summary>
		/// Instantiates a new RendererAttribute.
		/// </summary>
		/// <param name="rendererType">Type of the default renderer.</param>
		public RendererAttribute(Type rendererType)
		{
			this.type = rendererType;
		}

		/// <summary>
		/// Gets the default renderer type.
		/// </summary>
		public Type RendererType { get { return this.type; } }
	}
}
