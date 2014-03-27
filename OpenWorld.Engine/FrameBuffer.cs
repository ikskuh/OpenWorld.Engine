using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Represents a frame buffer.
	/// </summary>
	public sealed class FrameBuffer : IGLResource
	{
		static ThreadLocal<FrameBuffer> current = new ThreadLocal<FrameBuffer>(() => null);

		/// <summary>
		/// Gets or sets the current frame buffer.
		/// </summary>
		public static FrameBuffer Current
		{
			get
			{
				if (!Game.IsThread(EngineThreadType.Render)) throw new InvalidOperationException("Can't be used outside the engine draw thread.");
				return current.Value;
			}
			set
			{
				if (!Game.IsThread(EngineThreadType.Render)) throw new InvalidOperationException("Can't be used outside the engine draw thread.");
				current.Value = value;
				if (value != null)
					GL.BindFramebuffer(FramebufferTarget.Framebuffer, value.id);
				else
					GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
			}
		}

		int id;
		RenderBuffer depthBuffer;
		Texture[] textures = null;

		/// <summary>
		/// Instantiates a new frame buffer.
		/// </summary>
		public FrameBuffer()
		{
			OpenGL.Invoke(() =>
				{
					GL.GenFramebuffers(1, out this.id);
				});
		}

		/// <summary>
		/// Creates a new frame buffer and sets the textures.
		/// </summary>
		/// <param name="textures"></param>
		public FrameBuffer(params Texture2D[] textures)
			: this()
		{
			this.SetTextures(textures);
		}

		/// <summary>
		/// Creates a new frame buffer and sets the textures.
		/// </summary>
		/// <param name="depthBuffer"></param>
		/// <param name="textures"></param>
		public FrameBuffer(RenderBuffer depthBuffer, params Texture2D[] textures)
			: this()
		{
			this.SetTextures(depthBuffer, textures);
		}

		/// <summary>
		/// Binds the frame buffer.
		/// </summary>
		public void Bind()
		{
			FrameBuffer.Current = this;
		}

		/// <summary>
		/// Sets the render targets with no depth buffer
		/// </summary>
		/// <param name="textureList">Array of textures that should be used as a render target.</param>
		public void SetTextures(params Texture2D[] textureList)
		{
			this.SetTextures(null, textureList);
		}

		/// <summary>
		/// Sets the render targets.
		/// </summary>
		/// <param name="depthBuffer">Depth buffer for this frame buffer</param>
		/// <param name="textureList">Array of textures that should be used as a render target.</param>
		public void SetTextures(RenderBuffer depthBuffer, params Texture2D[] textureList)
		{
			this.depthBuffer = depthBuffer;
			this.textures = textureList;
			OpenGL.Invoke(() =>
				{
					// Setup depth buffer
					GL.BindFramebuffer(FramebufferTarget.Framebuffer, this.id);
					int depthBufferID = 0;
					if (this.depthBuffer != null)
						depthBufferID = this.depthBuffer.Id;
					GL.FramebufferRenderbuffer(
						FramebufferTarget.Framebuffer,
						FramebufferAttachment.DepthAttachment,
						RenderbufferTarget.Renderbuffer,
						depthBufferID);

					if (this.textures != null)
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
					if ((error = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer)) != FramebufferErrorCode.FramebufferComplete)
						throw new InvalidOperationException("Failed to bind FrameBuffer: " + error);
				});
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
		/// Gets the OpenGL resource id.
		/// </summary>
		public int Id { get { return this.id; } }

		static Color clearColor = Color.Black;
		static float clearDepth = 1.0f;

		/// <summary>
		/// Gets or sets the current clear color.
		/// </summary>
		public static Color ClearColor
		{
			get { return FrameBuffer.clearColor; }
			set
			{
				FrameBuffer.clearColor = value;
				GL.ClearColor(FrameBuffer.clearColor);
			}
		}

		/// <summary>
		/// Gets or sets the current clear depth.
		/// </summary>
		public static float ClearDepth
		{
			get { return FrameBuffer.clearDepth; }
			set
			{
				FrameBuffer.clearDepth = value;
				GL.ClearDepth(FrameBuffer.clearDepth);
			}
		}

		/// <summary>
		/// Clears color and depth from the current frame buffer.
		/// </summary>
		public static void Clear()
		{
			FrameBuffer.Clear(true, true);
		}

		/// <summary>
		/// Clears the current frame buffer.
		/// </summary>
		/// <param name="color">Indicates wheather the color should be cleared or not.</param>
		/// <param name="depth">Indicates wheather the depth should be cleared or not.</param>
		public static void Clear(bool color, bool depth)
		{
			FrameBuffer.Clear(color, depth, false);
		}

		/// <summary>
		/// Clears the current frame buffer.
		/// </summary>
		/// <param name="color">Indicates wheather the color should be cleared or not.</param>
		/// <param name="depth">Indicates wheather the depth should be cleared or not.</param>
		/// <param name="stencil">Indicates wheather the stencil should be cleared or not.</param>
		public static void Clear(bool color, bool depth, bool stencil)
		{
			ClearBufferMask mask = ClearBufferMask.None;
			if (color) mask |= ClearBufferMask.ColorBufferBit;
			if (depth) mask |= ClearBufferMask.DepthBufferBit;
			if (stencil) mask |= ClearBufferMask.StencilBufferBit;

			if (mask != ClearBufferMask.None)
				GL.Clear(mask);
		}
	}
}