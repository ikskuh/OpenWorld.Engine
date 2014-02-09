using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	partial class SceneNode
	{
		/// <summary>
		/// Defines a component that can be bound to a scene node.
		/// </summary>
		public abstract class Component
		{
			private static readonly object lockObject = new object();
			private static SceneNode componentNode;

			/// <summary>
			/// Adds a component to a scene node.
			/// </summary>
			/// <typeparam name="T">Type of the component.</typeparam>
			/// <param name="node">The scene node that should host the component.</param>
			/// <returns>Created component.</returns>
			public static T Add<T>(SceneNode node)
				where T : Component
			{
				if (node == null)
					throw new ArgumentNullException("node");
				if (node.components.ContainsKey(typeof(T)))
					throw new InvalidOperationException("Component " + typeof(T).Name + " already exists in " + node.GetType().Name);
				RequiredComponentAttribute[] attribs = (RequiredComponentAttribute[])typeof(T).GetCustomAttributes(typeof(RequiredComponentAttribute), false);
				foreach (var attrib in attribs)
				{
					if (!attrib.RequiredType.IsSubclassOf(typeof(Component)))
						continue;
					if (node.Components.Get(attrib.RequiredType) == null)
						throw new InvalidOperationException("Cannot add component: " + attrib.RequiredType.Name + " is required.");
				}
				lock (Component.lockObject)
				{
					Component.componentNode = node;
					var component = Activator.CreateInstance<T>();
					node.components.Add(typeof(T), component);
					Component.componentNode = null;
					return component;
				}
			}

			/// <summary>
			/// Removes a component from a scene node.
			/// </summary>
			/// <typeparam name="T">Type of the component.</typeparam>
			/// <param name="node">Node that contains the scene node.</param>
			public static void Remove<T>(SceneNode node)
				where T : Component
			{
				if (node == null)
					throw new ArgumentNullException("node");
				if (!node.components.ContainsKey(typeof(T)))
					throw new InvalidOperationException("Component " + typeof(T).Name + " does not exists in " + node.GetType().Name);
				node.components.Remove(typeof(T));
			}

			readonly SceneNode node;
			private bool started = false;

			/// <summary>
			/// Creates a new component.
			/// </summary>
			internal Component()
			{
				this.node = Component.componentNode;
				this.Enabled = true;
			}


			internal void Update(GameTime time)
			{
				if (this.Enabled)
				{
					if (!this.started)
					{
						this.OnStart(time);
						this.started = true;
					}
					if (this.started)
					{
						this.OnUpdate(time);
					}
				}
				else
				{
					if (this.started)
					{
						this.OnStop(time);
					}
				}
			}

			internal void PreRender(GameTime time, SceneRenderer renderer)
			{
				if (!this.Enabled) return;
				this.OnPreRender(time, renderer);
			}

			internal void Render(GameTime time, SceneRenderer renderer)
			{
				if (!this.Enabled) return;
				this.OnRender(time, renderer);
			}
			internal void PostRender(GameTime time, SceneRenderer renderer)
			{
				if (!this.Enabled) return;
				this.OnPostRender(time, renderer);
			}

			/// <summary>
			/// Starts the component.
			/// </summary>
			/// <param name="time">Time snapshot</param>
			protected virtual void OnStart(GameTime time) { }

			/// <summary>
			/// Updates the component every frame.
			/// </summary>
			/// <param name="time">Time snapshot</param>
			protected virtual void OnUpdate(GameTime time) { }

			/// <summary>
			/// Initializes rendering of the scene node.
			/// </summary>
			/// <param name="time">Time snapshot</param>
			/// <param name="renderer">The renderer that is used for drawing.</param>
			protected virtual void OnPreRender(GameTime time, SceneRenderer renderer) { }


			/// <summary>
			/// Renders the scene node.
			/// </summary>
			/// <param name="time">Time snapshot</param>
			/// <param name="renderer">The renderer that is used for drawing.</param>
			protected virtual void OnRender(GameTime time, SceneRenderer renderer) { }


			/// <summary>
			/// Postprocesses rendering of the scene node.
			/// </summary>
			/// <param name="time">Time snapshot</param>
			/// <param name="renderer">The renderer that is used for drawing.</param>
			protected virtual void OnPostRender(GameTime time, SceneRenderer renderer) { }

			/// <summary>
			/// Stops the component.
			/// </summary>
			/// <param name="time">Time snapshot</param>
			protected virtual void OnStop(GameTime time) { }

			/// <summary>
			/// Calls OnStop, then releases all other non-released resources.
			/// </summary>
			protected internal void Release() { this.OnStop(Game.Current.Time); }

			/// <summary>
			/// Gets the hosting scene node.
			/// </summary>
			public SceneNode Node { get { return this.node; } }

			/// <summary>
			/// Gets or sets a value that indicates wheather the component is active or not.
			/// </summary>
			public bool Enabled { get; set; }
		}
	}
}
