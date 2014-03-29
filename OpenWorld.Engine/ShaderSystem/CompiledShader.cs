using OpenTK.Graphics.OpenGL4;
using OpenWorld.Engine.ShaderSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.ShaderSystem
{
	/// <summary>
	/// Provides a compiled OpenGL shader program.
	/// </summary>
	public sealed class CompiledShader : IGLResource
	{
		readonly Shader parent;
		int id;
		private Uniform[] uniformByLocation;
		private Dictionary<string, Uniform> uniformByName;

		internal CompiledShader(Shader parent, int programId)
		{
			this.parent = parent;
			this.id = programId;
			OpenGL.Invoke(this.ReadUniforms);
		}

		/// <summary>
		/// Reads all uniforms with type, id and name.
		/// </summary>
		private void ReadUniforms()
		{
			int maxLength, count;
			GL.GetProgram(this.id, GetProgramParameterName.ActiveUniformMaxLength, out maxLength);
			GL.GetProgram(this.id, GetProgramParameterName.ActiveUniforms, out count);

			this.uniformByLocation = new Uniform[count];
			this.uniformByName = new Dictionary<string, Uniform>();
			for (int i = 0; i < this.uniformByLocation.Length; i++)
			{
				int size, length;
				ActiveUniformType type;
				StringBuilder nameBuilder = new StringBuilder(maxLength);

				GL.GetActiveUniform(this.id, i, maxLength, out length, out size, out type, nameBuilder);

				string name = nameBuilder.ToString();

				var uniform = new Uniform(this, i, name, type, size);
				this.uniformByLocation[i] = uniform;
				this.uniformByName.Add(name, uniform);
			}
		}

		/// <summary>
		/// Binds the shader program.
		/// </summary>
		public void Bind()
		{
			OpenGL.ValidateThread();
			GL.UseProgram(this.id);
			if(this.Shader.Variables != null)
				this.BindUniform(this.Shader.Variables);
		}

		/// <summary>
		/// Binds all uniforms defined in source to this shader.
		/// </summary>
		/// <param name="source">Object that contains the uniforms.</param>
		public void BindUniform(object source)
		{
			if (source == null) return;
			var proxy = AutoUniforms.Get(source);

			proxy.SetUniforms(this);
		}

		/// <summary>
		/// Binds all uniforms defined in the sources to this shader.
		/// </summary>
		/// <param name="sources"></param>
		public void BindUniforms(params object[] sources)
		{
			foreach (var source in sources)
				this.BindUniform(source);
		}

		/// <summary>
		/// Gets the texture slot for a uniform variable.
		/// </summary>
		/// <param name="uniform"></param>
		/// <returns></returns>
		public int GetTextureSlot(Uniform uniform)
		{
			int slot = 0;
			for (int i = 0; i < uniform.Location; i++)
			{
				if (this.uniformByLocation[i].IsSampler)
					slot++;
			}
			return slot;
		}

		/// <summary>
		/// Destroys the program and releases all resources.
		/// </summary>
		public void Dispose()
		{
			if (this.id != 0)
			{
				GL.DeleteProgram(this.id);
				this.id = 0;
			}
		}

		/// <summary>
		/// Gets the shader that created this compiled shader.
		/// </summary>
		public Shader Shader
		{
			get { return parent; }
		}

		/// <summary>
		/// Gets the program id.
		/// </summary>
		public int Id
		{
			get { return this.id; }
		}

		/// <summary>
		/// Gets the amount of active uniforms in the shader.
		/// </summary>
		public int UniformCount
		{
			get { return (this.uniformByLocation ?? new Uniform[0]).Length; }
		}

		/// <summary>
		/// Gets if the shader has a tesselation stage.
		/// </summary>
		public bool HasTesselation { get; private set; }

		/// <summary>
		/// Gets a shader uniform by location.
		/// </summary>
		/// <param name="location">Location of the uniform.</param>
		/// <returns>Uniform at location.</returns>
		public Uniform this[int location]
		{
			get { return this.uniformByLocation[location]; }
		}

		/// <summary>
		/// Gets a shader uniform by name.
		/// </summary>
		/// <param name="name">Name of the uniform.</param>
		/// <returns>Uniform with the name or null.</returns>
		public Uniform this[string name]
		{
			get
			{
				if (!this.uniformByName.ContainsKey(name))
					return null;
				return this.uniformByName[name];
			}
		}
	}
}
