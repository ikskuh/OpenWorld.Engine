using OpenTK;
using OpenTK.Graphics;
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
	/// Defines a window that hosts a game.
	/// </summary>
	public sealed class Window : GameWindow
	{
		private Game game;
		private VertexArray defaultVertexArray;
		private GameTime gameTime;
		private InputManager inputManager;

		/// <summary>
		/// Instantiates a new window.
		/// </summary>
		/// <param name="width">Width of the render area</param>
		/// <param name="height">Height of the render area</param>
		public Window(int width, int height)
			: base(
			width, height,
			GraphicsMode.Default,
			"OpenWorld",
			GameWindowFlags.Default,
			DisplayDevice.Default,
			3, 3,
			GraphicsContextFlags.ForwardCompatible
#if DEBUG
 | GraphicsContextFlags.Debug
#endif
)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoad(EventArgs e)
		{
			this.defaultVertexArray = new VertexArray();
			this.defaultVertexArray.Bind();

			this.inputManager = new InputManager(
				this.Keyboard,
				this.Mouse,
				this.Joysticks.ToArray());

			if (this.Game != null)
				this.Game.Load();

			this.gameTime = new GameTime();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			if (e == null)
				throw new ArgumentNullException("e");
			this.gameTime.DeltaTime = (float)e.Time;
			this.gameTime.TotalTime += this.gameTime.DeltaTime;
			if (this.Game != null)
			{
				this.Game.IsActive = this.Focused;
				this.Game.Update(this.gameTime);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnRenderFrame(FrameEventArgs e)
		{
			if (this.Game != null)
				this.Game.Draw(this.gameTime);
			this.SwapBuffers();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnUnload(EventArgs e)
		{
			if (this.Game != null)
				this.Game.Unload();
			this.defaultVertexArray.Dispose();
			this.defaultVertexArray = null;
		}

		/// <summary>
		/// Gets or sets the game that will be hosted by this window.
		/// </summary>
		public Game Game
		{
			get
			{
				return this.game;
			}
			set
			{
				this.game = value;
				if (this.game != null)
					this.game.Window = this;
			}
		}

		/// <summary>
		/// Gets the viewport width
		/// </summary>
		public int ViewportWidth { get { return this.ClientSize.Width; } }

		/// <summary>
		/// Gets the viewport height
		/// </summary>
		public int ViewportHeight { get { return this.ClientSize.Height; } }

		/// <summary>
		/// Gets the viewport aspect
		/// </summary>
		public float Aspect { get { return (float)this.ClientSize.Width / (float)this.ClientSize.Height; } }

		/// <summary>
		/// Gets the input manager
		/// </summary>
		public InputManager InputManager
		{
			get { return inputManager; }
		}
	}
}
