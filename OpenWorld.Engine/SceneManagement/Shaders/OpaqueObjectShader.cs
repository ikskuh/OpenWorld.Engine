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
			this.Compile(Resource.GetString("OpenWorld.Engine.SceneManagement.Shaders.opaqueObject.glsl"));
		}
	}
}
