using OpenTK;
using OpenTK.Graphics.OpenGL;
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

		protected override void OnLoad()
		{
			this.Assets.Sources.Add(new FileSystemAssetSource("../../../Assets/"));
			FrameBuffer.ClearColor = Color.SkyBlue;

			this.camera = new PerspectiveLookAtCamera();
			this.camera.LookAt(new Vector3(3, 2, 1), new Vector3(0, 0, 0));

			this.renderer = new SimpleRenderer();

			this.scene = new Scene();

			var ground = new SceneNode();
			ground.Components.Add<Renderer>().Model = this.Assets.Load<Model>("testplane");
			ground.Transform.LocalTransform = Matrix4.CreateRotationX(MathHelper.PiOver2) * Matrix4.CreateScale(5.0f);
			ground.Parent = this.scene.Root;

			var boxShader = this.Assets.Load<ObjectShader>("customShader");

			var box = new SceneNode();
			box.Components.Add<Renderer>().Model = this.Assets.Load<Model>("crate");
			box.Transform.LocalPosition = new Vector3(0, 0.5f, 0);
			box.Material = new Material()
			{
				IsTranslucent = false,
				Shader = boxShader,
			};
			box.Parent = this.scene.Root;
		}

		protected override void OnUpdate(GameTime time)
		{
			this.scene.Update(time);
		}

		protected override void OnDraw(GameTime time)
		{
			// Clear the screen, as usual
			GL.Clear(ClearBufferMask.ColorBufferBit);

			this.scene.Draw(this.camera, this.renderer, time);
		}
	}
}
