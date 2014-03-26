using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenWorld.Engine.PostProcessingShaders;
using OpenWorld.Engine.SceneManagement.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Defines a deferred renderer.
	/// </summary>
	public sealed class DeferredRenderer : SceneRenderer
	{
		[UniformPrefix("light")]
		class LightUniforms
		{
			[Uniform("ViewPosition")]
			public Vector3 ViewPosition { get; set; }

			[Uniform("Position")]
			public Vector3 Position { get; set; }

			[Uniform("Radius")]
			public float Radius { get; set; }

			[Uniform("Color")]
			public Color Color { get; set; }
		}

		private FrameBuffer frameBuffer;

		/// <summary>
		/// Gets the texture that contains world positions.
		/// </summary>
		[Uniform("PositionBuffer")]
		public Texture2D PositionBuffer { get; private set; }

		/// <summary>
		/// Gets the texture that contains world normals.
		/// </summary>
		[Uniform("NormalBuffer")]
		public Texture2D NormalBuffer { get; private set; }

		/// <summary>
		/// Gets the texture that contains diffuse lighting.
		/// </summary>
		[Uniform("DiffuseLightBuffer")]
		public Texture2D DiffuseLightBuffer { get; private set; }

		/// <summary>
		/// Gets the texture that contains specular lighting.
		/// </summary>
		[Uniform("SpecularLightBuffer")]
		public Texture2D SpecularLightBuffer { get; private set; }

		private Texture2D resultBuffer;

		private PostProcessingPipeline pipeline;
		private TonemappingShader tonemappingShader;
		private GammaCorrectionShader gammaCorrectionShader;
		private LightScatteringShader lightScatteringShader;
		private DitheringShader ditheringShader;
		private PostProcessingStage lightScattering;
		private PostProcessingStage tonemapping;
		private PostProcessingStage gammaCorrection;
		private PostProcessingStage dithering;

		private GeometryShader geometryShader;
		private PointLightShader pointLightShader;
		private Model lightCube;

		/// <summary>
		/// Creates a new deferred renderer that uses the whole screen.
		/// </summary>
		public DeferredRenderer()
			: this(Game.Current.Size.Width, Game.Current.Size.Height)
		{

		}

		/// <summary>
		/// Creates a new deferred renderer.
		/// </summary>
		/// <param name="width">Width of the render targets.</param>
		/// <param name="height">Height of the render targets.</param>
		public DeferredRenderer(int width, int height)
		{
			this.lightCube = Model.CreateCube(1.1f);

			this.Width = width;
			this.Height = height;

			// Create graphics buffer
			this.PositionBuffer = new Texture2D(width, height, PixelInternalFormat.Rgba16f, PixelFormat.Rgba, PixelType.Float);
			this.NormalBuffer = new Texture2D(width, height, PixelInternalFormat.Rgba16f, PixelFormat.Rgba, PixelType.Float);
			this.DiffuseLightBuffer = new Texture2D(width, height, PixelInternalFormat.Rgba16f, PixelFormat.Rgba, PixelType.Float);
			this.SpecularLightBuffer = new Texture2D(width, height, PixelInternalFormat.Rgba16f, PixelFormat.Rgba, PixelType.Float);
			this.resultBuffer = new Texture2D(width, height, PixelInternalFormat.Rgba16f, PixelFormat.Rgba, PixelType.Float);

			// Create 3d shaders
			this.geometryShader = new GeometryShader();

			this.pointLightShader = new PointLightShader();

			// Create post processing shaders
			this.gammaCorrectionShader = new GammaCorrectionShader();
			this.gammaCorrectionShader.Gamma = 2.2f;

			this.lightScatteringShader = new LightScatteringShader();
			this.lightScatteringShader.OcclusionBuffer = this.NormalBuffer;

			this.ditheringShader = new DitheringShader();

			this.tonemappingShader = new TonemappingShader();
			this.tonemappingShader.HdrExposure = 0.8f;

			// Create post processing stages
			this.lightScattering = new PostProcessingStage(this.lightScatteringShader);
			this.tonemapping = new PostProcessingStage(this.tonemappingShader);
			this.gammaCorrection = new PostProcessingStage(this.gammaCorrectionShader);
			this.dithering = new PostProcessingStage(this.ditheringShader);

			// Create post processing pipeline
			this.pipeline = new PostProcessingPipeline(width, height);
			this.pipeline.Stages.Add(this.lightScattering);
			this.pipeline.Stages.Add(this.tonemapping);
			this.pipeline.Stages.Add(this.gammaCorrection);
			this.pipeline.Stages.Add(this.dithering);

			Game.Current.InvokeOpenGL(() =>
			{
				this.frameBuffer = new FrameBuffer();
				this.frameBuffer.DepthBuffer = new RenderBuffer(width, height);
			});

			this.DefaultShader = new OpaqueObjectShader();
		}

		/// <summary>
		/// Actually renders the scene.
		/// <param name="scene">The scene that should be rendered.</param>
		/// <param name="camera">The camera setting for the scene.</param>
		/// </summary>
		protected override void Render(Scene scene, Camera camera)
		{
			if (scene == null)
				throw new ArgumentNullException("scene");
			if (camera == null)
				throw new ArgumentNullException("camera");

			Viewport.Push();
			Viewport.Area = new Box2i(0, 0, this.Width, this.Height);

			GL.Enable(EnableCap.DepthTest);
			GL.Enable(EnableCap.TextureCubeMap);
			GL.Enable(EnableCap.TextureCubeMapSeamless);
			GL.DepthFunc(DepthFunction.Lequal);
			GL.Enable(EnableCap.CullFace);
			GL.ClearDepth(1.0f);
			GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

			//RenderLightMaps(ref camera, ref projection, out shadowMatrix, args);

			RenderGeometry(camera);
			RenderLights(camera);
			RenderOpaque(camera);
			//RenderTranslucent(ref camera, ref projection, args);

			var sunView = camera.ViewMatrix;
			sunView.M41 = 0; sunView.M42 = 0; sunView.M43 = 0;

			if (this.Sky != null)
			{
				Vector3 sunPosition = this.Sky.GetSunDirection() * 1000.0f;
				var projPos = Vector4.Transform(new Vector4(sunPosition, 1), sunView * camera.ProjectionMatrix);
				this.lightScatteringShader.LightPosition = new Vector2(0.5f, 0.5f) + 0.5f * (projPos.Xy * (1.0f / projPos.W));
				this.lightScattering.Enabled = false;// Math.Abs(this.lightScatteringShader.LightPosition.X) < 2.0f && Math.Abs(this.lightScatteringShader.LightPosition.Y) < 2.0f && projPos.Z > 0.0f;
			}
			else
			{
				this.lightScattering.Enabled = false;
			}

			this.pipeline.Apply(this.resultBuffer);

			GL.Disable(EnableCap.Blend);
			GL.Disable(EnableCap.DepthTest);
			GL.Disable(EnableCap.CullFace);

			FrameBuffer.Unbind();

			Viewport.Pop();

			if (Game.Current.Input.Keyboard[OpenTK.Input.Key.Number1])
			{
				this.pipeline.DrawQuad(this.NormalBuffer);
			}
			else if (Game.Current.Input.Keyboard[OpenTK.Input.Key.Number2])
			{
				this.pipeline.DrawQuad(this.DiffuseLightBuffer);
			}
			else if (Game.Current.Input.Keyboard[OpenTK.Input.Key.Number3])
			{
				this.pipeline.DrawQuad(this.SpecularLightBuffer);
			}
			else if (Game.Current.Input.Keyboard[OpenTK.Input.Key.Number4])
			{
				this.pipeline.DrawQuad(this.PositionBuffer);
			}
			else
			{
				this.pipeline.DrawQuad(this.resultBuffer);
			}
		}

		private void RenderGeometry(Camera camera)
		{
			this.Matrices.View = camera.ViewMatrix;
			this.Matrices.Projection = camera.ProjectionMatrix;

			GL.CullFace(CullFaceMode.Back);
			GL.Disable(EnableCap.Blend);
			GL.DepthFunc(DepthFunction.Less);

			this.frameBuffer.SetTextures(this.PositionBuffer, this.NormalBuffer);
			this.frameBuffer.Use();

			GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);

			this.geometryShader.Use();
			foreach (var job in this.SolidRenderJobs)
			{
				this.Matrices.World = job.Transform;

				geometryShader.Use(job.Material, this.Matrices, this);

				job.Model.Draw((mesh) => geometryShader.Update(mesh), geometryShader.HasTesselation);
			}
		}

		private void RenderLights(Camera camera)
		{
			this.Matrices.View = camera.ViewMatrix;
			this.Matrices.Projection = camera.ProjectionMatrix;

			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.One);
			GL.CullFace(CullFaceMode.Front);
			GL.DepthFunc(DepthFunction.Lequal);


			this.frameBuffer.SetTextures(this.DiffuseLightBuffer);
			this.frameBuffer.Use();

			// Clear first frame buffer (diffuse)
			GL.ClearColor(0.15f, 0.15f, 0.15f, 1.0f);
			GL.Clear(ClearBufferMask.ColorBufferBit);

			this.frameBuffer.SetTextures(this.SpecularLightBuffer);
			this.frameBuffer.Use();

			// Clear second frame buffer (specular)
			GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
			GL.Clear(ClearBufferMask.ColorBufferBit);

			this.frameBuffer.SetTextures(this.DiffuseLightBuffer, this.SpecularLightBuffer);
			this.frameBuffer.Use();

			// Clear depth
			GL.DepthMask(true);
			GL.Clear(ClearBufferMask.DepthBufferBit);
			GL.DepthMask(false);

			//this.LightCount = 0;

			var lights = new LightUniforms();
			lights.ViewPosition = Matrix4.Invert(camera.ViewMatrix).Row3.Xyz;

			foreach (var job in this.LightRenderJobs)
			{
				lights.Position = job.Position;
				lights.Radius = job.Radius;
				lights.Color = job.Color;
				this.Matrices.World = Matrix4.CreateScale(2.0f * job.Radius) * Matrix4.CreateTranslation(job.Position);

				// Set uniforms

				pointLightShader.Use(lights, this.Matrices, this);

				this.lightCube.Draw();
			}

			//this.sunLightShader.LightDirection = this.sky.GetSunDirection();
			//this.sunLightShader.ShadowMatrix = shadowMatrix;
			//this.ui.Draw(new RectangleF(-1, -1, 2, 2), this.normalBuffer, this.sunLightShader);

			GL.DepthMask(true);
		}

		private void RenderOpaque(Camera camera)
		{
			this.Matrices.View = camera.ViewMatrix;
			this.Matrices.Projection = camera.ProjectionMatrix;

			// Unbind framebuffer -> draw on screen
			this.frameBuffer.SetTextures(this.resultBuffer);
			this.frameBuffer.Use();

			// No depth, we use the depth from the previous pass.
			GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			GL.DepthMask(false);

			this.Sky.Draw(this, camera);

			GL.DepthMask(true);
			GL.Enable(EnableCap.DepthTest);
			GL.Enable(EnableCap.CullFace);
			GL.Disable(EnableCap.Blend);
			GL.CullFace(CullFaceMode.Back);
			GL.DepthFunc(DepthFunction.Lequal);

			//this.OpaqueCount = 0;
			foreach (var job in this.SolidRenderJobs)
			{
				this.Matrices.World = job.Transform;

				var shader = job.Material.Shader ?? this.DefaultShader;

				shader.Use(job.Material, this.Matrices, this);

				job.Model.Draw((mesh) => shader.Update(mesh), shader.HasTesselation);

				//this.OpaqueCount += 1;
			}
		}

		/// <summary>
		/// Gets the width of the deferred render targets.
		/// </summary>
		public int Width { get; private set; }

		/// <summary>
		/// Gets the height of the deferred render targets.
		/// </summary>
		public int Height { get; private set; }
	}
}