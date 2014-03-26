using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines a 2-dimensional texture.
	/// </summary>
	[AssetExtension(".dds", ".png", ".bmp", ".jpg", ".gif")]
	public sealed class Texture2D : Texture
	{
		/// <summary>
		/// Instantiates a Texture2D
		/// </summary>
		public Texture2D()
			: base(TextureTarget.Texture2D)
		{
		}

		/// <summary>
		/// Instantiates a new Texture2D.
		/// </summary>
		/// <param name="isSRGB">Is this texture in sRGB color space?</param>
		public Texture2D(bool isSRGB)
			: this()
		{
			this.IsSRGB = isSRGB;
		}

		/// <summary>
		/// Instantiates a new Texture2D
		/// </summary>
		/// <param name="width">Width of the texture</param>
		/// <param name="height">Height of the texture</param>
		/// <param name="internalFormat">Internal Format</param>
		/// <param name="pixelFormat">Pixel Format</param>
		/// <param name="pixelType">Pixel Type</param>
		public Texture2D(int width, int height, PixelInternalFormat internalFormat, OpenTK.Graphics.OpenGL4.PixelFormat pixelFormat, PixelType pixelType)
			: this()
		{
			Game.Current.InvokeOpenGL(() =>
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
				});
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
		/// <param name="source">Stream that contains the bitmap data.</param>
		public Texture2D(Stream source)
			: this()
		{
			this.Load(new Bitmap(source));
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

		/// <summary>
		/// Loads the 2d texture.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="stream"></param>
		/// <param name="extensionHint"></param>
		protected override void Load(AssetLoadContext context, Stream stream, string extensionHint)
		{
			if (extensionHint == ".dds")
			{
				Log.WriteLine(LocalizedStrings.LoadingUncompressedBitmapFromStream);
				Game.Current.InvokeOpenGL(() =>
					{
						if (LoadDDS(stream) != this.Target)
							throw new InvalidDataException("Could not load texture: Invalid texture format.");
					});
			}
			else
			{
				Log.WriteLine(LocalizedStrings.LoadingUncompressedBitmapFromStream);
				using (var bmp = new Bitmap(stream))
					this.Load(bmp);
			}
			this.Filter = Filter.LinearMipMapped;
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
			var lockData = bmp.LockBits(area, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			Game.Current.InvokeOpenGL(() =>
				{
					this.Bind();

					GL.TexImage2D(
						this.Target,
						0,
						this.IsSRGB ? PixelInternalFormat.Srgb8Alpha8 : PixelInternalFormat.Rgba,
						bmp.Width, bmp.Height,
						0,
						OpenTK.Graphics.OpenGL4.PixelFormat.Bgra,
						PixelType.UnsignedByte,
						lockData.Scan0);
					bmp.UnlockBits(lockData);

					this.GenerateMipMaps();
				});
			this.Width = bmp.Width;
			this.Height = bmp.Height;
		}

		/// <summary>
		/// Loads the texture with RGBA8 data.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public void Load(IntPtr source, int width, int height)
		{
			Game.Current.InvokeOpenGL(() =>
			{
				this.Bind();
				GL.TexImage2D(
					this.Target,
					0,
					this.IsSRGB ? PixelInternalFormat.Srgb8Alpha8 : PixelInternalFormat.Rgba,
					width, height,
					0,
					OpenTK.Graphics.OpenGL4.PixelFormat.Bgra,
					PixelType.UnsignedByte,
					source);
				this.GenerateMipMaps();
			});

			this.Width = width;
			this.Height = height;
		}

		/// <summary>
		/// Loads the texture with RGBA8 data.
		/// </summary>
		public void Load(byte[] pixels, int width, int height)
		{
			Game.Current.InvokeOpenGL(() =>
			{
				this.Bind();
				GL.TexImage2D(
					this.Target,
					0,
					this.IsSRGB ? PixelInternalFormat.Srgb8Alpha8 : PixelInternalFormat.Rgba,
					width, height,
					0,
					OpenTK.Graphics.OpenGL4.PixelFormat.Bgra,
					PixelType.UnsignedByte,
					pixels);
				this.GenerateMipMaps();
			});

			this.Width = width;
			this.Height = height;
		}

		/// <summary>
		/// Generates mip maps
		/// </summary>
		public void GenerateMipMaps()
		{
			Game.Current.InvokeOpenGL(() =>
				{
					this.Bind();
					GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
				});
		}

		/// <summary>
		/// Sets the data of this texture.
		/// </summary>
		/// <param name="pixels">Contains all Width*Height pixels.</param>
		/// <param name="format">Format of pixels</param>
		/// <param name="type">Type of pixels</param>
		public void SetData(byte[] pixels, PixelFormat format, PixelType type)
		{
			Game.Current.InvokeOpenGL(() =>
				{
					this.Bind();
					GL.TexSubImage2D(
						this.Target,
						0,
						0,
						0,
						this.Width,
						this.Height,
						format, type,
						pixels);

					GL.GetTexImage(
						this.Target,
						0,
						format,
						type,
						pixels);
					pixels[0] = pixels[0];
				});
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
