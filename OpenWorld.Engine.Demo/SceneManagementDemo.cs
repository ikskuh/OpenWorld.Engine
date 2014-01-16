using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenWorld.Engine.SceneManagement;

namespace OpenWorld.Engine.Demo
{
	class SceneManagementDemo : Game
	{
		// The scene we use
		Scene scene;

		// Our camera
		PerspectiveLookAtCamera camera;

		protected override void OnLoad()
		{
			// Set up the background color
			GL.ClearColor(0.2f, 0.2f, 1.0f, 1.0f);

			// Add a new asset source to the asset manager. Assets will now be searched in the file system.
			this.Assets.Sources.Add(new FileSystemAssetSource("../../../Assets/"));

			// Create our camera. A perspective camera with 60° field of view.
			this.camera = new PerspectiveLookAtCamera();
			
			// Set the camera position and target
			this.camera.LookAt(
				new Vector3(-0.1f, 1.9f, -4.0f),
				new Vector3(0.0f, 0.0f, 0.0f));

			this.scene = new Scene();

			SceneNode child = new SceneNode();

			// Add a component of type Renderer to the scene node.
			// A Renderer draws the scene node, so it can be shown.
			var renderer = child.Components.Add<Renderer>();
			// Load a new model and assign it to the renderer.
			// The asset manager will search in all asset sources to find the "crate" model.
			renderer.Model = this.Assets.Load<Model>("crate");

			// Add the SceneNode to the scene so the node gets updated and drawn.
			this.scene.Root.Children.Add(child);
		}

		protected override void OnUpdate(GameTime time)
		{
			// Update the scene and all of its nodes.
			this.scene.Update(time);
		}

		protected override void OnDraw(GameTime time)
		{
			// Clear the screen, as usual
			GL.Clear(ClearBufferMask.ColorBufferBit);

			// Draw our scene with the camera.
			this.camera.Draw(this.scene, time);
		}

	}
}
