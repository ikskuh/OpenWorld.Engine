using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Represents an OpenGL render buffer.
	/// </summary>
	public sealed class RenderBuffer : IGLResource
	{
		int id;

		/// <summary>
		/// Instantiates a new render buffer.
		/// </summary>
		public RenderBuffer()
		{
			OpenGL.Invoke(() =>
				{
					GL.GenRenderbuffers(1, out this.id);
				});
		}

		/// <summary>
		/// Instantiates a new render buffer with a given size.
		/// <param name="width">Width of the buffer.</param>
		/// <param name="height">Height of the buffer.</param>
		/// </summary>
		public RenderBuffer(int width, int height)
			: this()
		{
			this.SetSize(width, height);
		}

		/// <summary>
		/// Binds the render buffer.
		/// </summary>
		public void Bind()
		{
			GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, this.id);
		}

		/// <summary>
		/// Sets the size of the render buffer.
		/// </summary>
		/// <param name="width">Width of the buffer.</param>
		/// <param name="height">Height of the buffer.</param>
		public void SetSize(int width, int height)
		{
			OpenGL.Invoke(() =>
				{
					this.Bind();
					GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.DepthComponent24, width, height);
					RenderBuffer.Unbind();
				});
			this.Width = width;
			this.Height = height;
		}

		/// <summary>
		/// Disposes the render buffer.
		/// </summary>
		public void Dispose()
		{
			if (this.id != 0)
				GL.DeleteRenderbuffers(1, ref this.id);
			this.id = 0;
		}

		/// <summary>
		/// Gets the width of the render buffer.
		/// </summary>
		public int Width { get; private set; }

		/// <summary>
		/// Gets the height of the render buffer.
		/// </summary>
		public int Height { get; private set; }

		/// <summary>
		/// Unbinds the currently bound render buffer.
		/// </summary>
		public static void Unbind()
		{
			GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
		}

		/// <summary>
		/// Gets the OpenGL render buffer id.
		/// </summary>
		public int Id { get { return this.id; } }
	}
}
