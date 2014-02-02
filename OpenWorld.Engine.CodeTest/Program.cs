using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenWorld.Engine.SceneManagement;
using OpenWorld.Engine.Sound;
using OpenWorld.Engine.UserInterface;
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
			using(Window window = new Window(800, 480))
			{
				window.Game = new Program();
				window.Run(60, 60);
			}
		}

		Scene scene;
		SceneNode cameraNode;
		CompositeCamera camera;
		DeferredRenderer renderer;

		protected override void OnLoad()
		{
			Texture2D.UseSRGB = true;

			this.Assets.Sources.Add(new FileSystemAssetSource("../../../Assets/"));
			FrameBuffer.ClearColor = Color.SkyBlue;

			this.scene = new Scene(SceneCreationFlags.EnablePhysics);

			this.cameraNode = new SceneNode();
			this.cameraNode.Transform.LocalPosition = new Vector3(8.0f, 10.0f, 5.0f);
			this.cameraNode.Transform.LookAt(new Vector3(0.0f, -2.0f, 0.0f));
			this.cameraNode.Parent = this.scene.Root;

			this.camera = new CompositeCamera();
			this.camera.ProjectionMatrixSource = new Perspective(70.0f);
			this.camera.ViewMatrixSource = this.cameraNode;

			this.renderer = new DeferredRenderer(800, 480);

			//this.Assets.Load<Model>("rope");

			CreateBox();

			SceneNode demo = new SceneNode();
			demo.Components.Add<Renderer>().Model = this.Assets.Load<Model>("demoscene");
			demo.Components.Add<PolygonShape>().Model = this.Assets.Load<Model>("demoscene");
			demo.Components.Add<RigidBody>();
			demo.Transform.LocalPosition = new Vector3(0, -11, 0);
			demo.Parent = scene.Root;

			SceneNode fesant = new SceneNode();
			fesant.Components.Add<Renderer>().Model = this.Assets.Load<Model>("felix/fesant");
			fesant.Transform.SetMatrix(Matrix4.CreateScale(0.125f) * Matrix4.CreateRotationX(-GameMath.ToRadians(90)) * Matrix4.CreateTranslation(0, -10, -4));
			fesant.Parent = scene.Root;

			SceneNode light = new SceneNode();
			light.Components.Add<PointLight>();
			light.Transform.LocalPosition = new Vector3(2, 8, 1);
			light.Parent = scene.Root;

			var soundContainer = new SceneNode();
			soundContainer.Components.Add<Scriptable>().Script =
@"function update(self)
	self.Node.Transform:Rotate(0, 0.4, 0);
end";
			soundContainer.Parent = this.cameraNode;

			var soundEmitter = new SceneNode();
			var soundSource = soundEmitter.Components.Add<Sound3D>();
			soundSource.Sound = this.Assets.Load<AudioBuffer>("Sounds/computerbeep");
			soundSource.AutoPlay = true;
			soundSource.IsLooped = true;
			soundEmitter.Components.Add<Renderer>().Model = Model.CreateCube(0.25f);
			soundEmitter.Transform.LocalPosition = new Vector3(0, 0, 5);
			soundEmitter.Parent = soundContainer;
		}

		private void CreateBox()
		{
			SceneNode child = new SceneNode();
			var renderer = child.Components.Add<Renderer>();
			renderer.Model = this.Assets.Load<Model>("crate");
			var light = child.Components.Add<PointLight>();
			Random rnd = new Random();
			light.Color = new Color(
				(float)rnd.NextDouble(),
				(float)rnd.NextDouble(),
				(float)rnd.NextDouble());
			child.Components.Add<SphereShape>();
			child.Components.Add<RigidBody>().Mass = 1.0f;
			child.Transform.LocalPosition = new Vector3(0, 5, 0);
			this.scene.Root.Children.Add(child);
		}

		protected override void OnUpdate(GameTime time)
		{
			if(this.Input.Keyboard[OpenTK.Input.Key.Space])
				CreateBox();

			this.scene.Update(time);

			AudioListener.Instance.Position = this.cameraNode.Transform.WorldPosition;
			AudioListener.Instance.LookAt = this.cameraNode.Transform.Forward;
		}

		protected override void OnDraw(GameTime time)
		{
			// Clear the screen, as usual
			GL.Clear(ClearBufferMask.ColorBufferBit);

			this.scene.Draw(this.camera, this.renderer, time);
		}
	}
}
