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
		private FrameBuffer frameBuffer;
		private Texture2D positionBuffer;
		private Texture2D normalBuffer;
		private Texture2D diffuseLightBuffer;
		private Texture2D specularLightBuffer;
		private Texture2D resultBuffer;

		private Texture2D blackTexture;
		private Texture2D missingTexture;
		private Texture2D flatNormalMap;

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
		private OpaqueObjectShader objectShader;
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
			this.positionBuffer = new Texture2D(width, height, PixelInternalFormat.Rgba16f, PixelFormat.Rgba, PixelType.Float);
			this.normalBuffer = new Texture2D(width, height, PixelInternalFormat.Rgba16f, PixelFormat.Rgba, PixelType.Float);
			this.diffuseLightBuffer = new Texture2D(width, height, PixelInternalFormat.Rgba16f, PixelFormat.Rgba, PixelType.Float);
			this.specularLightBuffer = new Texture2D(width, height, PixelInternalFormat.Rgba16f, PixelFormat.Rgba, PixelType.Float);
			this.resultBuffer = new Texture2D(width, height, PixelInternalFormat.Rgba16f, PixelFormat.Rgba, PixelType.Float);

			using (var stream = Resource.Open("OpenWorld.Engine.Resources.flatNormals.png"))
			{
				bool srgb = Texture2D.UseSRGB;
				Texture2D.UseSRGB = false;
				this.flatNormalMap = new Texture2D(new System.Drawing.Bitmap(stream));
				Texture2D.UseSRGB = srgb;
			}
			using (var stream = Resource.Open("OpenWorld.Engine.Resources.black.png"))
			{
				this.blackTexture = new Texture2D(new System.Drawing.Bitmap(stream));
			}
			using (var stream = Resource.Open("OpenWorld.Engine.Resources.missing.png"))
			{
				this.missingTexture = new Texture2D(new System.Drawing.Bitmap(stream));
			}
			// Create 3d shaders
			this.geometryShader = new GeometryShader();

			this.pointLightShader = new PointLightShader();
			this.pointLightShader.PositionBuffer = this.positionBuffer;
			this.pointLightShader.NormalBuffer = this.normalBuffer;

			this.objectShader = new OpaqueObjectShader();
			this.objectShader.DiffuseLightBuffer = this.diffuseLightBuffer;
			this.objectShader.SpecularLightBuffer = this.specularLightBuffer;


			// Create post processing shaders
			this.gammaCorrectionShader = new GammaCorrectionShader();
			this.gammaCorrectionShader.Gamma = 2.2f;

			this.lightScatteringShader = new LightScatteringShader();
			this.lightScatteringShader.OcclusionBuffer = this.normalBuffer;

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

			int[] viewport = new int[4];
			GL.GetInteger(GetPName.Viewport, viewport);

			GL.Viewport(0, 0, this.Width, this.Height);

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

			//Vector3 sunPosition =  this.sky.GetSunDirection() * 1000.0f;
			//var projPos = Vector4.Transform(new Vector4(sunPosition, 1), sunView * camera.ProjectionMatrix);
			//this.lightScatteringShader.LightPosition = new Vector2(0.5f, 0.5f) + 0.5f * (projPos.Xy * (1.0f / projPos.W));
			//this.lightScattering.Enabled = Math.Abs(this.lightScatteringShader.LightPosition.X) < 2.0f && Math.Abs(this.lightScatteringShader.LightPosition.Y) < 2.0f && projPos.Z > 0.0f;

			this.pipeline.Apply(this.resultBuffer);

			GL.Disable(EnableCap.Blend);
			GL.Disable(EnableCap.DepthTest);
			GL.Disable(EnableCap.CullFace);

			FrameBuffer.Unbind();

			GL.Viewport(viewport[0], viewport[1], viewport[2], viewport[3]);

			if (Game.Current.Input.Keyboard[OpenTK.Input.Key.Number1])
			{
				this.pipeline.DrawQuad(this.normalBuffer);
			}
			else if (Game.Current.Input.Keyboard[OpenTK.Input.Key.Number2])
			{
				this.pipeline.DrawQuad(this.diffuseLightBuffer);
			}
			else if (Game.Current.Input.Keyboard[OpenTK.Input.Key.Number3])
			{
				this.pipeline.DrawQuad(this.specularLightBuffer);
			}
			else if (Game.Current.Input.Keyboard[OpenTK.Input.Key.Number4])
			{
				this.pipeline.DrawQuad(this.positionBuffer);
			}
			else
			{
				this.pipeline.DrawQuad(this.resultBuffer);
			}
		}

		private void RenderGeometry(Camera camera)
		{
			GL.CullFace(CullFaceMode.Back);
			GL.Disable(EnableCap.Blend);
			GL.DepthFunc(DepthFunction.Less);

			this.frameBuffer.SetTextures(this.positionBuffer, this.normalBuffer);
			this.frameBuffer.Use();

			GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);

			this.geometryShader.Use();
			foreach (var op in this.SolidRenderJobs)
			{
				// Assign default normal map if none.
				this.geometryShader.NormalMap = this.flatNormalMap;
				this.geometryShader.World = op.Transform;
				this.geometryShader.View = camera.ViewMatrix;
				this.geometryShader.Projection = camera.ProjectionMatrix;
				this.geometryShader.SpecularPower = op.Material.SpecularPower;
				this.geometryShader.Apply();

				op.Model.Draw((type, texture) =>
					{
						if (type != TextureType.NormalMap)
							return;
						this.geometryShader.NormalMap = texture ?? this.flatNormalMap;
						this.geometryShader.Apply();
					});
			}
		}

		private void RenderLights(Camera camera)
		{
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.One);
			GL.CullFace(CullFaceMode.Front);
			GL.DepthFunc(DepthFunction.Lequal);


			this.frameBuffer.SetTextures(this.diffuseLightBuffer, this.specularLightBuffer);
			this.frameBuffer.Use();

			GL.ClearColor(0.15f, 0.15f, 0.15f, 1.0f);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			GL.DepthMask(false);

			//this.LightCount = 0;
			this.pointLightShader.ViewPosition = Matrix4.Invert(camera.ViewMatrix).Row3.Xyz;
			this.pointLightShader.Use();

			foreach (var op in this.LightRenderJobs)
			{
				var world = Matrix4.CreateScale(2.0f * op.Radius) * Matrix4.CreateTranslation(op.Position);

				this.pointLightShader.World = world;
				this.pointLightShader.View = camera.ViewMatrix;
				this.pointLightShader.Projection = camera.ProjectionMatrix;
				this.pointLightShader.Position = op.Position;
				this.pointLightShader.Radius = op.Radius;
				this.pointLightShader.Color = op.Color;
				this.pointLightShader.Apply();

				this.lightCube.Draw();

				//this.LightCount += 1;
			}

			//this.sunLightShader.LightDirection = this.sky.GetSunDirection();
			//this.sunLightShader.ShadowMatrix = shadowMatrix;
			//this.ui.Draw(new RectangleF(-1, -1, 2, 2), this.normalBuffer, this.sunLightShader);

			GL.DepthMask(true);
		}

		private void RenderOpaque(Camera camera)
		{
			// Unbind framebuffer -> draw on screen
			this.frameBuffer.SetTextures(this.resultBuffer);
			this.frameBuffer.Use();

			this.objectShader.Use();

			// No depth, we use the depth from the previous pass.
			GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			GL.DepthMask(false);

			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.One);

			this.Sky.Draw(this, camera);

			GL.DepthMask(true);
			GL.CullFace(CullFaceMode.Back);
			GL.Disable(EnableCap.Blend);

			//this.OpaqueCount = 0;
			foreach (var op in this.SolidRenderJobs)
			{
				this.objectShader.World = op.Transform;
				this.objectShader.View = camera.ViewMatrix;
				this.objectShader.Projection = camera.ProjectionMatrix;
				this.objectShader.Apply();

				op.Model.Draw((type, texture) =>
				{
					switch (type)
					{
						case TextureType.Diffuse:
							this.objectShader.DiffuseColorTexture = texture ?? missingTexture;
							break;
						case TextureType.Specular:
							this.objectShader.SpecularColorTexture = texture ?? blackTexture;
							break;
						default:
							return;
					}
					this.objectShader.Apply();
				});

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