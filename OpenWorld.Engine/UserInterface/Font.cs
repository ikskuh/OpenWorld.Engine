using OpenTK;
using OpenTK.Graphics.OpenGL;
using SharpFont;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.UserInterface
{
	/// <summary>
	/// Represents a font.
	/// </summary>
	public sealed class Font : IDisposable
	{
		class FontGlyph
		{
			public uint Index { get; set; }

			public GlyphSlot Glyph { get; set; }

			public Texture2D Texture { get; set; }

			public Buffer VertexBuffer { get; set; }
		}

		private static Library library = new Library();

		private Face face = null;
		private Shader fontShader = null;
		private VertexArray vao;
		private readonly Dictionary<uint, FontGlyph> glyphs = new Dictionary<uint, FontGlyph>();

		/// <summary>
		/// Instantiates a new font.
		/// </summary>
		/// <param name="fileName">Font filename</param>
		/// <param name="height">Height of the font in pixels</param>
		public Font(string fileName, int height)
		{
			this.Name = System.IO.Path.GetFileNameWithoutExtension(fileName);
			this.Height = height;

			var fontFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
			if (!System.IO.File.Exists(fileName))
			{
				fileName = fontFolder + "/" + System.IO.Path.GetFileName(fileName);
				if (!System.IO.File.Exists(fileName))
					throw new System.IO.FileNotFoundException("Could not find font file.", fileName);
			}

			this.face = Font.library.NewFace(fileName, 0);
			this.face.SetPixelSizes(0, (uint)height);

			this.vao = new VertexArray();
			this.vao.Bind();
			GL.EnableVertexAttribArray(0);
			VertexArray.Unbind();

			this.fontShader = new Shader();
			this.fontShader.Compile(
@"#version 330
layout(location = 0) in vec4 inVertex;
uniform mat4 orthoMatrix;
uniform mat4 worldMatrix;

out vec2 uv;

void main()
{
	gl_Position = orthoMatrix * worldMatrix * vec4(inVertex.xy, 0.0f, 1.0f);
	uv = inVertex.zw;
}",
@"#version 330
layout(location = 0) out vec4 result;
in vec2 uv;
uniform vec4 color;
uniform sampler2D glyph;
void main()
{
	result = color * texture(glyph, uv);
}
");

			// Take all characters from space to tilde
			for (char c = ' '; c <= '~'; c++)
			{
				this.GetGlyph(c);
			}
		}

		private FontGlyph GetGlyph(char chr)
		{
			return this.GetGlyph(this.face.GetCharIndex(chr));
		}

		private FontGlyph GetGlyph(uint glyphIndex)
		{
			if (this.glyphs.ContainsKey(glyphIndex))
				return this.glyphs[glyphIndex];

			FontGlyph g = new FontGlyph() { Index = glyphIndex };

			this.face.LoadGlyph(glyphIndex, LoadFlags.Default, LoadTarget.Normal);
			this.face.Glyph.RenderGlyph(RenderMode.Normal);
			g.Glyph = this.face.Glyph;

			if (g.Glyph.Bitmap != null && g.Glyph.Bitmap.Width > 0 && g.Glyph.Bitmap.Rows > 0)
			{
				using (var bmp = new Bitmap(g.Glyph.Bitmap.Width, g.Glyph.Bitmap.Rows))
				{
					var data = g.Glyph.Bitmap.BufferData;
					for (int x = 0; x < bmp.Width; x++)
					{
						for(int y = 0; y < bmp.Height; y++)
						{
							bmp.SetPixel(x, y, System.Drawing.Color.FromArgb(
								data[bmp.Width * y + x],
								255, 255, 255));
						}
					}

					g.Texture = new Texture2D(bmp);
					g.Texture.Filter = Filter.Linear;
					g.Texture.WrapS = TextureWrapMode.ClampToEdge;
					g.Texture.WrapT = TextureWrapMode.ClampToEdge;
				}
				
				float w = g.Glyph.Bitmap.Width;
				float h = g.Glyph.Bitmap.Rows;
				float x2 = g.Glyph.BitmapLeft;
				float y2 = this.Height - g.Glyph.BitmapTop;

				g.VertexBuffer = new Buffer(BufferTarget.ArrayBuffer);
				g.VertexBuffer.SetData(BufferUsageHint.StaticDraw, new[]
					{
						new Vector4(x2,     y2    , 0, 0),
						new Vector4(x2 + w, y2    , 1, 0),
						new Vector4(x2,     y2 + h, 0, 1),
						new Vector4(x2 + w, y2 + h, 1, 1)
					});
			}

			this.glyphs.Add(glyphIndex, g);
			return g;
		}

		/// <summary>
		/// Measures a string size.
		/// </summary>
		/// <param name="text">The string to measure</param>
		/// <returns>Size of the area the string takes</returns>
		public Vector2 Measure(string text)
		{
			if (text == null)
				throw new ArgumentNullException("text");
			Vector2 size = new Vector2(0.0f, 0.0f);
			float penX = 0, penY = 0;
			for (int i = 0; i < text.Length; i++)
			{
				char c = text[i];
				if(c == '\n')
				{
					penX = 0;
					penY += face.Size.Metrics.Height / 64.0f;
					continue;
				}

				var g = this.GetGlyph(c);

				penX += g.Glyph.Advance.X >> 6;
				penY += g.Glyph.Advance.Y >> 6;

				if (this.face.HasKerning && i < text.Length - 1)
				{
					char cNext = text[i + 1];
					penX += this.face.GetKerning(
						g.Index,
						face.GetCharIndex(cNext),
						KerningMode.Default).X >> 6;
				}

				size.X = Math.Max(size.X, penX);
				size.Y = Math.Max(size.Y, penY + this.Height);
			}

			return size;
		}

		internal void Draw(Matrix4 orthoMatrix, string text, int x, int y, Color color)
		{
			float penX = x, penY = y;

			this.vao.Bind();

			this.fontShader.Use();
			this.fontShader.SetUniform("orthoMatrix", orthoMatrix);
			for (int i = 0; i < text.Length; i++)
			{
				char c = text[i];
				if (c == '\n')
				{
					penX = x;
					penY += face.Size.Metrics.Height / 64.0f;
					continue;
				}

				var g = this.GetGlyph(c);

				if (g.VertexBuffer != null)
				{
					// We have something to render

					g.VertexBuffer.Bind();
					GL.VertexAttribPointer(
						0,
						4,
						VertexAttribPointerType.Float,
						false,
						16,
						0);

					this.fontShader.SetUniform("color", color);
					this.fontShader.SetUniform("worldMatrix", Matrix4.CreateTranslation(penX, penY, 0.0f));
					this.fontShader.SetTexture("glyph", g.Texture, 0);

                    GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
				}


				penX += g.Glyph.Advance.X >> 6;
				penY += g.Glyph.Advance.Y >> 6;

				if (this.face.HasKerning && i < text.Length - 1)
				{
					char cNext = text[i + 1];
					penX += this.face.GetKerning(
						g.Index,
						face.GetCharIndex(cNext),
						KerningMode.Default).X >> 6;
				}
			}

			VertexArray.Unbind();
		}

		/// <summary>
		/// Disposes the font.
		/// </summary>
		public void Dispose()
		{
			if (this.face != null)
				this.face.Dispose();
			if (this.fontShader != null)
				this.fontShader.Dispose();
			if (this.vao != null)
				this.vao.Dispose();

			// Dispose every self-made glyph texture and vertex buffers...
			foreach(var g in this.glyphs)
			{
				if (g.Value.Texture != null)
					g.Value.Texture.Dispose();
				if (g.Value.VertexBuffer != null)
					g.Value.VertexBuffer.Dispose();
				g.Value.Texture = null;
				g.Value.VertexBuffer = null;
			}
			// Now remove all the glyphs
			this.glyphs.Clear();

			this.face = null;
			this.fontShader = null;
			this.vao = null;
		}

		/// <summary>
		/// Gets the name of the font
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Gets the height of the font
		/// </summary>
		public int Height { get; private set; }
	}
}
