using OpenTK.Graphics.OpenGL;
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
		ObjectShader defaultShader;

		/// <summary>
		/// Creates a simple renderer.
		/// </summary>
		public SimpleRenderer()
		{
			this.defaultShader = new ObjectShader();
		}

		/// <summary>
		/// Renders the scene.
		/// </summary>
		protected override void Render(Scene scene)
		{
			GL.ClearDepth(1.0f);

			GL.Enable(EnableCap.DepthTest);
			GL.Enable(EnableCap.AlphaTest);
			GL.Disable(EnableCap.Blend);

			GL.DepthFunc(DepthFunction.Lequal);
			GL.AlphaFunc(AlphaFunction.Less, 0.5f);

			GL.Clear(ClearBufferMask.DepthBufferBit);

			this.Draw(scene, this.SolidRenderJobs);

			GL.Disable(EnableCap.DepthTest);
			GL.Disable(EnableCap.AlphaTest);
			GL.Enable(EnableCap.Blend);

			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			this.Draw(scene, this.TranslucentRenderJobs);
		}

		private void Draw(Scene scene, IEnumerable<ModelRenderJob> jobs)
		{
			foreach(var job in jobs)
			{
				var shader = job.Material.Shader ?? this.defaultShader;

				shader.World = job.Transform;
				shader.View = scene.Camera.ViewMatrix;
				shader.Projection = scene.Camera.ProjectionMatrix;
				shader.Use();

				job.Model.Draw((type, texture) =>
					{
						if (type != TextureType.Diffuse)
							return;
						shader.DiffuseTexture = texture;
						shader.Apply();
					});
			}
		}
	}
}
