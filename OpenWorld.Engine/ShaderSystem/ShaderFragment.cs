using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines an OpenGL shader object.
	/// </summary>
	public sealed class ShaderFragment : IGLResource
	{
		int id;
		ShaderType type;

		/// <summary>
		/// Creates a new shader fragment.
		/// </summary>
		/// <param name="type">Type of the shader fragment.</param>
		public ShaderFragment(ShaderType type)
		{
			this.type = type;
			OpenGL.Invoke(() => this.id = GL.CreateShader(type));
		}

		/// <summary>
		/// Creates a new shader fragment and compiles it.
		/// </summary>
		/// <param name="type">Type of the shader fragment.</param>
		/// <param name="source">Source code to compile.</param>
		public ShaderFragment(ShaderType type, string source)
			: this(type)
		{
			this.Compile(source);
		}

		/// <summary>
		/// Compiles the shader fragment.
		/// </summary>
		/// <param name="sources">Source code parts to compile.</param>
		public void Compile(params string[] sources)
		{
			GL.ShaderSource(this.id, sources.Length, sources, (int[])null);
			GL.CompileShader(this.id);

			int status;
			string infoLog = GL.GetShaderInfoLog(this.id);
			GL.GetShader(this.id, ShaderParameter.CompileStatus, out status);
			if(status != 0)
			{
				Log.WriteLine(LocalizedStrings.ShaderCompilerFailedResult, this.type);
				if (!string.IsNullOrWhiteSpace(infoLog))
				{
					Log.WriteLine(infoLog);
				}
			}
			else
			{
				if (!string.IsNullOrWhiteSpace(infoLog))
				{
					Log.WriteLine(LocalizedStrings.ShaderCompilerResult, this.type);
					Log.WriteLine(infoLog);
				}
			}
		}

		/// <summary>
		/// Fails.
		/// </summary>
		public void Bind()
		{
			throw new InvalidOperationException("Cannot bind an OpenGL shader object.");
		}

		/// <summary>
		/// Destroys the shader.
		/// </summary>
		public void Dispose()
		{
			if(this.id != 0)
			{
				GL.DeleteShader(this.id);
				this.id = 0;
			}
		}

		/// <summary>
		/// Gets the type of this shader fragment.
		/// </summary>
		public ShaderType Type
		{
			get { return type; }
		}

		/// <summary>
		/// The OpenGL shader object id.
		/// </summary>
		public int Id { get { return this.id; } }
	}
}
