using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines a post processing pipeline.
	/// </summary>
	public sealed class PostProcessingPipeline : IDisposable
	{
		private readonly List<PostProcessingStage> stages = new List<PostProcessingStage>();
		private VertexArray vao;
		private Buffer vertexBuffer;
		private FrameBuffer frameBuffer;
		private Texture2D swapBuffer;
		private PostProcessingShader swapShader;

		/// <summary>
		/// Instantiates a new post processing pipeline.
		/// </summary>
		/// <param name="width">Screen width</param>
		/// <param name="height">Screen height></param>
		public PostProcessingPipeline(int width, int height)
		{
			Game.Current.InvokeOpenGL(() =>
			   {
				   this.vertexBuffer = new Buffer(BufferTarget.ArrayBuffer);
				   this.vertexBuffer.SetData<float>(BufferUsageHint.StaticDraw, new[]
				{
					-1.0f, -1.0f,
					-1.0f, 1.0f,
					1.0f, -1.0f,
					1.0f, 1.0f
				});

				   this.vao = new VertexArray();
				   this.vao.Bind();
				   this.vertexBuffer.Bind();

				   GL.EnableVertexAttribArray(0);
				   GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 0, 0);

				   VertexArray.Unbind();

				   this.frameBuffer = new FrameBuffer();
			   });

			this.swapBuffer = new Texture2D(width, height, PixelInternalFormat.Rgb16f, PixelFormat.Rgba, PixelType.Float);

			// Blank post processing shader that just copies the buffer
			this.swapShader = new PostProcessingShader();
		}

		/// <summary>
		/// Applies the pipeline to a back buffer.
		/// </summary>
		/// <param name="backBuffer"></param>
		public void Apply(Texture2D backBuffer)
		{
			GL.Disable(EnableCap.Blend);
			GL.Disable(EnableCap.DepthTest);
			GL.Disable(EnableCap.CullFace);

			this.vao.Bind();

			Texture2D currentTarget = this.swapBuffer;
			Texture2D currentBuffer = backBuffer;

			foreach (var ppEffect in this.stages)
			{
				if (!ppEffect.Enabled)
					continue;
				if (ppEffect.Effect == null)
					continue;
				this.frameBuffer.SetTextures(currentTarget);
				this.frameBuffer.Use();

				ppEffect.Effect.BackBuffer = currentBuffer;
				ppEffect.Effect.Use();
				
                GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);

				SwapBuffers(ref currentTarget, ref currentBuffer);
			}
			// If we haven't rendered to the backBuffer last, swap again
			if (currentTarget == backBuffer)
			{
				this.frameBuffer.SetTextures(currentTarget);
				this.frameBuffer.Use();

				this.swapShader.BackBuffer = currentBuffer;
				this.swapShader.Use();

                GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);

				SwapBuffers(ref currentTarget, ref currentBuffer);
			}
			FrameBuffer.Unbind();
			VertexArray.Unbind();
		}

		/// <summary>
		/// Draws a fullscreen quad with the render pipeline.
		/// </summary>
		/// <param name="texture">The texture to be drawn</param>
		public void DrawQuad(Texture2D texture)
		{
			this.vao.Bind();

			this.swapShader.BackBuffer = texture;
			this.swapShader.Use();

			GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);

			VertexArray.Unbind();
		}

		private static void SwapBuffers(ref Texture2D currentTarget, ref Texture2D currentBuffer)
		{
			var temp = currentTarget;
			currentTarget = currentBuffer;
			currentBuffer = temp;
		}

		/// <summary>
		/// Gets a collection of post processing stages.
		/// </summary>
		public ICollection<PostProcessingStage> Stages
		{
			get { return stages; }
		}

		/// <summary>
		/// Disposes the post processing pipeline.
		/// </summary>
		public void Dispose()
		{
			Game.Current.InvokeOpenGL(() =>
			   {
				   if (this.vertexBuffer != null)
					   this.vertexBuffer.Dispose();
				   if (this.vao != null)
					   this.vao.Dispose();

				   if (this.frameBuffer != null)
					   this.frameBuffer.Dispose();
				   if (this.swapBuffer != null)
					   this.swapBuffer.Dispose();
				   if (this.swapShader != null)
					   this.swapShader.Dispose();
			   });
			this.vertexBuffer = null;
			this.vao = null;
			this.frameBuffer = null;
			this.swapBuffer = null;
			this.swapShader = null;
		}
	}
}
