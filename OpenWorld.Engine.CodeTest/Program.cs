using Awesomium.Core;
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
		WebSession session;
		WebView view;

		static void Main(string[] args)
		{
			var game = new Program();
			game.Run();
		}

		Scene scene;
		PerspectiveLookAtCamera camera;
		SceneRenderer renderer;
		Texture2D texture;
		Model boxModel;

		protected override void OnLoad()
		{
			this.Assets.Sources.Add(new FileSystemAssetSource("../../../Assets/"));
			this.Assets.Sources.Add(new FileSystemAssetSource("./"));
			FrameBuffer.ClearColor = Color.SkyBlue;

			WebConfig config = new WebConfig()
			{

			};
			WebCore.Initialize(config, true);

			this.session = WebCore.CreateWebSession(new WebPreferences()
			{
				WebAudio = false,
				WebGL = false,
			});

			this.texture = new Texture2D(512, 512, PixelInternalFormat.Rgba, PixelFormat.Rgba, PixelType.UnsignedByte);

			this.view = WebCore.CreateWebView(512, 512, this.session, WebViewType.Offscreen);
			this.view.Source = new Uri("http://www.masterq32.de/");

			this.view.CreateSurface += (s, e) =>
			{
				var bmpSurface = new BitmapSurface(e.Width, e.Height);
				bmpSurface.Updated += (s1, e1) =>
				{
					this.texture.Load(bmpSurface.Buffer, bmpSurface.Width, bmpSurface.Height);
				};
				e.Surface = bmpSurface;
			};

			this.camera = new PerspectiveLookAtCamera();
			this.camera.LookAt(new Vector3(3, 2, 1), new Vector3(0, 0, 0));

			this.renderer = new SimpleRenderer();

			this.scene = new Scene();

			//var plane = this.Assets.LoadSync<Model>("testplane");

			//var ground = new SceneNode();
			//ground.Components.Add<Renderer>().Model = plane;
			//ground.Transform.LocalTransform = Matrix4.CreateRotationX(MathHelper.PiOver2) * Matrix4.CreateScale(5.0f);
			//ground.Parent = this.scene.Root;

			var boxShader = Shader.CompileNew(File.ReadAllText("../../../Assets/customShader.glsl"));

			boxModel = Model.CreateCube(1.0f);
			boxModel.GetMeshes()[0].DiffuseTexture = this.texture;

			var box = new SceneNode();
			box.Components.Add<Renderer>().Model = boxModel;// this.Assets.Load<Model>("crate");
			box.Transform.LocalPosition = new Vector3(0, 0.5f, 0);
			box.Material = new Material()
			{
				IsTranslucent = false,
				//Shader = boxShader,
			};
			box.Parent = this.scene.Root;

			Input.Mouse.Move += Mouse_Move;
			Input.Mouse.ButtonDown += Mouse_ButtonDown;
			Input.Mouse.ButtonUp += Mouse_ButtonUp;
			Input.Mouse.WheelChanged += Mouse_WheelChanged;
		}

		void Mouse_ButtonDown(object sender, OpenTK.Input.MouseButtonEventArgs e)
		{
			switch (e.Button)
			{
				case OpenTK.Input.MouseButton.Left:
					view.InjectMouseDown(MouseButton.Left);
					break;
				case OpenTK.Input.MouseButton.Middle:
					view.InjectMouseDown(MouseButton.Middle);
					break;
				case OpenTK.Input.MouseButton.Right:
					view.InjectMouseDown(MouseButton.Right);
					break;
			}
		}

		void Mouse_ButtonUp(object sender, OpenTK.Input.MouseButtonEventArgs e)
		{
			switch (e.Button)
			{
				case OpenTK.Input.MouseButton.Left:
					view.InjectMouseUp(MouseButton.Left);
					break;
				case OpenTK.Input.MouseButton.Middle:
					view.InjectMouseUp(MouseButton.Middle);
					break;
				case OpenTK.Input.MouseButton.Right:
					view.InjectMouseUp(MouseButton.Right);
					break;
			}
		}

		void Mouse_WheelChanged(object sender, OpenTK.Input.MouseWheelEventArgs e)
		{
			if (Input.Keyboard[OpenTK.Input.Key.ShiftLeft])
				view.InjectMouseWheel(0, 20 * e.Delta);
			else
				view.InjectMouseWheel(20 * e.Delta, 0);
		}

		void Mouse_Move(object sender, OpenTK.Input.MouseMoveEventArgs e)
		{
			view.InjectMouseMove(e.X, e.Y);
		}

		protected override void OnUpdate(GameTime time)
		{
			this.scene.Update(time);
		}

		protected override void OnDraw(GameTime time)
		{
			WebCore.Update();

			// Clear the screen, as usual
			GL.Clear(ClearBufferMask.ColorBufferBit);

			this.scene.Draw(this.camera, this.renderer, time);
		}
	}
}
