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
			this.Compile(Resource.GetString("OpenWorld.Engine.SceneManagement.Shaders.light.glsl"));
		}
	}
}
