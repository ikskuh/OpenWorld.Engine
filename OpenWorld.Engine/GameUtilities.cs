using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Provides a set of utility functions.
	/// </summary>
	public sealed class GameUtilities
	{
		private readonly Game game;
		private Buffer vertexBuffer;
		private VertexArray vao;
		private PostProcessingShader quadShader;

		internal GameUtilities(Game game)
		{
			this.game = game;
			this.Initialize();
		}

		private void Initialize()
		{
			game.InvokeOpenGL(() =>
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

					this.quadShader = new PostProcessingShader("layout(location = 0) out vec4 result; void main() { result = texture(backBuffer, vec2(uv.x, 1.0f - uv.y)); }");
				});
		}


		/// <summary>
		/// Draws a textured quad on the screen.
		/// </summary>
		/// <param name="box2">Area to be drawn in.</param>
		/// <param name="texture2D">Texture to be drawn.</param>
		public void Draw(Box2 box2, Texture2D texture2D)
		{
			GL.Disable(EnableCap.DepthTest);
			GL.Disable(EnableCap.CullFace);
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			this.vao.Bind();

			this.quadShader.BackBuffer = texture2D;
			this.quadShader.Use();

			GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);

			VertexArray.Unbind();
		}
	}
}
