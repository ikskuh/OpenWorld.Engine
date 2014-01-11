using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Represents a frame buffer.
	/// </summary>
	public sealed class FrameBuffer : IGLResource
	{
		int id;
		Texture[] textures = null;

		// TODO: Restructure frame buffer usage, so Use is no longer needed, just Bind.

		/// <summary>
		/// Instantiates a new frame buffer.
		/// </summary>
		public FrameBuffer()
		{
			GL.GenFramebuffers(1, out this.id);
		}

		/// <summary>
		/// Binds the frame buffer.
		/// </summary>
		public void Bind()
		{
			GL.BindFramebuffer(FramebufferTarget.Framebuffer, this.id);
		}

		/// <summary>
		/// Binds the frame buffer and sets all the parameters.
		/// </summary>
		public void Use()
		{
			this.Bind();

			// Setup depth buffer
			int depthBufferID = 0;
			if (this.DepthBuffer != null)
				depthBufferID = this.DepthBuffer.Id;
			GL.FramebufferRenderbuffer(
				FramebufferTarget.Framebuffer, 
				FramebufferAttachment.DepthAttachment, 
				RenderbufferTarget.Renderbuffer, 
				depthBufferID);

			if(this.textures != null)
			{
				DrawBuffersEnum[] drawBuffers = new DrawBuffersEnum[this.textures.Length];
				for (int i = 0; i < this.textures.Length; i++)
				{
					GL.FramebufferTexture(
						FramebufferTarget.Framebuffer,
						FramebufferAttachment.ColorAttachment0 + i, 
						this.textures[i].Id, 0);
					drawBuffers[i] = DrawBuffersEnum.ColorAttachment0 + i;
				}

				GL.DrawBuffers(drawBuffers.Length, drawBuffers);
			}
			else
			{
				GL.DrawBuffers(0, new DrawBuffersEnum[0]);
			}

			FramebufferErrorCode error;
			if((error = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer)) != FramebufferErrorCode.FramebufferComplete)
				throw new InvalidOperationException("Failed to bind FrameBuffer: " + error);
		}

		/// <summary>
		/// Sets the render targets.
		/// </summary>
		/// <param name="textureList">Array of textures that should be used as a render target.</param>
		public void SetTextures(params Texture2D[] textureList)
		{
			this.textures = textureList;
		}

		/// <summary>
		/// Disposes the frame buffer.
		/// </summary>
		public void Dispose()
		{
			if (this.id != 0)
				GL.DeleteFramebuffers(1, ref this.id);
			this.id = 0;
		}

		/// <summary>
		/// Gets or sets the depth buffer.
		/// </summary>
		public RenderBuffer DepthBuffer { get; set; }

		/// <summary>
		/// Gets the OpenGL resource id.
		/// </summary>
		public int Id { get { return this.id; } }

		/// <summary>
		/// Unbinds the current frame buffer.
		/// </summary>
		public static void Unbind()
		{
			GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
		}
	}
}