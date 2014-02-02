using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	[AssetExtension(".dds")]
	public abstract partial class Texture : Asset, IGLResource
	{
		private TextureTarget target;
		private Filter filter;
		private TextureWrapMode wrapS, wrapT, wrapR;
		private int id;

		/// <summary>
		/// Instantiates a new texture
		/// </summary>
		/// <param name="target">Texture target</param>
		protected Texture(TextureTarget target)
		{
			Game.Current.InvokeOpenGL(() =>
					{
						this.target = target;
						this.id = GL.GenTexture();

						this.WrapS = TextureWrapMode.Repeat;
						this.WrapT = TextureWrapMode.Repeat;
						this.WrapR = TextureWrapMode.Repeat;

						this.Filter = Filter.Nearest;
					});
		}

		protected override void OnUnload()
		{
			this.Dispose();
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
					Game.Current.InvokeOpenGL(() =>
					   {
						   GL.DeleteTexture(this.id);
					   });
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
				this.wrapS = value;
				Game.Current.InvokeOpenGL(() =>
				{
					this.Bind();
					GL.TexParameter(this.Target, TextureParameterName.TextureWrapS, (int)this.wrapS);
				});
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
				this.wrapT = value;
				Game.Current.InvokeOpenGL(() =>
				{
					this.Bind();
					GL.TexParameter(this.Target, TextureParameterName.TextureWrapT, (int)this.wrapS);
				});
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
				this.wrapR = value;
				Game.Current.InvokeOpenGL(() =>
				{
					this.Bind();
					GL.TexParameter(this.Target, TextureParameterName.TextureWrapR, (int)this.wrapS);
				});
			}
		}

		/// <summary>
		/// Gets or sets the texture filtering.
		/// </summary>
		public Filter Filter
		{
			get { return this.filter; }
			set
			{
				if (this.filter == value)
					return;
				this.filter = value;
				int min = (int)TextureMinFilter.Nearest;
				int mag = (int)TextureMagFilter.Nearest;
				switch (this.filter)
				{
					case Filter.Nearest:
						min = (int)TextureMinFilter.Nearest;
						mag = (int)TextureMagFilter.Nearest;
						break;
					case Filter.Linear:
						min = (int)TextureMinFilter.Linear;
						mag = (int)TextureMagFilter.Linear;
						break;
					case Filter.LinearMipMapped:
						min = (int)TextureMinFilter.LinearMipmapLinear;
						mag = (int)TextureMagFilter.Linear;
						break;
					default:
						throw new ArgumentException("Filter is not valid.", "filter");
				}
				Game.Current.InvokeOpenGL(() =>
					{
						this.Bind();
						GL.TexParameter(this.Target, TextureParameterName.TextureMagFilter, mag);
						GL.TexParameter(this.Target, TextureParameterName.TextureMinFilter, min);
					});
			}
		}

		/// <summary>
		/// Gets or sets a value that determines wheather sRGB is used or not.
		/// </summary>
		public static bool UseSRGB { get; set; }
	}
}
