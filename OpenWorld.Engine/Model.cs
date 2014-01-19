using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assimp;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using System.IO;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Represents a 3D model.
	/// </summary>
	[AssetExtension(".dae", ".obj")]
	public sealed partial class Model : IAsset
	{
		ModelMesh[] meshes;

		/// <summary>
		/// Instantiates an empty model
		/// </summary>
		public Model()
		{

		}

		/// <summary>
		/// Instantiates a one-mesh model.
		/// </summary>
		/// <param name="indexes">Index array</param>
		/// <param name="vertices">Vertex array</param>
		/// <param name="diffuse">Diffuse texture of the model</param>
		public Model(uint[] indexes, Vertex[] vertices, Texture2D diffuse)
		{
			this.meshes = new[]
			{
				new ModelMesh(indexes, vertices)
				{
					DiffuseTexture = diffuse,
				}
			};
		}

		/// <summary>
		/// Instantiates a model with meshes.
		/// </summary>
		/// <param name="meshes">Array of model meshes.</param>
		public Model(ModelMesh[] meshes)
		{
			if (meshes == null)
				throw new ArgumentNullException("meshes");
			this.meshes = new ModelMesh[meshes.Length];
			for (int i = 0; i < this.meshes.Length; i++)
				this.meshes[i] = meshes[i];
		}

		/// <summary>
		/// Loads a model file into the model.
		/// </summary>
		/// <param name="assetManager">AssetManager that provides texture assets</param>
		/// <param name="fileName">File name of the model.</param>
		public void Load(AssetManager assetManager, string fileName)
		{
			using (var importer = new AssimpImporter())
			{
				var scene = importer.ImportFile(
					fileName,
					PostProcessSteps.Debone |
					PostProcessSteps.PreTransformVertices |
					PostProcessSteps.Triangulate |
					PostProcessSteps.FlipUVs);

				this.Load(new AssetLoadContext(assetManager, Path.GetFileNameWithoutExtension(fileName), "."), scene);
			}
		}

		void IAsset.Load(AssetLoadContext context, Stream stream, string extensionHint)
		{
			using (var importer = new AssimpImporter())
			{
				var scene = importer.ImportFileFromStream(
					stream,
					PostProcessSteps.Debone |
					PostProcessSteps.PreTransformVertices |
					PostProcessSteps.Triangulate |
					PostProcessSteps.FlipUVs,
					extensionHint);
				this.Load(context, scene);
			}
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Objekte verwerfen, bevor Bereich verloren geht")]
		private void Load(AssetLoadContext context, Scene scene)
		{
			Node[] nodes = new[] { scene.RootNode };
			if (scene.RootNode.ChildCount > 0)
				nodes = scene.RootNode.Children;

			List<ModelMesh> meshList = new List<ModelMesh>();
			for (int i = 0; i < nodes.Length; i++)
			{
				Node node = nodes[i];
				if (!node.HasMeshes)
					continue;

				for (int j = 0; j < node.MeshCount; j++)
				{
					Mesh mesh = scene.Meshes[node.MeshIndices[j]];
					Vertex[] vertices = new Vertex[mesh.VertexCount];

					Assimp.Material assimpMaterial = null;
					Texture2D diffuseTexture = null;
					if (scene.HasMaterials && mesh.MaterialIndex >= 0)
					{
						assimpMaterial = scene.Materials[mesh.MaterialIndex];

						var slot = assimpMaterial.GetTexture(Assimp.TextureType.Diffuse, 0);
						if (!string.IsNullOrWhiteSpace(slot.FilePath))
						{
							string textureFilePath = Path.GetDirectoryName(slot.FilePath) + "/" + Path.GetFileNameWithoutExtension(slot.FilePath);
							if (Path.IsPathRooted(textureFilePath))
								textureFilePath = Path.GetFileNameWithoutExtension(slot.FilePath);
							diffuseTexture = context.AssetManager.Load<Texture2D>(context.Directory + textureFilePath);
						}
					}

					Vector3D[] texcoord0 = null;
					Vector3D[] texcoord1 = null;

					if (mesh.HasTextureCoords(0))
						texcoord0 = mesh.GetTextureCoords(0);
					if (mesh.HasTextureCoords(1))
						texcoord1 = mesh.GetTextureCoords(1);

					uint[] indices = mesh.GetIndices();
					for (int k = 0; k < mesh.VertexCount; k++)
					{
						Vertex vertex = new Vertex();

						vertex.Position = new Vector3(
							mesh.Vertices[k].X,
							mesh.Vertices[k].Y,
							mesh.Vertices[k].Z);
						if (mesh.HasNormals)
						{
							vertex.Normal = new Vector3(
								mesh.Normals[k].X,
								mesh.Normals[k].Y,
								mesh.Normals[k].Z);
						}
						if (texcoord0 != null)
						{
							vertex.UV = new Vector2(
								texcoord0[k].X,
								texcoord0[k].Y);
						}
						if (texcoord1 != null)
						{
							vertex.UV2 = new Vector2(
								texcoord1[k].X,
								texcoord1[k].Y);
						}

						vertices[k] = vertex;
					}

					ModelMesh modelMesh = new ModelMesh(indices, vertices);
					modelMesh.DiffuseTexture = diffuseTexture;
					meshList.Add(modelMesh);
				}
			}
			this.meshes = meshList.ToArray();
		}

		/// <summary>
		/// Draws the model.
		/// </summary>
		public void Draw()
		{
			this.Draw(null);
		}

		/// <summary>
		/// Draws the model.
		/// </summary>
		/// <param name="setTexture">Callback that allows to set shader parameters</param>
		public void Draw(SetTexture setTexture)
		{
			if (this.meshes == null)
				return;
			foreach (var mesh in this.meshes)
			{
				if (setTexture != null)
					setTexture(TextureType.Diffuse, mesh.DiffuseTexture);
				mesh.Draw();
			}
		}

		/// <summary>
		/// Gets the meshes of the model.
		/// </summary>
		/// <returns>Array with meshes.</returns>
		public ModelMesh[] GetMeshes()
		{
			return this.meshes;
		}
	}

	/// <summary>
	/// Sets a texture with type.
	/// </summary>
	/// <param name="type">Texture type</param>
	/// <param name="texture">Texture to set.</param>
	public delegate void SetTexture(OpenWorld.Engine.TextureType type, OpenWorld.Engine.Texture texture);
}
