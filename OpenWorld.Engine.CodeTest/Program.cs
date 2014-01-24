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
				new Vector3(-0.1f, 1.9f, -4.0f),
				new Vector3(0.0f, 0.0f, 0.0f));
			this.cameraLeft.Area = new Box2(000, 0, 400, 480);

			this.cameraRight = new PerspectiveLookAtCamera();
			this.cameraRight.LookAt(
				new Vector3(0.1f, 1.9f, -4.0f),
				new Vector3(0.0f, 0.0f, 0.0f));
			this.cameraRight.Area = new Box2(400, 0, 800, 480);

			this.renderer = new DeferredRenderer(400, 480);

			this.scene = new Scene(SceneCreationFlags.EnablePhysics);

			SceneNode child = new SceneNode();
			var renderer = child.Components.Add<Renderer>();
			renderer.Model = this.Assets.Load<Model>("crate");
			child.Components.Add<Scriptable>().Script =
@"function update(self)
	self.Node.Transform:Rotate(0, 0.2, 0)
end";
			this.scene.Root.Children.Add(child);

			SceneNode light = new SceneNode();
			light.Components.Add<PointLight>();
			light.Transform.Position = new Vector3(0, 0, 2);
			light.Parent = child;

			SceneNode demo = new SceneNode();
			demo.Components.Add<Renderer>().Model = this.Assets.Load<Model>("demoscene");
			demo.Transform.SetMatrix(Matrix4.CreateScale(0.5f) * Matrix4.CreateTranslation(0, -2, 0));
			demo.Parent = scene.Root;
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
			this.scene.Draw(this.cameraRight, this.renderer, time);
		}
	}
}
