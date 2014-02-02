using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines a cubemap texture
	/// </summary>
	public sealed class TextureCube : Texture
	{
		/// <summary>
		/// Instantiates a cubemap texture
		/// </summary>
		public TextureCube()
			: base(TextureTarget.TextureCubeMap)
		{

		}

		// +X, +Y, +Z, -X, -Y und -Z

		protected override void Load(AssetLoadContext manager, System.IO.Stream stream, string extensionHint)
		{
			using(var bmp = new Bitmap(stream))
			{
				this.Bind();
				LoadSide(bmp, 0, TextureTarget.TextureCubeMapPositiveX);
				LoadSide(bmp, 1, TextureTarget.TextureCubeMapPositiveY);
				LoadSide(bmp, 2, TextureTarget.TextureCubeMapPositiveZ);

				LoadSide(bmp, 3, TextureTarget.TextureCubeMapNegativeX);
				LoadSide(bmp, 4, TextureTarget.TextureCubeMapNegativeY);
				LoadSide(bmp, 5, TextureTarget.TextureCubeMapNegativeZ);
			}
			Game.Current.InvokeOpenGL(() =>
				{
					this.Bind();
					GL.TexParameter(this.Target, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
					GL.TexParameter(this.Target, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
				});
		}

		private void LoadSide(Bitmap bmp, int offset, TextureTarget target)
		{
			Rectangle area = new Rectangle(0, offset * bmp.Width, bmp.Width, bmp.Width);
			/*
			using(var tmp = new Bitmap(bmp.Width, bmp.Width))
			{
				using(var g = Graphics.FromImage(tmp))
				{
					g.DrawImageUnscaled(bmp, new Point(0, offset * bmp.Width));
				}
				tmp.RotateFlip(RotateFlipType.RotateNoneFlipXY);
				using (var g = Graphics.FromImage(bmp))
				{
					g.DrawImageUnscaled(bmp, area);
				}
			}
			*/
			var lockData = bmp.LockBits(area, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			Game.Current.InvokeOpenGL(() =>
				{
					this.Bind();
					GL.TexImage2D(
						target,
						0,
						PixelInternalFormat.Rgba,
						bmp.Width, bmp.Width,
						0,
						OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
						PixelType.UnsignedByte,
						lockData.Scan0);
				});
			bmp.UnlockBits(lockData);
		}
	}
}
