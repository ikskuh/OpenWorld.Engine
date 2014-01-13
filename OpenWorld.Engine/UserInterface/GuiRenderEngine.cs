using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine.UserInterface
{
	/// <summary>
	/// Provides methods to draw a 
	/// </summary>
	public sealed class GuiRenderEngine : IDisposable
	{
		[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
		struct UIVertex
		{
			public Vector2 Position;
			public Color Color;
			public Vector2 UV;

			public static int Size { get { return System.Runtime.InteropServices.Marshal.SizeOf(typeof(UIVertex)); } }
		}

		private readonly Dictionary<Type, GuiRenderer> renderers = new Dictionary<Type, GuiRenderer>();

		private readonly Gui gui;
		private GUIShader shader;
		private VertexArray vertexArray;
		private Buffer vertexBuffer;
		private Texture2D blankWhite;

		internal GuiRenderEngine(Gui gui)
		{
			this.gui = gui;

			this.shader = new GUIShader();
			this.shader.Transform =
				Matrix4.CreateScale(1.0f,-1.0f,1.0f) *
				Matrix4.CreateOrthographicOffCenter(
				0.0f, this.gui.ScreenSize.X,
					-this.gui.ScreenSize.Y, 0.0f,
					0.0f, 1.0f);

			this.vertexArray = new VertexArray();
			this.vertexArray.Bind();

			this.vertexBuffer = new Buffer(BufferTarget.ArrayBuffer);
			this.vertexBuffer.Bind();

			GL.EnableVertexAttribArray(0);
			GL.EnableVertexAttribArray(1);
			GL.EnableVertexAttribArray(2);

			GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, UIVertex.Size, 0);  // Position
			GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, UIVertex.Size, 8);  // Color
			GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, UIVertex.Size, 24); // UV

			VertexArray.Unbind();

			using (var bmp = new System.Drawing.Bitmap(2, 2))
			{
				bmp.SetPixel(0, 0, System.Drawing.Color.White);
				bmp.SetPixel(1, 0, System.Drawing.Color.White);
				bmp.SetPixel(0, 1, System.Drawing.Color.White);
				bmp.SetPixel(1, 1, System.Drawing.Color.White);
				this.blankWhite = new Texture2D(bmp);
			}
		}

		internal GuiRenderer GetRenderer(Type type)
		{
			GuiRenderer renderer;
			if(!this.renderers.ContainsKey(type))
			{
				Type iterationType = type;
				do
				{
					RendererAttribute[] attribs = (RendererAttribute[])iterationType.GetCustomAttributes(typeof(RendererAttribute), false);
					if (attribs.Length > 0)
					{
						var constructor = attribs[0].RendererType.GetConstructor(new Type[0]);
						if (constructor == null)
							throw new InvalidOperationException(attribs[0].RendererType.Name + " has no parameterless constructor.");
						renderer = (GuiRenderer)constructor.Invoke(new object[0]);
						renderer.Engine = this;
						this.renderers.Add(type, renderer);
						return renderer;
					}
					else
					{
						iterationType = iterationType.BaseType;
						if (iterationType == typeof(Control))
							iterationType = null;
					}
				} while (iterationType != null);
				throw new InvalidOperationException(type.Name + " has no renderer.");
			}
			renderer = this.renderers[type];
			renderer.Engine = this;
			return renderer;
		}

		/// <summary>
		/// Sets a renderer
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="renderer"></param>
		public void SetRenderer<T>(GuiRenderer renderer)
			where T : Control
		{
			if (this.renderers.ContainsKey(typeof(T)))
				this.renderers[typeof(T)] = renderer;
			else
				this.renderers.Add(typeof(T), renderer);
		}

		internal void SetGLStates()
		{
			GL.Disable(EnableCap.AlphaTest);
			GL.Disable(EnableCap.DepthTest);
			GL.Disable(EnableCap.Lighting);
			GL.Disable(EnableCap.CullFace);

			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
		}

		internal void SetArea(Box2 bounds, float offsetX, float offsetY)
		{
			this.shader.Transform =
				Matrix4.CreateScale(1.0f,-1.0f,1.0f) * 
				Matrix4.CreateOrthographicOffCenter(
					0 + offsetX, bounds.Width + offsetX,
					-bounds.Height - offsetY + 1, -offsetY + 1,
					0.0f, 1.0f);
			GL.Viewport(
				(int)bounds.Left,
				(int)this.gui.ScreenSize.Y - (int)bounds.Bottom + 1,
				(int)bounds.Width, 
				(int)bounds.Height);
		}

		#region DrawLine

		/// <summary>
		/// Draws a line
		/// </summary>
		/// <param name="startX"></param>
		/// <param name="startY"></param>
		/// <param name="endX"></param>
		/// <param name="endY"></param>
		/// <param name="color"></param>
		public void DrawLine(float startX, float startY, float endX, float endY, Color color)
		{
			this.DrawLine(new Vector2(startX, startY), new Vector2(endX, endY), color, 1.0f);
		}

		/// <summary>
		/// Draws a line
		/// </summary>
		/// <param name="startX"></param>
		/// <param name="startY"></param>
		/// <param name="endX"></param>
		/// <param name="endY"></param>
		/// <param name="color"></param>
		/// <param name="thickness"></param>
		public void DrawLine(float startX, float startY, float endX, float endY, Color color, float thickness)
		{
			this.DrawLine(new Vector2(startX, startY), new Vector2(endX, endY), color, thickness);
		}

		/// <summary>
		/// Draws a line
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="color"></param>
		public void DrawLine(Vector2 start, Vector2 end, Color color)
		{
			this.DrawLine(start, end, color, 1.0f);
		}

		/// <summary>
		/// Draws a line
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="color"></param>
		/// <param name="thickness"></param>
		public void DrawLine(Vector2 start, Vector2 end, Color color, float thickness)
		{
			this.DrawLine(start, end, color, color, thickness);
		}

		/// <summary>
		/// Draws a line
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="startColor"></param>
		/// <param name="endColor"></param>
		/// <param name="thickness"></param>
		public void DrawLine(Vector2 start, Vector2 end, Color startColor, Color endColor, float thickness)
		{
			// TODO: Fix line width
			GL.LineWidth(thickness);
			this.Draw(PrimitiveType.Lines, new[]
				{
					new UIVertex() { Position = start, Color = startColor },
					new UIVertex() { Position = end, Color = endColor },
				});
		}

		#endregion

		#region DrawRectangle

		/// <summary>
		/// Draws a rectangle
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="color"></param>
		public void DrawRectangle(Box2 rect, Color color)
		{
			this.DrawRectangle(rect, color, 1.0f);
		}

		/// <summary>
		/// Draws a rectangle
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="color"></param>
		/// <param name="thickness"></param>
		public void DrawRectangle(Box2 rect, Color color, float thickness)
		{
			this.DrawRectangle(rect.Left, rect.Top, rect.Width, rect.Height, color, thickness);
		}

		/// <summary>
		/// Draws a rectangle
		/// </summary>
		/// <param name="left"></param>
		/// <param name="top"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="color"></param>
		public void DrawRectangle(float left, float top, float width, float height, Color color)
		{
			this.DrawRectangle(left, top, width, height, color, 1.0f);
		}

		/// <summary>
		/// Draws a rectangle
		/// </summary>
		/// <param name="left"></param>
		/// <param name="top"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="color"></param>
		/// <param name="thickness"></param>
		public void DrawRectangle(float left, float top, float width, float height, Color color, float thickness)
		{
			this.DrawRectangle(new Vector2(left, top), new Vector2(width, height), color, thickness);
		}

		/// <summary>
		/// Draws a rectangle
		/// </summary>
		/// <param name="position"></param>
		/// <param name="size"></param>
		/// <param name="color"></param>
		public void DrawRectangle(Vector2 position, Vector2 size, Color color)
		{
			this.DrawRectangle(position, size, color, 1.0f);
		}

		/// <summary>
		/// Draws a rectangle
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="size"></param>
		/// <param name="color"></param>
		/// <param name="thickness"></param>
		public void DrawRectangle(Vector2 pos, Vector2 size, Color color, float thickness)
		{
			// TODO: Fix line width
			GL.LineWidth(thickness);
			this.Draw(PrimitiveType.LineStrip, new[]
				{
					new UIVertex() { Position = pos, Color = color },
					new UIVertex() { Position = pos + new Vector2(size.X - 1, 0), Color = color },
					new UIVertex() { Position = pos + new Vector2(size.X - 1, size.Y - 1), Color = color },
					new UIVertex() { Position = pos + new Vector2(0, size.Y - 1), Color = color },
					new UIVertex() { Position = pos, Color = color }
				});
		}

		#endregion

		#region FillRectangle

		/// <summary>
		/// Draws a filled rectangle
		/// </summary>
		/// <param name="left"></param>
		/// <param name="top"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="color"></param>
		public void FillRectangle(float left, float top, float width, float height, Color color)
		{
			this.FillRectangle(left, top, width, height, color, 1.0f);
		}

		/// <summary>
		/// Draws a filled rectangle
		/// </summary>
		/// <param name="left"></param>
		/// <param name="top"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="color"></param>
		/// <param name="thickness"></param>
		public void FillRectangle(float left, float top, float width, float height, Color color, float thickness)
		{
			this.FillRectangle(new Vector2(left, top), new Vector2(width, height), color, thickness);
		}

		/// <summary>
		/// Draws a filled rectangle
		/// </summary>
		/// <param name="position"></param>
		/// <param name="size"></param>
		/// <param name="color"></param>
		public void FillRectangle(Vector2 position, Vector2 size, Color color)
		{
			this.FillRectangle(position, size, color, 1.0f);
		}

		/// <summary>
		/// Draws a filled rectangle
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="size"></param>
		/// <param name="color"></param>
		/// <param name="thickness"></param>
		public void FillRectangle(Vector2 pos, Vector2 size, Color color, float thickness)
		{
			this.FillRectangle(new Box2(pos, pos + size), color, thickness);
		}

		/// <summary>
		/// Draws a filled rectangle
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="color"></param>
		public void FillRectangle(Box2 rect, Color color)
		{
			this.FillRectangle(rect, color, 1.0f);
		}

		/// <summary>
		/// Draws a filled rectangle
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="color"></param>
		/// <param name="thickness"></param>
		public void FillRectangle(Box2 rect, Color color, float thickness)
		{
			this.Draw(PrimitiveType.TriangleStrip, new[]
				{
					new UIVertex() { Position = new Vector2(rect.Left, rect.Top), Color = color },
					new UIVertex() { Position = new Vector2(rect.Right - 1, rect.Top), Color = color },
					new UIVertex() { Position = new Vector2(rect.Left, rect.Bottom - 1), Color = color },
					new UIVertex() { Position =new Vector2(rect.Right - 1, rect.Bottom - 1), Color = color },
				});
		}

		#endregion

		/// <summary>
		/// Draws a string
		/// </summary>
		/// <param name="text"></param>
		/// <param name="font"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="color"></param>
		public void DrawString(string text, Font font, float x, float y, Color color)
		{
			this.DrawString(text, font, x, y, color, TextAlign.TopLeft);
		}

		/// <summary>
		/// Draws a string
		/// </summary>
		/// <param name="text"></param>
		/// <param name="font"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="color"></param>
		/// <param name="align"></param>
		public void DrawString(string text, Font font, float x, float y, Color color, TextAlign align)
		{
			if(align != TextAlign.TopLeft)
			{
				var size = font.Measure(text);
				// Adjust X
				switch(align)
				{
					case TextAlign.TopLeft:
					case TextAlign.MiddleLeft:
					case TextAlign.BottomLeft:
						// No adjustment needed
						break;
					case TextAlign.TopCenter:
					case TextAlign.MiddleCenter:
					case TextAlign.BottomCenter:
						x -= 0.5f * size.X;
						break;
					case TextAlign.TopRight:
					case TextAlign.MiddleRight:
					case TextAlign.BottomRight:
						x -= size.X;
						break;
				}

				// Adjust Y
				switch (align)
				{
					case TextAlign.TopLeft:
					case TextAlign.TopCenter:
					case TextAlign.TopRight:
						// No adjustment needed
						break;
					case TextAlign.MiddleLeft:
					case TextAlign.MiddleCenter:
					case TextAlign.MiddleRight:
						y -= 0.5f * size.Y;
						break;
					case TextAlign.BottomLeft:
					case TextAlign.BottomCenter:
					case TextAlign.BottomRight:
						y -= size.Y;
						break;
				}
			}
			font.Draw(this.shader.Transform, text, (int)x, (int)y, color);
		}

		private void Draw(PrimitiveType type, UIVertex[] vertices)
		{
			this.Draw(type, vertices, this.blankWhite);
		}

		private void Draw(PrimitiveType type, UIVertex[] vertices, Texture2D texture)
		{
			this.vertexBuffer.SetData(BufferUsageHint.StaticDraw, vertices);

			this.shader.Texture = texture;
			this.shader.Use();
			this.vertexArray.Bind();

			GL.DrawArrays(type, 0, vertices.Length);
            

			VertexArray.Unbind();
		}

		/// <summary>
		/// Disposes the render engine
		/// </summary>
		public void Dispose()
		{
			if (this.shader != null)
				this.shader.Dispose();
			if (this.vertexBuffer != null)
				this.vertexBuffer.Dispose();
			if (this.vertexArray != null)
				this.vertexArray.Dispose();
			if (this.blankWhite != null)
				this.blankWhite.Dispose();

			this.shader = null;
			this.vertexBuffer = null;
			this.vertexArray = null;
			this.blankWhite = null;
		}
	}
}
