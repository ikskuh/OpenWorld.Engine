using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenWorld.Engine.SceneManagement;
using OpenWorld.Engine.UserInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine.CodeTest
{
	class Program : Game
	{
		static void Main(string[] args)
		{
			using(Window window = new Window(1024, 768))
			{
				window.Game = new Program();
				window.Run(60, 60);
			}
		}

		Scene scene;

		protected override void OnLoad()
		{
			this.Assets.RootDirectory = "../../../Assets/";
			GL.ClearColor(0.2f, 0.2f, 1.0f, 1.0f);

			var camera = new PerspectiveLookAtCamera();
			camera.LookAt(
				new Vector3(-2, 1.9f, -4.0f),
				new Vector3(0.0f, 0.0f, 0.0f));
			camera.Aspect = this.Window.Aspect;

			this.scene = new Scene();
			this.scene.Camera = camera;

			SceneNode child = new SceneNode();
			var renderer = child.Components.Add<Renderer>();
			renderer.Model = this.Assets.Load<Model>("crate");
			
			this.scene.Root.Children.Add(child);
		}

		protected override void OnUpdate(GameTime time)
		{
			this.scene.Update(time);
		}

		protected override void OnDraw(GameTime time)
		{
			// Clear the screen, as usual
			GL.Clear(ClearBufferMask.ColorBufferBit);

			this.scene.Draw(time);
		}
	}
}
