using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Draws a scene.
	/// </summary>
	public abstract partial class SceneRenderer
	{
		private List<ModelRenderJob> solidRenderJobs = new List<ModelRenderJob>();
		private List<ModelRenderJob> translucentRenderJobs = new List<ModelRenderJob>();

		/// <summary>
		/// Instantiates a new renderer.
		/// </summary>
		protected SceneRenderer()
		{

		}

		/// <summary>
		/// Starts the drawing process.
		/// </summary>
		public void Begin()
		{
			if (this.IsDrawing)
				throw new InvalidOperationException("You need to call End() before drawing again..");

			this.IsDrawing = true;
		}

		/// <summary>
		/// Draws a model.
		/// </summary>
		/// <param name="model">The model to be drawn.</param>
		/// <param name="transform"></param>
		/// <param name="material"></param>
		public void Draw(Model model, Matrix4 transform, Material material)
		{
			if (model == null)
				throw new ArgumentNullException("model");

			ModelRenderJob job = new ModelRenderJob(transform, model, material ?? this.DefaultMaterial ?? new Material());
			if (job.Material.IsTranslucent)
				this.translucentRenderJobs.Add(job);
			else
				this.solidRenderJobs.Add(job);
		}

		/// <summary>
		/// Ends the drawing process and renders everything.
		/// </summary>
		public void End(Scene scene)
		{
			if (!this.IsDrawing)
				throw new InvalidOperationException("You need to call Begin() first.");

			this.Render(scene);

			this.IsDrawing = false;
		}

		/// <summary>
		/// Actually renders the scene.
		/// <param name="scene">The scene that should be rendered.</param>
		/// </summary>
		protected abstract void Render(Scene scene);

		/// <summary>
		/// Gets a value that indicates if the renderer is currently drawing.
		/// </summary>
		public bool IsDrawing { get; private set; }

		/// <summary>
		/// Gets or sets the default material the renderer should use if no material is provided.
		/// </summary>
		public Material DefaultMaterial { get; set; }

		/// <summary>
		/// Gets all solid render jobs.
		/// </summary>
		protected IEnumerable<ModelRenderJob> SolidRenderJobs
		{
			get { return this.solidRenderJobs; }
		}

		/// <summary>
		/// Gets all translucent render jobs.
		/// </summary>
		protected IEnumerable<ModelRenderJob> TranslucentRenderJobs
		{
			get { return this.translucentRenderJobs; }
		}
	}
}
