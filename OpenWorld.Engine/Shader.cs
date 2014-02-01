using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Represents an OpenGL shader program.
	/// </summary>
	public class Shader : Asset, IGLResource
	{
		private class AutomaticShaderUniform
		{
			// Using a closure here to get a maximum of performance
			public Action Apply;
		}

		static Shader currentShader = null;

		int programID;
		Dictionary<string, int> uniforms = new Dictionary<string, int>();

		private List<AutomaticShaderUniform> automaticUniforms;

		/// <summary>
		/// Instantiates a new shader.
		/// </summary>
		public Shader()
		{
			this.InitializeAutomaticUniforms();
		}

		/// <summary>
		/// Gets and initializes all automatic uniforms
		/// </summary>
		private void InitializeAutomaticUniforms()
		{
			this.automaticUniforms = new List<AutomaticShaderUniform>();

			foreach (var property in this.GetType().GetProperties())
			{
				if (!property.CanRead)
					continue;
				UniformAttribute[] attribs = (UniformAttribute[])property.GetCustomAttributes(typeof(UniformAttribute), false);
				if (attribs.Length != 1)
					continue;

				// Create a closure for each uniform.
				// The closure later can simply and fast assign the uniform values.

				AutomaticShaderUniform uniform = new AutomaticShaderUniform();

				Type propertyType = property.PropertyType;
				var name = attribs[0].UniformName;
				if (propertyType == typeof(float))
				{
					uniform.Apply = () =>
					{
						var value = property.GetValue(this, new object[0]);
						this.SetUniform(name, (float)value);
					};
				}
				else if (propertyType == typeof(Vector2))
				{
					uniform.Apply = () =>
					{
						var value = property.GetValue(this, new object[0]);
						this.SetUniform(name, (Vector2)value);
					};
				}
				else if (propertyType == typeof(Vector3))
				{
					uniform.Apply = () =>
					{
						var value = property.GetValue(this, new object[0]);
						this.SetUniform(name, (Vector3)value);
					};
				}
				else if (propertyType == typeof(Vector4))
				{
					uniform.Apply = () =>
					{
						var value = property.GetValue(this, new object[0]);
						this.SetUniform(name, (Vector4)value);
					};
				}
				else if (propertyType == typeof(Color))
				{
					uniform.Apply = () =>
					{
						var value = property.GetValue(this, new object[0]);
						this.SetUniform(name, (Color)value);
					};
				}
				else if (propertyType == typeof(Matrix4))
				{
					uniform.Apply = () =>
					{
						var value = property.GetValue(this, new object[0]);
						this.SetUniform(name, (Matrix4)value, attribs[0].Transpose);
					};
				}
				else if (typeof(Texture).IsAssignableFrom(propertyType))
				{
					uniform.Apply = () =>
					{
						var value = property.GetValue(this, new object[0]);
						this.SetTexture(name, (Texture)value, this.TextureCount);
						this.TextureCount++;
					};
				}
				else
				{
					Console.WriteLine("{0} is not supported for automatic uniform assignment.");
					continue;
				}
				this.automaticUniforms.Add(uniform);
			}
		}

		/// <summary>
		/// Compiles the shader.
		/// </summary>
		/// <param name="vertexSource">Vertex shader source</param>
		/// <param name="fragmentSource">Fragment shader source</param>
		public void Compile(string vertexSource, string fragmentSource)
		{
			int status;
			string infoLog;

			// Create the shaders
			int vertexShader = GL.CreateShader(ShaderType.VertexShader);
			int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);

			// Compile Vertex Shader
			GL.ShaderSource(vertexShader, vertexSource);
			GL.CompileShader(vertexShader);

			// Check Vertex Shader
			infoLog = GL.GetShaderInfoLog(vertexShader);
			if (!string.IsNullOrWhiteSpace(infoLog))
			{
				Log.WriteLine(LocalizedStrings.VertexShaderCompilerResult);
				Log.WriteLine(infoLog);
			}
			GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out status);
			if (status == 0)
				throw new ShaderCompilationException(infoLog);

			// Compile Fragment Shader
			GL.ShaderSource(fragmentShader, fragmentSource);
			GL.CompileShader(fragmentShader);

			// Check Fragment Shader
			infoLog = GL.GetShaderInfoLog(fragmentShader);
			if (!string.IsNullOrWhiteSpace(infoLog))
			{
				Log.WriteLine(LocalizedStrings.FragmentShaderCompilerResult);
				Log.WriteLine(infoLog);
			}
			GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out status);
			if (status == 0)
				throw new ShaderCompilationException(infoLog);

			// Link the program
			if (this.programID != 0)
			{
				this.Dispose();
			}
			this.programID = GL.CreateProgram();
			GL.AttachShader(this.programID, vertexShader);
			GL.AttachShader(this.programID, fragmentShader);
			GL.LinkProgram(this.programID);

			// Check the program
			infoLog = GL.GetProgramInfoLog(this.programID);
			if (!string.IsNullOrWhiteSpace(infoLog))
			{
				Log.WriteLine(LocalizedStrings.ShaderLinkerResult);
				Log.WriteLine(infoLog);
			}

			GL.GetProgram(this.programID, GetProgramParameterName.LinkStatus, out status);

			if (status == 0)
				throw new ShaderLinkingException(infoLog);

			GL.DeleteShader(vertexShader);
			GL.DeleteShader(fragmentShader);
		}

		/// <summary>
		/// Binds the shader.
		/// <remarks>Uses glUseProgram</remarks>
		/// </summary>
		public void Bind()
		{
			GL.UseProgram(this.programID);
		}

		/// <summary>
		/// Uses the shader
		/// <remarks>Binds the shader and sets the uniforms</remarks>
		/// </summary>
		public void Use()
		{
#if !DEBUG
			if (this.programID == 0)
				throw new InvalidOperationException("Shader was disposed or not compiled.");
#endif
			this.Bind();
			Shader.currentShader = this;

			this.Apply();
		}

		/// <summary>
		/// Applies the uniforms.
		/// </summary>
		public void Apply()
		{
			// Load all properties with a Uniform-Attribute
			this.TextureCount = 0; // Reset the texture count.

			foreach (var uniform in this.automaticUniforms)
				uniform.Apply();

			this.OnApply();
		}

		/// <summary>
		/// Checks if a uniform exists
		/// </summary>
		/// <param name="name">Name of the uniform</param>
		/// <returns>True if the uniform exists</returns>
		public bool CheckUniformExists(string name)
		{
			if (this.uniforms.ContainsKey(name))
				return true;
			return GL.GetUniformLocation(this.programID, name) >= 0;
		}

		/// <summary>
		/// Gets the location of the uniform.
		/// </summary>
		/// <param name="name">Name of the uniform</param>
		/// <returns>Shader location of the uniform.</returns>
		public int GetUniformLocation(string name)
		{
			if (!this.uniforms.ContainsKey(name))
			{
				var location = GL.GetUniformLocation(this.programID, name);
				if (location < 0)
#if DEBUG
					Log.WriteLine("Uniform {0} not found!", name);
#else
					throw new UniformNotFoundException(name);
#endif
				this.uniforms.Add(name, location);
			}
			return this.uniforms[name];
		}

		/// <summary>
		/// Sets a mat4 uniform.
		/// </summary>
		/// <param name="name">Name of the uniform</param>
		/// <param name="value">Value of the uniform</param>
		public void SetUniform(string name, Matrix4 value)
		{
			this.SetUniform(name, value, false);
		}

		/// <summary>
		/// Sets a mat4 uniform.
		/// </summary>
		/// <param name="name">Name of the uniform</param>
		/// <param name="value">Value of the uniform</param>
		/// <param name="transpose">If true, the matrix gets transposed</param>
		public void SetUniform(string name, Matrix4 value, bool transpose)
		{
			if (name == null)
				throw new ArgumentNullException("name");
			if (Shader.currentShader != this)
				this.Use();

			int location = this.GetUniformLocation(name);
			GL.UniformMatrix4(location, transpose, ref value);
		}

		/// <summary>
		/// Sets an int uniform.
		/// </summary>
		/// <param name="name">Name of the uniform</param>
		/// <param name="value">Value of the uniform</param>
		public void SetUniform(string name, int value)
		{
			if (Shader.currentShader != this)
				this.Use();

			int location = this.GetUniformLocation(name);
			GL.Uniform1(location, value);
		}


		/// <summary>
		/// Sets a ivec uniform.
		/// </summary>
		/// <param name="name">Name of the uniform</param>
		/// <param name="value">Value of the uniform</param>
		/// <remarks>The array length defines the vector length</remarks>
		public void SetUniform(string name, int[] value)
		{
			if (value == null)
				throw new ArgumentNullException("value");
			if (value.Length < 1 || value.Length > 4)
				throw new ArgumentException("value needs to have 1 to 4 elements.", "value");
			if (Shader.currentShader != this)
				this.Use();
			int location = this.GetUniformLocation(name);
			switch (value.Length)
			{
				case 1:
					GL.Uniform1(location, 1, value);
					break;
				case 2:
					GL.Uniform1(location, 2, value);
					break;
				case 3:
					GL.Uniform1(location, 3, value);
					break;
				case 4:
					GL.Uniform1(location, 4, value);
					break;
			}

		}


		/// <summary>
		/// Sets a float uniform.
		/// </summary>
		/// <param name="name">Name of the uniform</param>
		/// <param name="value">Value of the uniform</param>
		public void SetUniform(string name, float value)
		{
			if (Shader.currentShader != this)
				this.Use();

			int location = this.GetUniformLocation(name);
			GL.Uniform1(location, value);
		}


		/// <summary>
		/// Sets a vec2 uniform.
		/// </summary>
		/// <param name="name">Name of the uniform</param>
		/// <param name="value">Value of the uniform</param>
		public void SetUniform(string name, Vector2 value)
		{
			if (Shader.currentShader != this)
				this.Use();

			int location = this.GetUniformLocation(name);
			GL.Uniform2(location, value);
		}


		/// <summary>
		/// Sets a vec3 uniform.
		/// </summary>
		/// <param name="name">Name of the uniform</param>
		/// <param name="value">Value of the uniform</param>
		public void SetUniform(string name, Vector3 value)
		{
			if (Shader.currentShader != this)
				this.Use();

			int location = this.GetUniformLocation(name);
			GL.Uniform3(location, value);
		}


		/// <summary>
		/// Sets a vec4 uniform.
		/// </summary>
		/// <param name="name">Name of the uniform</param>
		/// <param name="value">Value of the uniform</param>
		public void SetUniform(string name, Vector4 value)
		{
			if (Shader.currentShader != this)
				this.Use();

			int location = this.GetUniformLocation(name);
			GL.Uniform4(location, value);
		}


		/// <summary>
		/// Sets a vec4 uniform.
		/// </summary>
		/// <param name="name">Name of the uniform</param>
		/// <param name="value">Value of the uniform</param>
		public void SetUniform(string name, Color value)
		{
			if (Shader.currentShader != this)
				this.Use();

			int location = this.GetUniformLocation(name);
			GL.Uniform4(location, new Vector4(value.R, value.G, value.B, value.A));
		}


		/// <summary>
		/// Sets a sampler2D uniform and binds a texture to a slot.
		/// </summary>
		/// <param name="name">Name of the uniform</param>
		/// <param name="texture">Texture to be bound to the sampler</param>
		/// <param name="slot">Slot id of the texture (0 to 31)</param>
		public void SetTexture(string name, Texture texture, int slot)
		{
			if (slot < 0 || slot >= 32)
				throw new ArgumentOutOfRangeException("slot", "Slot must be between 0 and 31.");

			if (Shader.currentShader != this)
				this.Use();

			this.SetUniform(name, slot);

			GL.ActiveTexture(TextureUnit.Texture0 + slot);
			if (texture != null)
				texture.Bind();
			//else
			// TODO: Bind blank white texture
		}

		/// <summary>
		/// Disposes the shader.
		/// </summary>
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Disposes the shader
		/// </summary>
		/// <param name="disposing"></param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.programID != 0)
				{
					GL.DeleteProgram(this.programID);
					this.programID = 0;
				}
				this.uniforms.Clear();
			}
		}

		/// <summary>
		/// Applies the shader variables
		/// </summary>
		protected virtual void OnApply()
		{

		}

		static readonly XmlSerializer serializer = new XmlSerializer(typeof(Source));
		protected override void Load(AssetLoadContext context, System.IO.Stream stream, string extensionHint)
		{
			
			Source source = serializer.Deserialize(stream) as Source;
			this.Compile(source.VertexShader, source.FragmentShader);
		}

		/// <summary>
		/// Gets the id of the OpenGL program.
		/// </summary>
		public int Id { get { return this.programID; } }

		/// <summary>
		/// Gets the number of automatically assigned textures.
		/// <remarks>This can be used as a base texture offset for custom textures.</remarks>
		/// </summary>
		protected int TextureCount { get; private set; }
		
		/// <summary>
		/// Defines a serializable shader code container.
		/// </summary>
		[Serializable]
		[XmlRoot("Shader")]
		public class Source
		{
			/// <summary>
			/// Gets or sets vertex shader source
			/// </summary>
			public string VertexShader { get; set; }

			/// <summary>
			/// Gets or sets fragment shader source
			/// </summary>
			public string FragmentShader { get; set; }
		}
	}
}
