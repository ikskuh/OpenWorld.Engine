using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenWorld.Engine.PostProcessingShaders;
using OpenWorld.Engine.SceneManagement.Shaders;
using OpenWorld.Engine.ShaderSystem;
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
		private FrameBuffer frameBufferGeometry;
		private FrameBuffer frameBufferClearDiffuse;
		private FrameBuffer frameBufferClearSpecular;
		private FrameBuffer frameBufferLights;
		private FrameBuffer frameBufferScene;

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

		private Texture2D sceneBuffer;
		private RenderBuffer depthBuffer;

		private BlurShader blurShader;
		private CombineShader bloomCombineShader;
		private HighPassShader highPassShader;

		private TonemappingShader tonemappingShader;
		private GammaCorrectionShader gammaCorrectionShader;
		private LightScatteringShader lightScatteringShader;
		private DitheringShader ditheringShader;

		private PostProcessingStage preBloomStages;
		private PostProcessingStage bloomStages;
		private PostProcessingStage postBloomStages;

		private ShaderFragment geometryPixelShader;
		private LightShader lightShader;
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
			this.PositionBuffer = CreateTexture(width, height);
			this.NormalBuffer = CreateTexture(width, height);
			this.DiffuseLightBuffer = CreateTexture(width, height);
			this.SpecularLightBuffer = CreateTexture(width, height);
			this.sceneBuffer = CreateTexture(width, height);

			// Create 3d shaders
			this.geometryPixelShader = new ShaderFragment(ShaderType.FragmentShader);
			this.geometryPixelShader.Compile(
@"#version 410 core
uniform sampler2D meshNormalMap;
uniform sampler2D meshDiffuseTexture;
uniform float mtlSpecularPower;

in vec3 position;
in vec3 normal;
in vec3 tangent;
in vec3 bitangent;
in vec2 uv;

layout(location = 0) out vec4 positionOut;
layout(location = 1) out vec4 normalOut;

void main()
{
	if(texture(meshDiffuseTexture, uv).a < 0.5f) discard;	

	positionOut = vec4(position, 1);

	vec3 bump = normalize(2.0f * texture(meshNormalMap, uv).xyz - 1.0f);
	normalOut.xyz = mat3(tangent, bitangent, normal) * bump;
	normalOut.w = mtlSpecularPower;
	//normalOut.xyz = normal;
}");

			this.lightShader = new LightShader();

			// Create post processing shaders
			this.gammaCorrectionShader = new GammaCorrectionShader();
			this.gammaCorrectionShader.Gamma = 2.2f;

			this.lightScatteringShader = new LightScatteringShader();
			this.lightScatteringShader.OcclusionBuffer = this.NormalBuffer;

			this.ditheringShader = new DitheringShader();

			this.tonemappingShader = new TonemappingShader();
			this.tonemappingShader.HdrExposure = 1.25f;
			this.tonemappingShader.WhitePoint = 5.0f;

			this.blurShader = new BlurShader();
			this.blurShader.BlurStrength = new Vector2(0.04f, 0.04f);

			this.bloomCombineShader = new CombineShader();

			this.highPassShader = new HighPassShader();
			this.highPassShader.BloomThreshold = 0.95f;

			// Create post processing stages

			this.preBloomStages = new PostProcessingStage(this.lightScatteringShader)
			{
				TargetTexture = CreateTexture(width, height),
				Stage = new PostProcessingStage(this.gammaCorrectionShader)
				{
					TargetTexture = CreateTexture(width, height)
				}
			};

			this.bloomStages = new PostProcessingStage(this.highPassShader)
			{
				TargetTexture = CreateTexture(width / 2, height / 2),
				Stage = new PostProcessingStage(this.blurShader)
				{
					TargetTexture = CreateTexture(width, height),
					Stage = new PostProcessingStage(this.bloomCombineShader)
					{
						TargetTexture = CreateTexture(width, height),
					}
				}
			};

			this.postBloomStages = new PostProcessingStage(this.tonemappingShader)
			{
				TargetTexture = CreateTexture(width, height),
				Stage = new PostProcessingStage(this.ditheringShader)
				{
					TargetTexture = CreateTexture(width, height),
				}
			};

			//this.tonemapping = new PostProcessingStage(this.tonemappingShader);
			//this.tonemapping.TargetTexture = CreateTexture(width, height);

			this.depthBuffer = new RenderBuffer(width, height);

			this.frameBufferGeometry = new FrameBuffer(this.depthBuffer, this.PositionBuffer, this.NormalBuffer);
			this.frameBufferClearDiffuse = new FrameBuffer(this.DiffuseLightBuffer);
			this.frameBufferClearSpecular = new FrameBuffer(this.SpecularLightBuffer);
			this.frameBufferLights = new FrameBuffer(this.depthBuffer, this.DiffuseLightBuffer, this.SpecularLightBuffer);
			this.frameBufferScene = new FrameBuffer(this.depthBuffer, this.sceneBuffer);

			this.DefaultShader = new ObjectShader();
		}

		private static Texture2D CreateTexture(int width, int height)
		{
			var tex = new Texture2D(width, height, PixelInternalFormat.Rgba16f, PixelFormat.Rgba, PixelType.Float);
			tex.WrapR = TextureWrapMode.ClampToBorder;
			tex.WrapS = TextureWrapMode.ClampToBorder;
			tex.WrapT = TextureWrapMode.ClampToBorder;
			return tex;
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

			var renderTarget = FrameBuffer.Current;

			Viewport.Push();
			Viewport.Area = new Box2i(0, 0, this.Width, this.Height);

			GL.Enable(EnableCap.TextureCubeMap);
			GL.Enable(EnableCap.TextureCubeMapSeamless);

			//RenderLightMaps(ref camera, ref projection, out shadowMatrix, args);

			RenderGeometry(camera);
			RenderLights(camera);
			RenderOpaque(camera);
			//RenderTranslucent(ref camera, ref projection, args);

			var sunView = camera.ViewMatrix;
			sunView.M41 = 0; sunView.M42 = 0; sunView.M43 = 0;

			Vector3 sunPosition = this.Sky.GetSunDirection() * 1000.0f;
			var projPos = Vector4.Transform(new Vector4(sunPosition, 1), sunView * camera.ProjectionMatrix);
			this.lightScatteringShader.LightPosition = new Vector2(0.5f, 0.5f) + 0.5f * (projPos.Xy * (1.0f / projPos.W));
			//this.lightScattering.Enabled = false;// Math.Abs(this.lightScatteringShader.LightPosition.X) < 2.0f && Math.Abs(this.lightScatteringShader.LightPosition.Y) < 2.0f && projPos.Z > 0.0f;

			FrameBuffer.Current = renderTarget;

			Viewport.Pop();

			GL.Disable(EnableCap.Blend);
			GL.Disable(EnableCap.DepthTest);
			GL.Enable(EnableCap.CullFace);

			var target = new Box2(0, 0, Game.Current.Width, Game.Current.Height);
			Texture2D result = this.sceneBuffer;

			result = this.preBloomStages.Render(result);
			// After prebloom, set shader variable
			this.bloomCombineShader.OriginalMap = result;
			result = this.bloomStages.Render(result);
			result = this.postBloomStages.Render(result);

			if (!Game.Current.Input.Keyboard[OpenTK.Input.Key.ShiftLeft])
			{
				if (Game.Current.Input.Keyboard[OpenTK.Input.Key.Number1])
				{
					result = this.NormalBuffer;
				}
				else if (Game.Current.Input.Keyboard[OpenTK.Input.Key.Number2])
				{
					result = this.DiffuseLightBuffer;
				}
				else if (Game.Current.Input.Keyboard[OpenTK.Input.Key.Number3])
				{
					result = this.SpecularLightBuffer;
				}
				else if (Game.Current.Input.Keyboard[OpenTK.Input.Key.Number4])
				{
					result = this.PositionBuffer;
				}
			}
			else
			{
				if (Game.Current.Input.Keyboard[OpenTK.Input.Key.Number1])
				{
					result = this.preBloomStages.FinalTexture;
				}
				else if (Game.Current.Input.Keyboard[OpenTK.Input.Key.Number2])
				{
					result = this.bloomStages.FinalTexture;
				}
				else if (Game.Current.Input.Keyboard[OpenTK.Input.Key.Number3])
				{
					result = this.postBloomStages.FinalTexture;
				}
			}

			Game.Current.Utilities.Draw(target, result, true);
		}

		private void RenderGeometry(Camera camera)
		{
			FrameBuffer.Current = this.frameBufferGeometry;

			this.Matrices.View = camera.ViewMatrix;
			this.Matrices.Projection = camera.ProjectionMatrix;

			GL.Disable(EnableCap.Blend);
			GL.Enable(EnableCap.DepthTest);
			GL.Enable(EnableCap.CullFace);
			GL.CullFace(CullFaceMode.Back);
			GL.DepthFunc(DepthFunction.Less);

			GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
			GL.ClearDepth(1.0f);
			GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);

			foreach (var job in this.SolidRenderJobs)
			{
				this.Matrices.World = job.Transform;

				var shader = job.Material.Shader ?? this.DefaultShader;

				CompiledShader cs = shader.Select("DeferredRenderer", this.geometryPixelShader);
				cs.Bind();
				cs.BindUniforms(job.Material, this.Matrices, this);

				job.Model.Draw((mesh) => cs.BindUniform(mesh), cs.HasTesselation);
			}

			FrameBuffer.Current = null;
		}

		private void RenderLights(Camera camera)
		{
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.One);
			GL.Enable(EnableCap.CullFace);
			GL.CullFace(CullFaceMode.Front);
			GL.Disable(EnableCap.DepthTest);
			GL.DepthFunc(DepthFunction.Gequal);

			FrameBuffer.Current = this.frameBufferClearDiffuse;

			// Clear first frame buffer (diffuse)
			GL.ClearColor(0.05f, 0.05f, 0.05f, 1.0f);
			GL.Clear(ClearBufferMask.ColorBufferBit);

			FrameBuffer.Current = this.frameBufferClearSpecular;

			// Clear second frame buffer (specular)
			GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
			GL.Clear(ClearBufferMask.ColorBufferBit);

			FrameBuffer.Current = this.frameBufferLights;

			GL.DepthMask(false);

			//this.LightCount = 0;

			var viewPos = Matrix4.Invert(camera.ViewMatrix).Row3.Xyz;

			var sun = new DirectionalLight()
			{
				Color = Color.White,
				Direction = -this.Sky.GetSunDirection(),
				Intensity = 1.0f,
			};
			sun.Intensity = (float)Math.Pow(1.0f - Math.Max(0.0f, Vector3.Dot(Vector3.UnitY, sun.Direction)), 4.0f);

			foreach (var job in this.LightRenderJobs.Concat(new[] { new LightRenderJob(Vector3.Zero, sun) }))
			{
				string @class = "PointLight";
				switch (job.Light.Type)
				{
					case LightType.Point:
						@class = "PointLight";

						this.Matrices.View = camera.ViewMatrix;
						this.Matrices.Projection = camera.ProjectionMatrix;
						this.Matrices.World = Matrix4.CreateScale(8.0f * job.Light.Intensity) * Matrix4.CreateTranslation(job.Position);
						break;
					case LightType.Spot:
						@class = "SpotLight";

						this.Matrices.View = camera.ViewMatrix;
						this.Matrices.Projection = camera.ProjectionMatrix;
						this.Matrices.World = Matrix4.CreateScale(2.0f * job.Light.Intensity) * Matrix4.CreateTranslation(job.Position);
						break;
					case LightType.Directional:
						@class = "DirectionalLight";

						// Draw cube as fullscreen quad
						this.Matrices.View = Matrix4.Identity;
						this.Matrices.Projection = Matrix4.Identity;
						this.Matrices.World = Matrix4.CreateScale(2.0f, 2.0f, 0.0f);
						break;
				}

				var cs = lightShader.Select(@class);
				cs.Bind();
				cs.BindUniforms(this, job, job.Light, this.Matrices);

				// Bind view position manual.
				if (cs["lightViewPosition"] != null)
					cs["lightViewPosition"].SetValue(viewPos);

				this.lightCube.Draw();
			}

			GL.DepthMask(true);
			FrameBuffer.Current = null;
		}

		private void RenderOpaque(Camera camera)
		{
			this.Matrices.View = camera.ViewMatrix;
			this.Matrices.Projection = camera.ProjectionMatrix;

			FrameBuffer.Current = this.frameBufferScene;

			// No depth, we use the depth from the previous pass.
			GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
			GL.Clear(ClearBufferMask.ColorBufferBit);

			// Don't write z, just compare
			GL.DepthMask(false);

			this.Sky.Draw(this, camera);

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

				CompiledShader cs = shader.Select("DeferredRenderer");
				cs.Bind();
				cs.BindUniforms(job.Material, this.Matrices, this);

				job.Model.Draw((mesh) => cs.BindUniform(mesh), cs.HasTesselation);

				//this.OpaqueCount += 1;
			}

			GL.DepthMask(true);
			FrameBuffer.Current = null;
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