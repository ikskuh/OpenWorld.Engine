using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	[AssetExtension(".dds")]
	public partial class Texture : IGLResource
	{
		private TextureTarget target;
		private TextureWrapMode wrapS, wrapT, wrapR;
		private int id;

		/// <summary>
		/// Instantiates a new texture
		/// </summary>
		/// <param name="target">Texture target</param>
		protected Texture(TextureTarget target)
		{
			this.target = target;
			this.id = GL.GenTexture();

			this.WrapS = TextureWrapMode.Repeat;
			this.WrapT = TextureWrapMode.Repeat;
			this.WrapR = TextureWrapMode.Repeat;
		}

		/// <summary>
		/// Binds the texture to its target.
		/// </summary>
		public void Bind()
		{
			GL.BindTexture(this.target, this.id);
		}

		/// <summary>
		/// Disposes the texture
		/// </summary>
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Disposes the texture
		/// </summary>
		/// <param name="disposing"></param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.id != 0)
				{
					GL.DeleteTexture(this.id);
					this.id = 0;
				}
			}
		}

		/// <summary>
		/// Target of the texture
		/// </summary>
		public TextureTarget Target
		{
			get { return target; }
		}

		/// <summary>
		/// Gets the OpenGL texture id.
		/// </summary>
		public int Id { get { return this.id; } }

		/// <summary>
		/// Gets or sets the wrap mode for the x coordinate
		/// </summary>
		public TextureWrapMode WrapS
		{
			get
			{
				return this.wrapS;
			}
			set
			{
				this.Bind();
				this.wrapS = value;
				GL.TexParameter(this.Target, TextureParameterName.TextureWrapS, (int)this.wrapS);
			}
		}

		/// <summary>
		/// Gets or sets the wrap mode for the y coordinate
		/// </summary>
		public TextureWrapMode WrapT
		{
			get
			{
				return this.wrapT;
			}
			set
			{
				this.Bind();
				this.wrapT = value;
				GL.TexParameter(this.Target, TextureParameterName.TextureWrapT, (int)this.wrapS);
			}
		}

		/// <summary>
		/// Gets or sets the wrap mode for the z coordinate
		/// </summary>
		public TextureWrapMode WrapR
		{
			get
			{
				return this.wrapR;
			}
			set
			{
				this.Bind();
				this.wrapR = value;
				GL.TexParameter(this.Target, TextureParameterName.TextureWrapR, (int)this.wrapS);
			}
		}
	}
}
