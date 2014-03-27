using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Provides methods and informations about OpenGL.
	/// </summary>
	public static class OpenGL
	{
		/// <summary>
		/// Invokes a method call in the OpenGL thread.
		/// </summary>
		/// <param name="routine"></param>
		public static void Invoke(DeferredRoutine routine)
		{
			// Don't make warning "Obsolete"
#pragma warning disable 618
			Game.Current.InvokeOpenGL(routine);
#pragma warning restore 618
		}
	}
}
