using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Defines a terrain.
	/// </summary>
	public class Terrain : Component
	{
		private Model model;
		private float[,] heights;
		private PolygonShape shape;

		/// <summary>
		/// Creates a new terrain.
		/// </summary>
		public Terrain()
		{
			this.Resolution = 128;
			this.CellSize = 32.0f;
		}

		/// <summary>
		/// Preloads the terrain with the current settings.
		/// </summary>
		/// <remarks>Can be used for loading terrain data after set up.</remarks>
		public void Preload()
		{
			this.heights = new float[this.Resolution, this.Resolution];
		}

		/// <summary>
		/// Notifies the component that it was added.
		/// </summary>
		protected override void OnAdd()
		{
			base.OnAdd();
			this.shape = this.Node.Components.Add<PolygonShape>();
		}

		/// <summary>
		/// Generates the terrain
		/// </summary>
		/// <param name="time"></param>
		protected override void OnStart(GameTime time)
		{
			base.OnStart(time);

			uint size = (uint)(this.Resolution - 1);

			this.heights = this.heights ?? new float[this.Resolution, this.Resolution];

			Vertex[] vertices = new Vertex[(size + 1) * (size + 1)];
			uint[] indexes = new uint[6 * size * size];

			for (int x = 0; x <= size; x++)
			{
				for (int y = 0; y <= size; y++)
				{
					vertices[y * (size + 1) + x] = new Vertex(
						new Vector3(this.CellSize * (x - size / 2), this.heights[x, y], this.CellSize * (y - size / 2)),
						new Vector3(0, 1, 0),
						new Vector2((float)x / size, (float)y / size));
				}
			}

			int index = 0;
			for (uint y = 0; y < size; y++)
			{
				for (uint x = 0; x < size; x++)
				{
					uint i = y * (size + 1) + x;
					indexes[6 * index + 0] = i + 1;
					indexes[6 * index + 1] = i + 0;
					indexes[6 * index + 2] = i + (size + 1) + 0;

					indexes[6 * index + 3] = i + 1;
					indexes[6 * index + 4] = i + (size + 1) + 0;
					indexes[6 * index + 5] = i + (size + 1) + 1;

					index++;
				}
			}

			this.model = new Model(indexes, vertices, this.BaseTexture);

			if (this.shape == null)
				this.shape = this.Node.Components.Add<PolygonShape>();

			this.shape.Model = this.model;
		}

		/// <summary>
		/// Updates changed terrain data.
		/// </summary>
		public void Update()
		{
			if (this.model == null)
				return;
			if (this.heights == null)
				return;

			uint size = (uint)(this.Resolution - 1);
			var vertices = this.model.GetMeshes()[0].GetVertices();
			for (int x = 0; x <= size; x++)
			{
				for (int y = 0; y <= size; y++)
				{
					vertices[y * (size + 1) + x] = new Vertex(
						new Vector3(this.CellSize * (x - size / 2), this.heights[x, y], this.CellSize * (y - size / 2)),
						new Vector3(0, 1, 0),
						new Vector2((float)x / size, (float)y / size));
				}
			}
			for (int x = 0; x <= size; x++)
			{
				for (int y = 0; y <= size; y++)
				{
					Vector3 dx = Vector3.Zero;
					Vector3 dz = Vector3.Zero;
					if(x > 0)
						dx += vertices[y * (size+1) + (x-1)].Position;
					if(x < size)
						dx -= vertices[y * (size + 1) + (x + 1)].Position;

					if (y > 0)
						dz += vertices[(y-1) * (size + 1) + x].Position;
					if (y < size)
						dz -= vertices[(y+1) * (size + 1) + x].Position;

					vertices[x * (size + 1) + x].Normal = Vector3.Cross(dz, dx).Normalized();
				}
			}
			this.model.GetMeshes()[0].UpdateVertices();
		}

		/// <summary>
		/// Renders the terrain.
		/// </summary>
		/// <param name="time"></param>
		/// <param name="renderer"></param>
		protected override void OnRender(GameTime time, SceneRenderer renderer)
		{
			if (this.model == null)
				return;
			renderer.Draw(this.model, this.Node.Transform.WorldTransform, this.Node.Material);
		}

		/// <summary>
		/// Destroys the terrain
		/// </summary>
		/// <param name="time"></param>
		protected override void OnStop(GameTime time)
		{
			if (this.shape != null)
				this.Node.Components.Remove<PolygonShape>();
			this.shape = null;
			base.OnStop(time);
		}

		/// <summary>
		/// Notifies the component that it was removed.
		/// </summary>
		protected override void OnRemove()
		{
			if (this.shape != null)
				this.Node.Components.Remove<PolygonShape>();
			this.shape = null;
			base.OnRemove();
		}

		/// <summary>
		/// Defines the resolution of the terrain.
		/// </summary>
		public int Resolution { get; set; }

		/// <summary>
		/// Gets or sets the terrain cell size.
		/// </summary>
		public float CellSize { get; set; }

		/// <summary>
		/// Gets or sets the terrain texture.
		/// </summary>
		public Texture2D BaseTexture { get; set; }

		/// <summary>
		/// Gets or sets a height value.
		/// </summary>
		/// <param name="x">x coord</param>
		/// <param name="y">y coord</param>
		/// <returns>Height at (x,y)</returns>
		/// <remarks>Terrain must be preload or started</remarks>
		public float this[int x, int y]
		{
			get { return this.heights[x, y]; }
			set { this.heights[x, y] = value; }
		}
	}
}
