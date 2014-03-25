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
			internal ModelRenderJob(Matrix4 transform, Model model, BaseMaterial material)
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
			public BaseMaterial Material { get; private set; }
		}

		/// <summary>
		/// Defines a render job that should draw a light.
		/// </summary>
		protected class LightRenderJob
		{
			internal LightRenderJob(Vector3 position, float radius, Color color)
			{
				this.Position = position;
				this.Radius = radius;
				this.Color = color;
			}

			/// <summary>
			/// Gets the position of the light.
			/// </summary>
			public Vector3 Position { get; private set; }

			/// <summary>
			/// Gets the radius of the light.
			/// </summary>
			public float Radius { get; private set; }

			/// <summary>
			/// Gets the color of the light.
			/// </summary>
			public Color Color { get; private set; }
		}
	}
}
