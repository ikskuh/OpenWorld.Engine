using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement.Shaders
{
	class GeometryShader : ObjectShader
	{
		public GeometryShader()
		{
			this.Compile(
				Resource.GetString("OpenWorld.Engine.SceneManagement.Shaders.geometry.vs"),
				Resource.GetString("OpenWorld.Engine.SceneManagement.Shaders.geometry.fs"));
		}

		protected override void OnApply()
		{
			base.OnApply();
			this.SetTexture("textureNormalMap", this.NormalMap, 1);
		}

		public Texture NormalMap { get; set; }
	}
}
