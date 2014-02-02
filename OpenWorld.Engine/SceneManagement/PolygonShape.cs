using BulletSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Defines a polygonal collision shape.
	/// </summary>
	public sealed class PolygonShape : Shape
	{
		BvhTriangleMeshShape shape;

		/// <summary>
		/// Starts the component.
		/// </summary>
		/// <param name="time">Time snapshot</param>
		protected override void OnStart(GameTime time)
		{
			base.OnStart(time);

			while (!this.Model.IsLoaded) Thread.Sleep(0);

			List<Vector3> positions = new List<Vector3>();
			List<int> indexList = new List<int>();

			int offset = 0;
			foreach(var mesh in this.Model.GetMeshes())
			{
				offset = positions.Count;
				foreach(var vertex in mesh.GetVertices())
				{
					positions.Add(new Vector3(vertex.Position.X, vertex.Position.Y, vertex.Position.Z));
				}

				uint[] indexes = mesh.GetIndexes();
				for (int i = 0; i < indexes.Length; i++)
				{
					indexList.Add(offset + (int)indexes[i]);
				}
			}

			var triangleMesh = new TriangleIndexVertexArray(indexList.ToArray(), positions.ToArray());

			this.shape = new BvhTriangleMeshShape(triangleMesh, true, true);
		}


		/// <summary>
		/// Gets the Jitter shape fitting this shape component.
		/// </summary>
		/// <returns>A Jitter shape.</returns>
		protected internal override CollisionShape GetShape()
		{
			return this.shape;
		}

		/// <summary>
		/// Gets or sets the collision model.
		/// </summary>
		public Model Model { get; set; }
	}
}
