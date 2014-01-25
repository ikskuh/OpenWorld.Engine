using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenWorld.Engine.SceneManagement;
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
		PerspectiveLookAtCamera cameraLeft;
		PerspectiveLookAtCamera cameraRight;
		DeferredRenderer renderer;

		protected override void OnLoad()
		{
			Texture2D.UseSRGB = true;

			this.Assets.Sources.Add(new FileSystemAssetSource("../../../Assets/"));
			GL.ClearColor(0.2f, 0.2f, 1.0f, 1.0f);

			this.cameraLeft = new PerspectiveLookAtCamera();
			this.cameraLeft.LookAt(
				new Vector3(2.0f, 10.0f, 16.0f),
				new Vector3(0.0f, 0.0f, 0.0f));

			//this.renderer = new DeferredRenderer(400, 480);
			this.renderer = new DeferredRenderer(800, 480);

			this.scene = new Scene(SceneCreationFlags.EnablePhysics);

			SceneNode child = new SceneNode();
			var renderer = child.Components.Add<Renderer>();
			renderer.Model = this.Assets.Load<Model>("crate");
			child.Components.Add<BoxShape>();
			child.Components.Add<RigidBody>().Mass = 1.0f;
			child.Transform.LocalPosition = new Vector3(0, 5, 0);
			this.scene.Root.Children.Add(child);

			SceneNode light = new SceneNode();
			light.Components.Add<PointLight>();
			light.Transform.LocalPosition = new Vector3(0, 0, 2);
			light.Parent = child;

			SceneNode demo = new SceneNode();
			demo.Components.Add<Renderer>().Model = this.Assets.Load<Model>("demoscene");

			var polygonShape = demo.Components.Add<PolygonShape>();
			polygonShape.Model = this.Assets.Load<Model>("demoscene");

			var demoRigidBody = demo.Components.Add<RigidBody>();
			//demoRigidBody.IsStatic = true;
			demo.Parent = scene.Root;

			SceneNode ground = new SceneNode();
			var boxShape = ground.Components.Add<BoxShape>();
			boxShape.Width = 100.0f;
			boxShape.Height = 0.5f;
			boxShape.Length = 100.0f;

			var groundRigidBody = ground.Components.Add<RigidBody>();
			//groundRigidBody.IsStatic = true;
			ground.Parent = scene.Root;
		}

		protected override void OnUpdate(GameTime time)
		{
			this.scene.Update(time);
		}

		protected override void OnDraw(GameTime time)
		{
			// Clear the screen, as usual
			GL.Clear(ClearBufferMask.ColorBufferBit);

			this.scene.Draw(this.cameraLeft, this.renderer, time);
			//this.scene.Draw(this.cameraRight, this.renderer, time);
		}
	}
}
