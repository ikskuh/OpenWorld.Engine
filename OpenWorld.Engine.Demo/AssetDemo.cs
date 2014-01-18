using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenWorld.Engine.Sound;

namespace OpenWorld.Engine.Demo
{
	class AssetDemo : Game
	{
		ObjectShader shader;
		Model model;
        AudioBuffer sound;
        AudioSource source;

		protected override void OnLoad()
		{
			// Sky blue and 1.0f clear depth
			GL.ClearColor(0.3f, 0.3f, 1.0f, 1.0f);
			GL.ClearDepth(1.0f);
			GL.Enable(EnableCap.DepthTest);
			GL.DepthFunc(DepthFunction.Lequal);

			// Setup the asset directory
			this.Assets.Sources.Add(new FileSystemAssetSource("../../../Assets/"));

			this.model = this.Assets.Load<Model>("crate"); // No file extension needed
            this.sound = this.Assets.Load<AudioBuffer>("Birdy01");
            source = new AudioSource(sound);
            source.Play();


			// Create and set up object shader
			bool useDefaultShader = true;
			if (useDefaultShader)
			{
				// Use the default shader
				this.shader = new ObjectShader(); 
			}
			else
			{
				// Or load custom one with asset manager
				this.shader = this.Assets.Load<ObjectShader>("normals");
			}
			this.shader.Projection = Matrix4.CreatePerspectiveFieldOfView(
				GameMath.ToRadians(60), // 60° field of view
				this.Window.Aspect,     // Just take the window aspect
				0.1f,                   // z near
				1000.0f);               // z far
			this.shader.View = Matrix4.LookAt(
				new Vector3(1.0f, 2.0f, -2.5f), // A little aside
				new Vector3(0.0f, 0.0f, 0.0f),  // Center
				new Vector3(0.0f, 1.0f, 0.0f)); // Y up
		}

		protected override void OnUpdate(GameTime time)
		{
			// Rotate the model with time
			this.shader.World = Matrix4.CreateRotationY(time.TotalTime);
		}

		protected override void OnDraw(GameTime time)
		{
			// Clear depth and color
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			this.shader.Use(); // Use our shader to render the model

			// Now render our model with a lamba delegate to set up textures
			this.model.Draw((type, texture) =>
				{
					// Set the diffuse texture
					if (type == TextureType.Diffuse)
						this.shader.DiffuseTexture = texture;
					
					// Setup the changes in the shader
					this.shader.Apply(); 
				});
		}
	}
}
