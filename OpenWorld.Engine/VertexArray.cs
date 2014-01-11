using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Represents an OpenGL vertex array object.
	/// </summary>
	public sealed class VertexArray : IGLResource
	{
		private int id;

		/// <summary>
		/// Instantiates a new vertex array object.
		/// </summary>
		public VertexArray()
		{
			GL.GenVertexArrays(1, out this.id);
		}

		/// <summary>
		/// Binds the vertex array object.
		/// </summary>
		public void Bind()
		{
			GL.BindVertexArray(this.id);
		}

		/// <summary>
		/// Disposes the vertex array object.
		/// </summary>
		public void Dispose()
		{
			if(this.id != 0)
			{
				GL.DeleteVertexArrays(1, ref this.id);
				this.id = 0;
			}
		}

		/// <summary>
		/// Gets the OpenGL id of the vertex array object.
		/// </summary>
		public int Id { get { return this.id; } }

		/// <summary>
		/// Unbinds the active vertex array object.
		/// </summary>
		public static void Unbind()
		{
			GL.BindVertexArray(0);
		}
	}
}
