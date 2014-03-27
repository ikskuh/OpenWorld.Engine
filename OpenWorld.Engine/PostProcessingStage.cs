using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Represents a post processing stage.
	/// </summary>
	public sealed class PostProcessingStage
	{
		class PPSTools
		{
			public VertexArray vao;
			public Buffer vertexBuffer;
			public FrameBuffer frameBuffer;

			public PPSTools()
			{
				OpenGL.Invoke(() =>
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
			}
		}

		ThreadLocal<PPSTools> tools = new ThreadLocal<PPSTools>(() => new PPSTools());

		/// <summary>
		/// Instantiates an empty post processing stage.
		/// </summary>
		public PostProcessingStage()
		{

		}

		/// <summary>
		/// Instantiates a new post processing stage with an effect.
		/// </summary>
		/// <param name="effect">The effect for this stage.</param>
		public PostProcessingStage(PostProcessingShader effect)
			: this()
		{
			this.Effect = effect;
		}

		/// <summary>
		/// Instantiates a new post processing stage with an effect.
		/// </summary>
		/// <param name="effect">The effect for this stage.</param>
		/// <param name="enabled">Determines if the post processing stage is enabled.</param>
		public PostProcessingStage(PostProcessingShader effect, bool enabled)
		{
			this.Effect = effect;
		}

		/// <summary>
		/// Renders the post processing stage and all linked stages.
		/// </summary>
		/// <returns>TargetTexture of the last linked post processing stage.</returns>
		public Texture2D Render(Texture2D sourceTexture)
		{
			if (!Game.IsThread(EngineThreadType.Render)) throw new InvalidOperationException("Can't be invoked outside of render thread.");
			GL.Disable(EnableCap.Blend);
			GL.Disable(EnableCap.DepthTest);
			GL.Disable(EnableCap.CullFace);

			var stage = this;
			stage.SourceTexture = sourceTexture;

			FrameBuffer renderTarget = FrameBuffer.Current;
			this.tools.Value.vao.Bind();
			Viewport.Push();
			while(stage != null)
			{
				if (stage.Effect == null)
					throw new InvalidOperationException("One or more post processing stages are missing a shader.");
				if (stage.TargetTexture == null)
					throw new InvalidOperationException("One or more post processing stages are missing a target texture.");

				Viewport.Area = new Box2i(0, 0, stage.TargetTexture.Width, stage.TargetTexture.Height);
				this.tools.Value.frameBuffer.SetTextures(stage.TargetTexture);
				this.tools.Value.frameBuffer.Bind();

				stage.Effect.InvertY = true;
				stage.Effect.Use(stage);

				GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);

				if (stage.Stage == null)
					break;
				else
				{
					stage.Stage.SourceTexture = stage.TargetTexture;
					stage = stage.Stage;
				}
			};
			Viewport.Pop();
			VertexArray.Unbind();
			FrameBuffer.Current = renderTarget;

			return stage.TargetTexture;
		}

		/// <summary>
		/// Gets or sets the post processing effect.
		/// </summary>
		public PostProcessingShader Effect { get; set; }

		/// <summary>
		/// Gets or sets the source texture. The stage shader will get its inputTexture from this.
		/// </summary>
		[Uniform("inputTexture")]
		public Texture2D SourceTexture { get; set; }

		/// <summary>
		/// Gets or sets the target texture. The stage will render its result in this texture.
		/// </summary>
		public Texture2D TargetTexture { get; set; }

		/// <summary>
		/// Gets or sets the post processing stage that is chained to this stage.
		/// </summary>
		/// <remarks>Stage builds a linked list that is called in Render.</remarks>
		public PostProcessingStage Stage { get; set; }

		public Texture2D FinalTexture
		{
			get
			{
				var stage = this;
				while (stage.Stage != null) stage = stage.Stage;
				return stage.TargetTexture;
			}
		}
	}
}
