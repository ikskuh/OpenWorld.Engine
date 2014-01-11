using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Represents an OpenGL buffer.
	/// </summary>
	public sealed class Buffer : IGLResource
	{
		BufferTarget target;
		int id;

		/// <summary>
		/// Instantiates a new OpenGL buffer.
		/// </summary>
		/// <param name="target">The buffer target.</param>
		public Buffer(BufferTarget target)
		{
			this.target = target;
			GL.GenBuffers(1, out this.id);
		}
		
/// <summary>
/// Binds the buffer.
/// </summary>
		public void Bind()
		{
			if (this.id == 0)
				throw new InvalidOperationException("Buffer was disposed.");
			GL.BindBuffer(this.target, this.id);
		}

		/// <summary>
		/// Sets the buffer data.
		/// </summary>
		/// <typeparam name="T">Type of the buffer data.</typeparam>
		/// <param name="hint">Usage hint of the data.</param>
		/// <param name="data">The data to be filled in the buffer.</param>
		public void SetData<T>(BufferUsageHint hint, T[] data)
			where T : struct
		{
			if (data == null)
				throw new ArgumentNullException("data");
			this.Bind();
			GL.BufferData<T>(this.target, (IntPtr)(Marshal.SizeOf(typeof(T)) * data.Length), data, hint);
		}

		/// <summary>
		/// Disposes the buffer.
		/// </summary>
		public void Dispose()
		{
			if(this.id != 0)
			{
				GL.DeleteBuffers(1, ref this.id);
				this.id = 0;
			}
		}

		/// <summary>
		/// Gets the OpenGL buffer id.
		/// </summary>
		public int Id { get { return this.id; } }
	}
}
