using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenWorld.Engine.SceneManagement;
using OpenWorld.Engine.Sound;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine.CodeTest
{
	class Program : Game
	{

		static void Main(string[] args)
		{
			var game = new Program();
			game.Run();
		}

		Scene scene;
		PerspectiveLookAtCamera camera;
		SceneRenderer renderer;
		SceneNode cursor;

		protected override void OnLoad()
		{
			this.Assets.Sources.Add(new FileSystemAssetSource("../../../Assets/"));
			this.Assets.Sources.Add(new FileSystemAssetSource("./"));
			FrameBuffer.ClearColor = Color.SkyBlue;

			this.camera = new PerspectiveLookAtCamera();
			this.camera.LookAt(new Vector3(3, 2, 1), new Vector3(0, 0, 0));

			this.renderer = new SimpleRenderer();

			this.scene = new Scene(SceneCreationFlags.EnablePhysics);

			var plane = this.Assets.LoadSync<Model>("testplane");
			//plane.GetMeshes()[0].DiffuseTexture = this.texture;

			var ground = new SceneNode();
			ground.Components.Add<Renderer>().Model = plane;
			ground.Transform.LocalTransform = Matrix4.CreateRotationX(MathHelper.PiOver2) * Matrix4.CreateScale(5.0f);
			ground.Parent = this.scene.Root;

			CreateBox(new Vector3(0, 0.5f, 0));

			cursor = new SceneNode();
			cursor.Components.Add<Renderer>().Model = Model.CreateCube(0.1f);
			cursor.Parent = this.scene.Root;
		}

		private void CreateBox(Vector3 boxPosition)
		{
			var box = new SceneNode();
			box.Components.Add<Renderer>().Model = Assets.Load<Model>("crate");
			var boxShape = box.Components.Add<BoxShape>();
			boxShape.Width = 1.0f;
			boxShape.Height = 1.0f;
			boxShape.Length = 1.0f;
			box.Components.Add<RigidBody>().Mass = 0.0f;
			box.Transform.LocalPosition = boxPosition;
			box.Parent = this.scene.Root;
		}

		protected override void OnUpdate(GameTime time)
		{
			var ray = camera.CreateRay(Input.Mouse.X, Input.Mouse.Y);
			var raycastResult = scene.Raycast(ray, 200.0f);
			if (raycastResult != null)
				cursor.Transform.LocalPosition = raycastResult.Position;

			this.scene.Update(time);
		}

		protected override void OnDrawPreState(GameTime time)
		{
			// Clear the screen, as usual
			GL.Clear(ClearBufferMask.ColorBufferBit);

			this.scene.Draw(this.camera, this.renderer, time);
		}
	}
}
