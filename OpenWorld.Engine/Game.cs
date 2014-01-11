using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines a game.
	/// </summary>
	public abstract class Game
	{
		/// <summary>
		/// Instantiates a new game.
		/// </summary>
		protected Game()
		{
			this.Assets = new AssetManager();
		}

		internal void Load()
		{
			this.OnLoad();
		}

		internal void Update(GameTime time)
		{
			this.OnUpdate(time);
		}

		internal void Draw(GameTime time)
		{
			this.OnDraw(time);
		}

		internal void Unload()
		{
			this.OnUnload();
		}

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
			get
			{
				if (this.Window != null)
					return this.Window.InputManager;
				return null;
			}
		}

		/// <summary>
		/// Gets the games window.
		/// </summary>
		public Window Window { get; internal set; }
	}
}
