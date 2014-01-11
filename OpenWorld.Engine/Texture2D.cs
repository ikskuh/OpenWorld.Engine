using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines a 2-dimensional texture.
	/// </summary>
	[AssetExtension(".png", ".bmp", ".dds")]
	public sealed class Texture2D : Texture, IAsset
	{
		/// <summary>
		/// Instantiates a Texture2D
		/// </summary>
		public Texture2D()
			: base(TextureTarget.Texture2D)
		{

		}

		/// <summary>
		/// Instantiates a new Texture2D
		/// </summary>
		/// <param name="width">Width of the texture</param>
		/// <param name="height">Height of the texture</param>
		/// <param name="internalFormat">Internal Format</param>
		/// <param name="pixelFormat">Pixel Format</param>
		/// <param name="pixelType">Pixel Type</param>
		public Texture2D(int width, int height, PixelInternalFormat internalFormat, OpenTK.Graphics.OpenGL.PixelFormat pixelFormat, PixelType pixelType)
			: this()
		{
			this.Bind();
			GL.TexImage2D(
				this.Target,
				0,
				internalFormat,
				width, height,
				0,
				pixelFormat,
				pixelType,
				IntPtr.Zero);
			this.SetFiltering(Filter.Nearest);
			this.Width = width;
			this.Height = height;
		}

		/// <summary>
		/// Instantiates a new Texture2D
		/// </summary>
		/// <param name="bmp">Bitmap that contains the pixel data</param>
		public Texture2D(Bitmap bmp)
			: this()
		{
			this.Load(bmp);
		}

		/// <summary>
		/// Instantiates a new Texture2D
		/// </summary>
		/// <param name="file">Texture file name</param>
		public Texture2D(string file)
			: this()
		{
			this.Load(file);
		}

		/// <summary>
		/// Loads the texture with the file
		/// </summary>
		/// <param name="file">File name of the texture file.</param>
		public void Load(string file)
		{
			if (Path.GetExtension(file) == ".dds")
			{
				throw new NotSupportedException();
			}
			else
			{
				using (var bmp = new Bitmap(file))
					this.Load(bmp);
			}
		}

		void IAsset.Load(AssetLoadContext context, Stream stream, string extensionHint)
		{
			if (extensionHint == ".dds")
			{
				if(LoadDDS(stream, this.Id, false) != this.Target)
					throw new InvalidDataException("Could not load texture: Invalid texture format.");
			}
			else
			{
				Log.WriteLine(LocalizedStrings.LoadingUncompressedBitmapFromStream);
				using (var bmp = new Bitmap(stream))
					this.Load(bmp);
			}
			this.SetFiltering(Filter.Linear);
		}

		/// <summary>
		/// Loads the texture with a bitmap.
		/// </summary>
		/// <param name="bmp">Bitmap that contains the pixel data.</param>
		public void Load(Bitmap bmp)
		{
			if (bmp == null)
				throw new ArgumentNullException("bmp");
			Rectangle area = new Rectangle(0, 0, bmp.Width, bmp.Height);
			var lockData = bmp.LockBits(area, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			this.Bind();
			GL.TexImage2D(
				this.Target,
				0,
				PixelInternalFormat.Srgb8Alpha8,
				bmp.Width, bmp.Height,
				0,
				OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
				PixelType.UnsignedByte,
				lockData.Scan0);
			bmp.UnlockBits(lockData);

			this.GenerateMipMaps();

			this.Width = bmp.Width;
			this.Height = bmp.Height;
		}

		/// <summary>
		/// Sets the texture filtering
		/// </summary>
		/// <param name="filter">Filer type</param>
		public void SetFiltering(Filter filter)
		{
			int min = (int)TextureMinFilter.Nearest;
			int mag = (int)TextureMagFilter.Nearest;
			switch (filter)
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

			this.Bind();
			GL.TexParameter(this.Target, TextureParameterName.TextureMagFilter, mag);
			GL.TexParameter(this.Target, TextureParameterName.TextureMinFilter, min);
		}

		/// <summary>
		/// Generates mip maps
		/// </summary>
		public void GenerateMipMaps()
		{
			this.Bind();
			GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
		}

		/// <summary>
		/// Gets the width of the texture
		/// </summary>
		public int Width { get; private set; }

		/// <summary>
		/// Gets the height of the texture
		/// </summary>
		public int Height { get; private set; }
	}
}
