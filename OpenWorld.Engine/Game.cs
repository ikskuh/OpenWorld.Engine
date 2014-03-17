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
using System.Diagnostics;

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
		private Thread soundThread;
		private Thread updateThread;
		private Thread[] deferralThreads;

		private volatile bool isRendering = false;
		private bool isRunning = false;

		private AudioContext audioContext;

		private readonly ConcurrentQueue<DeferredRoutineHandler> deferredRoutines = new ConcurrentQueue<DeferredRoutineHandler>();
		private readonly ConcurrentQueue<DeferredRoutineHandler> deferredGLRoutines = new ConcurrentQueue<DeferredRoutineHandler>();
		private readonly ConcurrentQueue<DeferredRoutineHandler> deferredALRoutines = new ConcurrentQueue<DeferredRoutineHandler>();

		private readonly CoRoutineHost coRoutineHost = new CoRoutineHost();

		private GameState currentState;
		private GameState nextState;

		internal event EventHandler<UpdateEventArgs> UpdateNonScene;

		/// <summary>
		/// Instantiates a new game.
		/// </summary>
		protected Game()
		{
			this.deferralThreads = new Thread[2];
		}

		/// <summary>
		/// Starts the game main loop.
		/// </summary>
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

				this.Utilities = new GameUtilities(this);

				this.Assets = new AssetManager();
				this.Size = presentation.Resolution;

				this.isRunning = true;

				// Start the deferral threads for deferred routines
				for (int i = 0; i < this.deferralThreads.Length; i++)
				{
					this.deferralThreads[i] = new Thread(this.DeferRoutines);
					this.deferralThreads[i].Name = "OpenWorld Deferred Routines Host";
					this.deferralThreads[i].Start();
				}

				// Start the sound thread.
				this.soundThread = new Thread(this.SoundLoop);
				this.soundThread.Name = "OpenWorld Sound Thread";
				this.soundThread.Start();

				// Load everything in the draw thread
				this.OnLoad();

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

						if (this.currentState != null)
							this.currentState.Draw(this.Time);

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

				this.input = null;
				this.Size = new System.Drawing.Size();

				this.Assets.CleanUp();
				this.Assets = null;
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
				Thread.Sleep(0);

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

		/// <summary>
		/// Defers a routine execution into the current thread.
		/// </summary>
		private void SoundLoop()
		{
			Game.currentGame.Value = this;

			this.audioContext = this.CreateAudioContext();
			this.audioContext.MakeCurrent();

			while (this.isRunning)
			{
				Thread.Sleep(1);

				DeferredRoutineHandler handler;
				if (!this.deferredALRoutines.TryDequeue(out handler))
					continue;
				if (handler == null)
					continue;
				handler.Routine();
				var error = OpenTK.Audio.OpenAL.AL.GetError();
				if(error != OpenTK.Audio.OpenAL.ALError.NoError)
				{
					System.Diagnostics.Debug.WriteLine("AL Error: " + error);
					System.Diagnostics.Debugger.Break();
				}
				handler.WaitHandle.Set();
			}

			this.audioContext.Dispose();
			this.audioContext = null; 

			Game.currentGame.Value = null;
		}

		private void UpdateLoop()
		{
			Game.currentGame.Value = this;

			Stopwatch watch = new Stopwatch();
			watch.Start();
			long lastTicks = 0;
			while (this.isRunning)
			{
				long current = watch.ElapsedTicks;
				this.Time = new GameTime(
					(float)current / (float)Stopwatch.Frequency,
					(float)(current - lastTicks) / (float)Stopwatch.Frequency);

				// Update the game
				this.OnUpdate(this.Time);

				// Step all co-routines
				this.coRoutineHost.Step();

				if(this.nextState != this.currentState)
				{
					if (this.currentState != null)
					{
						this.currentState.Leave();
						this.currentState.Game = null;
					}
					this.currentState = this.nextState;
					if (this.currentState != null)
					{
						this.currentState.Game = this;
						this.currentState.Enter();
					}
				}

				if (this.currentState != null)
					this.currentState.Update(this.Time);

				this.isRendering = true;

				// Update non-scene stuff here (physics, ...)
				if (this.UpdateNonScene != null)
					this.UpdateNonScene(this, new UpdateEventArgs(this.Time));

				// Wait for the rendering to be finished.
				while (this.isRendering) Thread.Sleep(1);

				lastTicks = current;
			}

			if (this.currentState != null)
				this.currentState.Leave();

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
			if (routine == null)
				return;
			if (Thread.CurrentThread == this.drawThread)
				routine();
			else
				this.DeferRoutine(true, routine).WaitOne();
		}


		/// <summary>
		/// Deferres a routine into the OpenAL thread and waits for the execution.
		/// </summary>
		/// <param name="routine">The routine to be deferred.</param>
		public void InvokeOpenAL(DeferredRoutine routine)
		{
			if (routine == null)
				return;
			if (Thread.CurrentThread == this.soundThread)
				routine();
			else
			{
				var handler = new DeferredRoutineHandler()
				{
					Routine = routine,
					WaitHandle = new ManualResetEvent(false)
				};
				this.deferredALRoutines.Enqueue(handler);
				handler.WaitHandle.WaitOne();
			}
		}

		/// <summary>
		/// Starts a co-routine.
		/// </summary>
		/// <param name="routine">The co-routine to start.</param>
		public void StartCoRoutine(CoRoutine routine)
		{
			this.coRoutineHost.Start(routine);
		}

		/// <summary>
		/// Sets the next game state.
		/// </summary>
		/// <param name="state">The state that is used from the next frame on.</param>
		public void SetState(GameState state)
		{
			this.nextState = state;
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

		/// <summary>
		/// Gets the current game state.
		/// </summary>
		public GameState State
		{
			get { return currentState; }
		}

		/// <summary>
		/// Gets the width of the screen.
		/// </summary>
		public int Width { get { return this.Size.Width; } }

		/// <summary>
		/// Gets the height of the screen.
		/// </summary>
		public int Height { get { return this.Size.Height; } }

		/// <summary>
		/// Gets a set of utilities.
		/// </summary>
		public GameUtilities Utilities { get; private set; }

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
