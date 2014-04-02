using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement.Shaders
{
	sealed class LightShader : ObjectShader
	{
		public LightShader()
		{
			this.Load(Resource.GetString("OpenWorld.Engine.SceneManagement.Shaders.light.glsl"));
		}
	}
}
