using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// 
	/// </summary>
	public sealed class Uniform
	{
		readonly CompiledShader shader;

		internal Uniform(CompiledShader cs, int location, string name, ActiveUniformType type, int size)
		{
			this.shader = cs;
			this.Location = location;
			this.Name = name;
			this.Type = type;
			this.Size = size;
		}

		#region SetValue

		private void ValidateType(ActiveUniformType type)
		{
			if (this.Type == type)
				return;
			throw new InvalidOperationException("The value you passed does not fit the uniforms type.");
		}

		/// <summary>
		/// Sets the value of the uniform.
		/// </summary>
		/// <param name="value"></param>
		public void SetValue(bool value)
		{
			ValidateType(ActiveUniformType.Float);
			GL.ProgramUniform1(this.shader.Id, this.Location, value ? -1 : 0);
		}

		/// <summary>
		/// Sets the value of the uniform.
		/// </summary>
		/// <param name="value"></param>
		public void SetValue(float value)
		{
			ValidateType(ActiveUniformType.Float);
			GL.ProgramUniform1(this.shader.Id, this.Location, value);
		}

		/// <summary>
		/// Sets the value of the uniform.
		/// </summary>
		/// <param name="value"></param>
		public void SetValue(Vector2 value)
		{
			ValidateType(ActiveUniformType.FloatVec2);
			GL.ProgramUniform2(this.shader.Id, this.Location, value.X, value.Y);
		}

		/// <summary>
		/// Sets the value of the uniform.
		/// </summary>
		/// <param name="value"></param>
		public void SetValue(Vector3 value)
		{
			ValidateType(ActiveUniformType.FloatVec3);
			GL.ProgramUniform3(this.shader.Id, this.Location, value.X, value.Y, value.Z);
		}

		/// <summary>
		/// Sets the value of the uniform.
		/// </summary>
		/// <param name="value"></param>
		public void SetValue(Vector4 value)
		{
			ValidateType(ActiveUniformType.FloatVec4);
			GL.ProgramUniform4(this.shader.Id, this.Location, value.X, value.Y, value.Z, value.W);
		}

		/// <summary>
		/// Sets the value of the uniform.
		/// </summary>
		/// <param name="value"></param>
		public void SetValue(Color value)
		{
			ValidateType(ActiveUniformType.FloatVec4);
			GL.ProgramUniform4(this.shader.Id, this.Location, value.R, value.G, value.B, value.A);
		}

		/// <summary>
		/// Sets the value of the uniform.
		/// </summary>
		/// <param name="value"></param>
		public void SetValue(int value)
		{
			ValidateType(ActiveUniformType.Int);
			GL.ProgramUniform1(this.shader.Id, this.Location, value);
		}

		/// <summary>
		/// Sets the value of the uniform.
		/// </summary>
		/// <param name="value"></param>
		public void SetValue(double value)
		{
			ValidateType(ActiveUniformType.Double);
			GL.ProgramUniform1(this.shader.Id, this.Location, value);
		}

		/// <summary>
		/// Sets the value of the uniform.
		/// </summary>
		/// <param name="value"></param>
		public void SetValue(Matrix4 value)
		{
			this.SetValue(value, false);
		}

		/// <summary>
		/// Sets the value of the uniform.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="transpose">Transposes the matrix.</param>
		public void SetValue(Matrix4 value, bool transpose)
		{
			ValidateType(ActiveUniformType.FloatMat4);
			var array = new[]
			{
				value.M11, value.M12, value.M13, value.M14,
				value.M21, value.M22, value.M23, value.M24,
				value.M31, value.M32, value.M33, value.M34,
				value.M41, value.M42, value.M43, value.M44
			};
			GL.ProgramUniformMatrix4(this.shader.Id, this.Location, array.Length, transpose, array);
		}

		/// <summary>
		/// Sets the value of the uniform.
		/// </summary>
		/// <param name="value"></param>
		public void SetValue(Texture value)
		{
			if(!this.IsSampler)
				throw new InvalidOperationException("Trying to set a sampler value into a non-sampler uniform.");
			
			int slot = this.shader.GetTextureSlot(this);
			this.SetValue(slot);

			GL.ActiveTexture(TextureUnit.Texture0 + slot);
			if (value != null)
				value.Bind();
			else
				// missingTexture.Value.Bind();
				throw new NotImplementedException();
		}

		#endregion

		/// <summary>
		/// Gets if the uniform is any sampler type.
		/// </summary>
		public bool IsSampler
		{
			get
			{
				return
					this.Type == ActiveUniformType.Sampler1D ||
					this.Type == ActiveUniformType.Sampler1DArray ||
					this.Type == ActiveUniformType.Sampler1DArrayShadow ||
					this.Type == ActiveUniformType.Sampler1DShadow ||
					this.Type == ActiveUniformType.Sampler2D ||
					this.Type == ActiveUniformType.Sampler2DArray ||
					this.Type == ActiveUniformType.Sampler2DArrayShadow ||
					this.Type == ActiveUniformType.Sampler2DMultisample ||
					this.Type == ActiveUniformType.Sampler2DMultisampleArray ||
					this.Type == ActiveUniformType.Sampler2DRect ||
					this.Type == ActiveUniformType.Sampler2DRectShadow ||
					this.Type == ActiveUniformType.Sampler2DShadow ||
					this.Type == ActiveUniformType.Sampler3D ||
					this.Type == ActiveUniformType.SamplerBuffer ||
					this.Type == ActiveUniformType.SamplerCube ||
					this.Type == ActiveUniformType.SamplerCubeMapArray ||
					this.Type == ActiveUniformType.SamplerCubeMapArrayShadow ||
					this.Type == ActiveUniformType.SamplerCubeShadow;
			}
		}

		/// <summary>
		/// Gets the uniform type.
		/// </summary>
		public ActiveUniformType Type { get; private set; }

		/// <summary>
		/// Gets the uniform name.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Gets the uniform location.
		/// </summary>
		public int Location { get; private set; }

		/// <summary>
		/// Gets the uniform size in bytes.
		/// </summary>
		public int Size { get; private set; }

		/// <summary>
		/// Gets the compiled shader the uniform is for.
		/// </summary>
		public CompiledShader Shader { get { return shader; } }

		/// <summary>
		/// Converts the uniform to a string.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("{0} - {1}@{2}", this.Name, this.Type, this.Location);
		}
	}
}
