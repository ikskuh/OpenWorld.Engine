using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Represents a mesh of a 3D model.
	/// </summary>
	public sealed class ModelMesh : IDisposable
	{
		private uint[] indexes;
		private Vertex[] vertices;

		private VertexArray vertexArray;
		private Buffer vertexBuffer;
		private Buffer indexBuffer;

		/// <summary>
		/// Instantiates a new model mesh.
		/// </summary>
		/// <param name="indexes">The indexes of the mesh.</param>
		/// <param name="vertices">The vertices of the mesh.</param>
		public ModelMesh(uint[] indexes, Vertex[] vertices)
		{
			if (indexes == null)
				throw new ArgumentNullException("indexes");
			if (vertices == null)
				throw new ArgumentNullException("vertices");

			this.indexes = indexes;
			this.vertices = vertices;

			this.vertexArray = new VertexArray();
			this.vertexBuffer = new Buffer(BufferTarget.ArrayBuffer);
			this.indexBuffer = new Buffer(BufferTarget.ElementArrayBuffer);

			this.vertexBuffer.SetData(BufferUsageHint.StaticDraw, this.vertices);
			this.indexBuffer.SetData(BufferUsageHint.StaticDraw, this.indexes);

			this.vertexArray.Bind();

			GL.EnableVertexAttribArray(0);
			GL.EnableVertexAttribArray(1);
			GL.EnableVertexAttribArray(2);

			this.vertexBuffer.Bind();

			// Position
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vertex.Size, 0);

			// Normal
			GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, Vertex.Size, 12);

			// UV 1
			GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, Vertex.Size, 24);

			// UV 2
			GL.VertexAttribPointer(3, 2, VertexAttribPointerType.Float, false, Vertex.Size, 32);

			// Bind element buffer
			this.indexBuffer.Bind();

			VertexArray.Unbind();
		}

		/// <summary>
		/// Draws the mesh.
		/// </summary>
		public void Draw()
		{
			this.vertexArray.Bind();
			GL.DrawElements(
				BeginMode.Triangles,
				this.indexes.Length,
				DrawElementsType.UnsignedInt,
				0);
			VertexArray.Unbind();
		}

		/// <summary>
		/// Disposes the mesh.
		/// </summary>
		public void Dispose()
		{
			if (this.vertexBuffer != null)
				this.vertexBuffer.Dispose();
			if (this.indexBuffer != null)
				this.indexBuffer.Dispose();
			if (this.vertexArray != null)
				this.vertexArray.Dispose();

			this.vertexBuffer = null;
			this.indexBuffer = null;
			this.vertexArray = null;
		}

		/// <summary>
		/// Gets an array that contains all vertices.
		/// </summary>
		/// <returns>Vertex array</returns>
		public Vertex[] GetVertices()
		{
			return this.vertices;
		}

		/// <summary>
		/// Gets an array that contains all indexes.
		/// </summary>
		/// <returns>Index array</returns>
		public uint[] GetIndexes()
		{
			return this.indexes;
		}

		/// <summary>
		/// Gets or sets the diffuse texture of the mesh.
		/// </summary>
		public Texture2D DiffuseTexture { get; set; }
	}
}
