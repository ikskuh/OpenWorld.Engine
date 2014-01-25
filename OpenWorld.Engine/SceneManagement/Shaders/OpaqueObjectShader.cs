using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement.Shaders
{
	class OpaqueObjectShader : ObjectShader
	{
		public OpaqueObjectShader()
		{
			this.Compile(
				Resource.GetString("OpenWorld.Engine.SceneManagement.Shaders.opaqueObject.vs"),
				Resource.GetString("OpenWorld.Engine.SceneManagement.Shaders.opaqueObject.fs"));
		}

		[Uniform("textureDiffuseColor")]
		public Texture DiffuseColorTexture { get; set; }

		[Uniform("textureSpecularColor")]
		public Texture SpecularColorTexture { get; set; }


		[Uniform("textureDiffuseLighting")]
		public Texture DiffuseLightBuffer { get; set; }

	[Uniform("textureSpecularLighting")]
		public Texture SpecularLightBuffer { get; set; }
	}
}
