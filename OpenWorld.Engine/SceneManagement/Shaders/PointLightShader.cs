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

		protected override void OnApply()
		{
			base.OnApply();

			if (this.CheckUniformExists("Color"))
				this.SetUniform("Color", this.Color);
			if (this.CheckUniformExists("Radius"))
				this.SetUniform("Radius", this.Radius);
			if (this.CheckUniformExists("LightCenter"))
				this.SetUniform("LightCenter", this.Position);
			//this.SetUniform("LightDirection", this.Direction);

			if (this.CheckUniformExists("texturePosition"))
				this.SetTexture("texturePosition", this.PositionBuffer, 0);
			if (this.CheckUniformExists("textureNormal"))
				this.SetTexture("textureNormal", this.NormalBuffer, 1);
		}

		public Vector3 Position { get; set; }
		public Vector3 Direction { get; set; }
		public Color Color { get; set; }
		public float Radius { get; set; }

		public Texture PositionBuffer { get; set; }
		public Texture NormalBuffer { get; set; }
	}
}
