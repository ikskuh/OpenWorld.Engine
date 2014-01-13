using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	partial class SceneRenderer
	{
		/// <summary>
		/// Defines a render job that should draw a model.
		/// </summary>
		protected class ModelRenderJob
		{
			internal ModelRenderJob(Matrix4 transform, Model model, Material material)
			{
				if (model == null)
					throw new ArgumentNullException("model");
				if (material == null)
					throw new ArgumentNullException("material");

				this.Transform = transform;
				this.Model = model;
				this.Material = material;
			}

			/// <summary>
			/// Gets the global transform.
			/// </summary>
			public Matrix4 Transform { get; private set; }

			/// <summary>
			/// Gets the model.
			/// </summary>
			public Model Model { get; private set; }

			/// <summary>
			/// Gets the material.
			/// </summary>
			public Material Material { get; private set; }
		}
	}
}
