using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assimp;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using System.IO;
using System.Threading;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Represents a 3D model.
	/// </summary>
	[AssetExtension(".dae", ".obj")]
	public sealed partial class Model : Asset
	{
		static PostProcessSteps postProcessing =
			PostProcessSteps.Debone |
			PostProcessSteps.PreTransformVertices |
			PostProcessSteps.Triangulate |
			PostProcessSteps.FlipUVs |
			PostProcessSteps.CalculateTangentSpace;

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
				var scene = importer.ImportFile(fileName, postProcessing);

				this.Load(new AssetLoadContext(assetManager, Path.GetFileNameWithoutExtension(fileName), "."), scene);
			}
		}

		/// <summary>
		/// Loads the model.
		/// </summary>
		protected override void Load(AssetLoadContext context, Stream stream, string extensionHint)
		{
			using (var importer = new AssimpImporter())
			{
				var scene = importer.ImportFileFromStream(stream, postProcessing, extensionHint);
				this.Load(context, scene);
			}
		}

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
					Texture2D specularTexture = null;
					Texture2D normalMap = null;
					if (scene.HasMaterials && mesh.MaterialIndex >= 0)
					{
						assimpMaterial = scene.Materials[mesh.MaterialIndex];
						diffuseTexture = LoadTexture(context, assimpMaterial.GetTexture(Assimp.TextureType.Diffuse, 0));
						specularTexture = LoadTexture(context, assimpMaterial.GetTexture(Assimp.TextureType.Specular, 0));

						bool usedSRGB = Texture.UseSRGB;
						Texture.UseSRGB = false; // We need "default" texture loading because normal maps are already linear space
						normalMap = LoadTexture(context, assimpMaterial.GetTexture(Assimp.TextureType.Normals, 0));

						Texture.UseSRGB = usedSRGB;
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

						if (mesh.HasTangentBasis)
						{
							vertex.Tangent = new Vector3(
								mesh.Tangents[k].X,
								mesh.Tangents[k].Y,
								mesh.Tangents[k].Z);

							vertex.BiTangent = new Vector3(
								mesh.BiTangents[k].X,
								mesh.BiTangents[k].Y,
								mesh.BiTangents[k].Z);
						}

						vertices[k] = vertex;
					}

					Game.Current.InvokeOpenGL(() =>
						{
							ModelMesh modelMesh = new ModelMesh(indices, vertices);
							modelMesh.DiffuseTexture = diffuseTexture;
							modelMesh.SpecularTexture = specularTexture;
							modelMesh.NormalMap = normalMap;
							meshList.Add(modelMesh);
						});
				}
			}
			this.meshes = meshList.ToArray();
		}

		private static Texture2D LoadTexture(AssetLoadContext context, TextureSlot slot)
		{
			if (string.IsNullOrWhiteSpace(slot.FilePath))
				return null;
			string textureFilePath = Path.GetDirectoryName(slot.FilePath) + "/" + Path.GetFileNameWithoutExtension(slot.FilePath);
			if (Path.IsPathRooted(textureFilePath))
				textureFilePath = Path.GetFileNameWithoutExtension(slot.FilePath);
			return context.AssetManager.Load<Texture2D>(context.Directory + textureFilePath);
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
				if (setTexture != null)
					setTexture(TextureType.Specular, mesh.SpecularTexture);
				if (setTexture != null)
					setTexture(TextureType.NormalMap, mesh.NormalMap);
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
