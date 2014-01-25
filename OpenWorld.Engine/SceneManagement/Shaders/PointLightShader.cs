using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement.Shaders
{
	class PointLightShader : ObjectShader
	{
		public PointLightShader()
		{
			this.Compile(
				Resource.GetString("OpenWorld.Engine.SceneManagement.Shaders.light.vs"),
				Resource.GetString("OpenWorld.Engine.SceneManagement.Shaders.light.fs"));
		}

		[Uniform("LightCenter")]
		public Vector3 Position { get; set; }

		[Uniform("Color")]
		public Color Color { get; set; }

		[Uniform("Radius")]
		public float Radius { get; set; }

		[Uniform("ViewPosition")]
		public Vector3 ViewPosition { get; set; }

		[Uniform("texturePosition")]
		public Texture PositionBuffer { get; set; }


		[Uniform("textureNormal")]
		public Texture NormalBuffer { get; set; }
	}
}
