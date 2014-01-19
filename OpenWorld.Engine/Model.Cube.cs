using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	partial class Model
	{
		/// <summary>
		/// Creates a new cube primitive.
		/// </summary>
		/// <param name="size">Length of the cube sides.</param>
		/// <returns></returns>
		public static Model CreateCube(float size)
		{
			size *= 0.5f;
			var indexes0 = new uint[] { 3, 0, 1, 1, 2, 3, 7, 4, 5, 5, 6, 7, 11, 8, 9, 9, 10, 11, 15, 12, 13, 13, 14, 15, 19, 16, 17, 17, 18, 19, 23, 20, 21, 21, 22, 23 };
			var vertices0 = new[] 
			{
				new Vertex(new Vector3(-size, -size, size), new Vector3(0, -1, 0), new Vector2(1, 1), new Vector2(0, 0)),
				new Vertex(new Vector3(-size, -size, -size), new Vector3(0, -1, 0), new Vector2(1, 0), new Vector2(0, 0)),
				new Vertex(new Vector3(size, -size, -size), new Vector3(0, -1, 0), new Vector2(0, 0), new Vector2(0, 0)),
				new Vertex(new Vector3(size, -size, size), new Vector3(0, -1, 0), new Vector2(0, 1), new Vector2(0, 0)),
				new Vertex(new Vector3(-size, size, size), new Vector3(0, 1, 0), new Vector2(0, 1), new Vector2(0, 0)),
				new Vertex(new Vector3(size, size, size), new Vector3(0, 1, 0), new Vector2(1, 1), new Vector2(0, 0)),
				new Vertex(new Vector3(size, size, -size), new Vector3(0, 1, 0), new Vector2(1, 0), new Vector2(0, 0)),
				new Vertex(new Vector3(-size, size, -size), new Vector3(0, 1, 0), new Vector2(0, 0), new Vector2(0, 0)),
				new Vertex(new Vector3(-size, -size, size), new Vector3(0, 0, 1), new Vector2(0, 1), new Vector2(0, 0)),
				new Vertex(new Vector3(size, -size, size), new Vector3(0, 0, 1), new Vector2(1, 1), new Vector2(0, 0)),
				new Vertex(new Vector3(size, size, size), new Vector3(0, 0, 1), new Vector2(1, 0), new Vector2(0, 0)),
				new Vertex(new Vector3(-size, size, size), new Vector3(0, 0, 1), new Vector2(0, 0), new Vector2(0, 0)),
				new Vertex(new Vector3(size, -size, size), new Vector3(1, 0, 0), new Vector2(0, 1), new Vector2(0, 0)),
				new Vertex(new Vector3(size, -size, -size), new Vector3(1, 0, 0), new Vector2(1, 1), new Vector2(0, 0)),
				new Vertex(new Vector3(size, size, -size), new Vector3(1, 0, 0), new Vector2(1, 0), new Vector2(0, 0)),
				new Vertex(new Vector3(size, size, size), new Vector3(1, 0, 0), new Vector2(0, 0), new Vector2(0, 0)),
				new Vertex(new Vector3(size, -size, -size), new Vector3(0, 0, -1), new Vector2(0, 1), new Vector2(0, 0)),
				new Vertex(new Vector3(-size, -size, -size), new Vector3(0, 0, -1), new Vector2(1, 1), new Vector2(0, 0)),
				new Vertex(new Vector3(-size, size, -size), new Vector3(0, 0, -1), new Vector2(1, 0), new Vector2(0, 0)),
				new Vertex(new Vector3(size, size, -size), new Vector3(0, 0, -1), new Vector2(0, 0), new Vector2(0, 0)),
				new Vertex(new Vector3(-size, -size, -size), new Vector3(-1, 0, 0), new Vector2(0, 1), new Vector2(0, 0)),
				new Vertex(new Vector3(-size, -size, size), new Vector3(-1, 0, 0), new Vector2(1, 1), new Vector2(0, 0)),
				new Vertex(new Vector3(-size, size, size), new Vector3(-1, 0, 0), new Vector2(1, 0), new Vector2(0, 0)),
				new Vertex(new Vector3(-size, size, -size), new Vector3(-1, 0, 0), new Vector2(0, 0), new Vector2(0, 0)),
			};
			var mesh0 = new ModelMesh(indexes0, vertices0);
			return new Model(new[] { mesh0 });
		}


	}
}
