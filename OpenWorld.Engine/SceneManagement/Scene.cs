using Jitter;
using Jitter.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Defines a 3D scene.
	/// </summary>
	public sealed class Scene
	{
		readonly SceneNode root = null;

		private CollisionSystem collisionSystem;
		private World world;
		private SceneRenderer defaultRenderer; 


		/// <summary>
		/// Instantiates a new scene.
		/// </summary>
		public Scene()
			: this(SceneCreationFlags.None)
		{

		}

		/// <summary>
		/// Instantiates a new scene.
		/// </summary>
		/// <param name="flags">Specifies the behaviour of the world.</param>
		public Scene(SceneCreationFlags flags)
		{
			this.root = SceneNode.CreateRoot(this);
			this.defaultRenderer = new SimpleRenderer();

			if (flags.HasFlag(SceneCreationFlags.EnablePhysics))
			{
				this.collisionSystem = new CollisionSystemPersistentSAP();
				this.world = new World(this.collisionSystem);
				this.world.Gravity = new Jitter.LinearMath.JVector(0.0f, -9.81f, 0.0f);
			}
		}

		/// <summary>
		/// Updates the whole scene.
		/// </summary>
		/// <param name="time">Time snapshot</param>
		public void Update(GameTime time)
		{
			if (this.PhysicsEnabled)
			{
				this.world.Step(time.DeltaTime, false);
			}
			// Just update the root node.
			this.root.DoUpdate(time);
		}

		/// <summary>
		/// Draws the whole scene.
		/// </summary>
		/// <param name="camera">The camera setting for the scene.</param>
		/// <param name="time">Time snapshot</param>
		public void Draw(Camera camera, GameTime time)
		{
			this.Draw(camera, this.defaultRenderer, time);	
		}

		/// <summary>
		/// Draws the whole scene.
		/// </summary>
		/// <param name="camera">The camera setting for the scene.</param>
		/// <param name="renderer">The renderer that draws the the scene.</param>
		/// <param name="time">Time snapshot</param>
		public void Draw(Camera camera, SceneRenderer renderer, GameTime time)
		{
			if (camera == null)
				throw new ArgumentNullException("camera");
			if (renderer == null)
				throw new ArgumentNullException("renderer");

			renderer.Begin();
			this.root.DoDraw(time, renderer);
			renderer.End(this, camera);
		}

		/// <summary>
		/// Gets the root of the scene.
		/// </summary>
		public SceneNode Root
		{
			get { return root; }
		}

		/// <summary>
		/// Gets the collision system if physics are enabled or returns null if not.
		/// </summary>
		public CollisionSystem CollisionSystem
		{
			get { return collisionSystem; }
		}

		/// <summary>
		/// Gets the Jitter world if physics are enabled or returns null if not.
		/// </summary>
		public World World
		{
			get { return world; }
		}

		/// <summary>
		/// Gets a value that indicates wheather physics are enabled or not.
		/// </summary>
		public bool PhysicsEnabled
		{
			get { return this.world != null; }
		}
	}

	/// <summary>
	/// Specifies flags for world creation.
	/// </summary>
	[Flags]
	public enum SceneCreationFlags
	{
		/// <summary>
		/// No special functionality.
		/// </summary>
		None = 0,

		/// <summary>
		/// Enables physics for the scene.
		/// </summary>
		EnablePhysics = (1 << 0)
	}
}
