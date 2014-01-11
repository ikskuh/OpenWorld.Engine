using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines an OpenGL resource.
	/// </summary>
	public interface IGLResource : IDisposable
	{
		/// <summary>
		/// Gets the resource identifier of the OpenGL resource.
		/// </summary>
		int Id { get; }

		/// <summary>
		/// Binds the OpenGL resource.
		/// </summary>
		void Bind();
	}
}
