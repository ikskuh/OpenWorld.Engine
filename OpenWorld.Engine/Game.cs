using OpenTK;
using OpenTK.Audio;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines a game.
	/// </summary>
	public abstract class Game
	{
		private class DeferredRoutineHandler
		{
			public ManualResetEvent WaitHandle { get; set; }

			public DeferredRoutine Routine { get; set; }
		}

		private InputManager input;
		private Thread drawThread;
		private Thread updateThread;
		private Thread[] deferralThreads;

		private volatile bool isRendering = false;
		private bool isRunning = false;

		private AudioContext audioContext;

		private readonly ConcurrentQueue<DeferredRoutineHandler> deferredRoutines = new ConcurrentQueue<DeferredRoutineHandler>();
		private readonly ConcurrentQueue<DeferredRoutineHandler> deferredGLRoutines = new ConcurrentQueue<DeferredRoutineHandler>();

		/// <summary>
		/// Instantiates a new game.
		/// </summary>
		protected Game()
		{
			this.deferralThreads = new Thread[2];
			this.Assets = new AssetManager();
		}

		public void Run()
		{
			this.drawThread = Thread.CurrentThread;

			Game.currentGame.Value = this;

			var presentation = this.GetPresentationParameters();

			using (GameWindow window = new GameWindow(
				presentation.Resolution.Width,
				presentation.Resolution.Height,
				GraphicsMode.Default,
				presentation.Title,
				presentation.IsFullscreen ? GameWindowFlags.Fullscreen : GameWindowFlags.Default,
				presentation.DisplayDevice ?? DisplayDevice.Default,
				3, 3,
#if DEBUG
 GraphicsContextFlags.Default | GraphicsContextFlags.ForwardCompatible | GraphicsContextFlags.Debug))
#else
				GraphicsContextFlags.Default | GraphicsContextFlags.ForwardCompatible))	
#endif
			{
				window.VSync = presentation.VSync ? VSyncMode.On : VSyncMode.Off;
				window.Closing += (s, e) => { this.isRunning = false; };
				window.Visible = true;

				this.input = new InputManager(
					window.Keyboard,
					window.Mouse,
					window.Joysticks.ToArray());
				this.Size = presentation.Resolution;

				this.isRunning = true;

				this.audioContext = this.CreateAudioContext();

				// Load everything in the draw thread
				this.OnLoad();

				// Start the deferral threads for deferred routines
				for (int i = 0; i < this.deferralThreads.Length; i++)
				{
					this.deferralThreads[i] = new Thread(this.DeferRoutines);
					this.deferralThreads[i].Name = "OpenWorld Deferred Routines Host";
					this.deferralThreads[i].Start();
				}

				// Start the update thread.
				this.updateThread = new Thread(this.UpdateLoop);
				this.updateThread.Name = "OpenWorld Game Update Thread";
				this.updateThread.Start();

				Thread.CurrentThread.Name = "OpenWorld Game Render Thread";

				while (this.isRunning)
				{
					while (!this.isRendering)
					{
						DeferredRoutineHandler handler = null;
						if (this.deferredGLRoutines.TryDequeue(out handler))
						{
							handler.Routine();
							handler.WaitHandle.Set();
						}
						Thread.Sleep(0);
					}

					// Process the window, draw everything, then show it.
					window.ProcessEvents();
					if (!window.IsExiting)
					{
						this.OnDraw(this.Time);
						window.SwapBuffers();
					}
					this.isRendering = false;
				}

				// Unload everything in the draw thread.
				this.OnUnload();

				for (int i = 0; i < this.deferralThreads.Length; i++)
				{
					this.deferralThreads[i].Join();
					this.deferralThreads[i] = null;
				}

				this.updateThread.Join();

				if (this.audioContext != null)
					this.audioContext.Dispose();

				this.input = null;
				this.Size = new System.Drawing.Size();
			}

			Game.currentGame.Value = null;
			this.drawThread = null;
		}

		/// <summary>
		/// Defers a routine execution into the current thread.
		/// </summary>
		private void DeferRoutines()
		{
			Game.currentGame.Value = this;
			while (this.isRunning)
			{
				DeferredRoutineHandler handler;
				if (!this.deferredRoutines.TryDequeue(out handler))
					continue;
				if (handler == null)
					continue;
				handler.Routine();
				handler.WaitHandle.Set();
			}
			Game.currentGame.Value = null;
		}

		private void UpdateLoop()
		{
			Game.currentGame.Value = this;

			if (this.audioContext != null)
				this.audioContext.MakeCurrent();

			DateTime start = DateTime.Now;
			GameTime timeLast = new GameTime(0, 0);
			while (this.isRunning)
			{
				float total = (float)(DateTime.Now - start).TotalSeconds;
				this.Time = new GameTime(total, total - timeLast.TotalTime);

				// Update the game
				this.OnUpdate(this.Time);

				this.isRendering = true;

				// TODO: Update physics here

				// Wait for the rendering to be finished.
				while (this.isRendering) Thread.Sleep(0);

				timeLast = this.Time;
			}

			Game.currentGame.Value = null;
		}

		/// <summary>
		/// Creates the games audio context.
		/// </summary>
		/// <returns>New AudioContext or null if no audio should be used.</returns>
		protected virtual AudioContext CreateAudioContext()
		{
			return new AudioContext();
		}

		/// <summary>
		/// Returns the presentation parameters for this game.
		/// </summary>
		/// <returns>Presentation parameters used for setting up the window.</returns>
		protected virtual PresentationParameters GetPresentationParameters()
		{
			return new PresentationParameters()
			{
				Resolution = new System.Drawing.Size(1024, 768),
				IsFullscreen = false,
				Title = this.GetType().Name,
				DisplayDevice = DisplayDevice.Default,
				VSync = false
			};
		}

		/// <summary>
		/// Stops the game.
		/// </summary>
		public void Exit()
		{
			this.isRunning = false;
		}

		/// <summary>
		/// Deferres a routine into another thread so the current thread can continue.
		/// </summary>
		/// <remarks>Useful for loading assets or resources.</remarks>
		/// <param name="routine">The routine to be deferred.</param>
		public WaitHandle DeferRoutine(DeferredRoutine routine)
		{
			return this.DeferRoutine(false, routine);
		}

		/// <summary>
		/// Deferres a routine into another thread so the current thread can continue.
		/// </summary>
		/// <remarks>Useful for loading assets or resources.</remarks>
		/// <param name="openGL">Defines if the routine is deferred into the OpenGL thread or not.</param>
		/// <param name="routine">The routine to be deferred.</param>
		public WaitHandle DeferRoutine(bool openGL, DeferredRoutine routine)
		{
			if (routine == null)
				return null;
			var handler = new DeferredRoutineHandler()
			{
				Routine = routine,
				WaitHandle = new ManualResetEvent(false)
			};
			if(openGL)
				this.deferredGLRoutines.Enqueue(handler);
			else
				this.deferredRoutines.Enqueue(handler);
			return handler.WaitHandle;
		}

		/// <summary>
		/// Deferres a routine into the OpenGL thread and waits for the execution.
		/// </summary>
		/// <remarks>Useful for mapping OpenGL resources from another thread.</remarks>
		/// <param name="routine">The routine to be deferred.</param>
		public void InvokeOpenGL(DeferredRoutine routine)
		{
			if (Thread.CurrentThread == this.drawThread)
				routine();
			else
				this.DeferRoutine(true, routine).WaitOne();
		}

		#region Pure Virtual Methods

		/// <summary>
		/// Gets called if the game should load its resources.
		/// </summary>
		protected virtual void OnLoad() { }

		/// <summary>
		/// Gets called if the game should update its logic.
		/// </summary>
		protected virtual void OnUpdate(GameTime time) { }

		/// <summary>
		/// Gets called if the game should draw itself.
		/// </summary>
		protected virtual void OnDraw(GameTime time) { }

		/// <summary>
		/// Gets called if the game should unload its resources.
		/// </summary>
		protected virtual void OnUnload() { }

		#endregion

		/// <summary>
		/// Gets or sets the asset manager.
		/// </summary>
		public AssetManager Assets { get; set; }

		/// <summary>
		/// Gets a value that determines wheather the game window is activated or not.
		/// </summary>
		public bool IsActive { get; internal set; }

		/// <summary>
		/// Gets the input manager.
		/// </summary>
		public InputManager Input
		{
			get { return this.input; }
		}

		/// <summary>
		/// Gets the current time snapshot.
		/// </summary>
		public GameTime Time { get; private set; }

		/// <summary>
		/// Gets the screen size.
		/// </summary>
		public System.Drawing.Size Size { get; private set; }

		/// <summary>
		/// Gets the aspect of the screen.
		/// </summary>
		public float Aspect { get { return (float)this.Size.Width / (float)this.Size.Height; } }

		#region Static Part

		private static readonly ThreadLocal<Game> currentGame = new ThreadLocal<Game>(() => null);

		/// <summary>
		/// Gets the current game for the current thread.
		/// </summary>
		public static Game Current
		{
			get { return Game.currentGame.Value; }
		}

		#endregion
	}
}
