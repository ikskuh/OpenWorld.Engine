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
			this.Load(Resource.GetString("OpenWorld.Engine.SceneManagement.Shaders.geometry.glsl"));
		}
	}
}
