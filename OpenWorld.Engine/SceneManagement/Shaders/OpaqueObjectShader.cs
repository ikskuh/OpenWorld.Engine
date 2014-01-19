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

		protected override void OnApply()
		{
			base.OnApply();

			if (this.CheckUniformExists("textureLighting"))
				this.SetTexture("textureLighting", this.LightBuffer, 1);
		}

		public Texture LightBuffer { get; set; }
	}
}
