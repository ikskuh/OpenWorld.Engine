using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines an abstract shader.
	/// </summary>
	[AssetExtension(".glsl")]
	public class Shader : Asset
	{
		private string source;
		private CompiledShader defaultShader;

		/// <summary>
		/// Creates a new shader.
		/// </summary>
		public Shader()
		{
			this.Variables = this;
		}

		/// <summary>
		/// Loads the shader source code for this 
		/// </summary>
		/// <param name="source">Source code of the shader.</param>
		public void Load(string source)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Selects the default shader program from this shader.
		/// </summary>
		/// <returns></returns>
		public CompiledShader Select()
		{
			throw new NotImplementedException();
			return this.defaultShader;
		}

		/// <summary>
		/// Selects the default shader program with overwritten shader fragments from this shader.
		/// </summary>
		/// <param name="fragments">Custom shader fragments that are used instead of the original shader.</param>
		/// <returns></returns>
		public CompiledShader Select(params ShaderFragment[] fragments)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Selects a shader program with a specific class from this shader.
		/// </summary>
		/// <param name="className">Shader class to select.</param>
		/// <returns></returns>
		public CompiledShader Select(string className)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Selects a shader program with a specific class with overwritten shader fragments from this shader.
		/// </summary>
		/// <param name="className">Shader class to select.</param>
		/// <param name="fragments">Custom shader fragments that are used instead of the original shader.</param>
		/// <returns></returns>
		public CompiledShader Select(string className, params ShaderFragment[] fragments)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Loads the shader as an asset.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="stream"></param>
		/// <param name="extensionHint"></param>
		protected override void Load(AssetLoadContext context, System.IO.Stream stream, string extensionHint)
		{
			using(var reader = new System.IO.StreamReader(stream))
			{
				this.Load(reader.ReadToEnd());
			}
		}

		/// <summary>
		/// Gets or sets the shader variables.
		/// </summary>
		/// <remarks>These variables will be passed to all compiled shaders as uniforms.</remarks>
		public object Variables { get; set; }
	}

	/// <summary>
	/// Defines an abstract shader that has 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Shader<T> : Shader
		where T : class
	{
		/// <summary>
		/// Creates a new shader.
		/// </summary>
		public Shader()
			: base()
		{
			// Reset variables to null, so we don't get inconsistence.
			this.Variables = null;
		}

		/// <summary>
		/// Gets or sets the shader variables.
		/// </summary>
		/// <remarks>These variables will be passed to all compiled shaders as uniforms.</remarks>
		public new T Variables
		{
			get
			{
				return base.Variables as T;
			}
			set
			{
				base.Variables = value;
			}
		}
	}
}
