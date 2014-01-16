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
			protected Component()
			{
				this.node = Component.componentNode;
				this.Enabled = true;
			}


			internal void Update()
			{
				if (this.Enabled)
				{
					if (!this.started)
					{
						this.OnStart();
						this.started = true;
					}
					if (this.started)
					{
						this.OnUpdate();
					}
				}
				else
				{
					if (this.started)
					{
						this.OnStop();
					}
				}
			}

			/// <summary>
			/// Starts the component.
			/// </summary>
			protected virtual void OnStart()
			{

			}

			/// <summary>
			/// Updates the component every frame.
			/// </summary>
			protected virtual void OnUpdate()
			{

			}

			/// <summary>
			/// Stops the component.
			/// </summary>
			protected virtual void OnStop()
			{

			}

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
