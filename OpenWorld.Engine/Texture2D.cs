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
	[AssetExtension(".dds", ".png", ".bmp")]
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
				if (LoadDDS(stream, this.Id, Texture2D.UseSRGB) != this.Target)
					throw new InvalidDataException("Could not load texture: Invalid texture format.");
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
			var lockData = bmp.LockBits(area, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			this.Bind();
			GL.TexImage2D(
				this.Target,
				0,
				Texture2D.UseSRGB ? PixelInternalFormat.Srgb8Alpha8 : PixelInternalFormat.Rgba,
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

		/// <summary>
		/// Gets or sets a value that determines wheather sRGB is used or not.
		/// </summary>
		public static bool UseSRGB { get; set; }
	}
}
