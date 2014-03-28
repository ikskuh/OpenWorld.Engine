using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Provides methods to draw a simple, non-lit scene.
	/// </summary>
	public sealed class SimpleRenderer : SceneRenderer
	{
		/// <summary>
		/// Creates a simple renderer.
		/// </summary>
		public SimpleRenderer()
		{

		}

		/// <summary>
		/// Renders the scene.
		/// </summary>
		protected override void Render(Scene scene, Camera camera)
		{
			GL.ClearDepth(1.0f);

			// No depth, we use the depth from the previous pass.
			GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			GL.DepthMask(false);

			this.Sky.Draw(this, camera);

			GL.DepthMask(true);
			
			GL.Enable(EnableCap.DepthTest);
			GL.Enable(EnableCap.CullFace);
			GL.CullFace(CullFaceMode.Back);
			GL.DepthFunc(DepthFunction.Lequal);
			GL.Disable(EnableCap.Blend);

			this.Draw(scene, camera, this.SolidRenderJobs);

			GL.Disable(EnableCap.DepthTest);
			GL.Enable(EnableCap.Blend);

			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			this.Draw(scene, camera, this.TranslucentRenderJobs);
		}

		private void Draw(Scene scene, Camera camera, IEnumerable<ModelRenderJob> jobs)
		{
			this.Matrices.View = camera.ViewMatrix;
			this.Matrices.Projection = camera.ProjectionMatrix;

			foreach(var job in jobs)
			{
				this.Matrices.World = job.Transform;

				var material = job.Material;
				var shader = material.Shader ?? this.DefaultShader;

				var cs = shader.Select();
				cs.Bind();
				cs.BindUniforms(material, this.Matrices, this);
				
				job.Model.Draw((mesh) => cs.BindUniform(mesh), cs.HasTesselation);
			}
		}
	}
}
