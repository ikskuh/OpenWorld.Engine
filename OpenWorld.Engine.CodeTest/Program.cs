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
			using(Window window = new Window(1024, 768))
			{
				window.Game = new Program();
				window.Run(60, 60);
			}
		}

		class MyLocalization : Localization
		{
			[ID("name")]
			public string Name { get; set; }
		}

		Scene scene;
		PerspectiveLookAtCamera camera;

		protected override void OnLoad()
		{
			this.Assets.Sources.Add(new FileSystemAssetSource("../../../Assets/"));
			this.Assets.Sources.Add(new ZipFileAssetSource("../../../Assets/assets.zip"));
			this.Assets.Sources.Add(new WebAssetSource("http://picshare.masterq32.de/"));
			GL.ClearColor(0.2f, 0.2f, 1.0f, 1.0f);

			this.camera = new PerspectiveLookAtCamera();
			this.camera.LookAt(
				new Vector3(-0.1f, 1.9f, -4.0f),
				new Vector3(0.0f, 0.0f, 0.0f));

			this.scene = new Scene();

			SceneNode child = new SceneNode();
			var renderer = child.Components.Add<Renderer>();
			renderer.Model = this.Assets.Load<Model>("crate");

			var scriptable = child.Components.Add<Scriptable>();
			scriptable.Script =
@"function start(self)
	
end
function update(self)
	self.Node.Transform:Rotate(0, 0.2, 0)
end
function stop(self)

end";

			// Load asset from Zip
			//var textureZip = this.Assets.Load<Texture2D>("crateTexture");

			// Load asset from Web
			//var textureWeb = this.Assets.Load<Texture2D>("abe2ee"); 

			var localization = this.Assets.Load<MyLocalization>("language");

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

			this.camera.Draw(this.scene, time);
		}
	}
}
