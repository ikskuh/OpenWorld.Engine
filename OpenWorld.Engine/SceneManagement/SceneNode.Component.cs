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

					component.OnStart();
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

			/// <summary>
			/// Creates a new component.
			/// </summary>
			protected Component()
			{
				this.node = Component.componentNode;
			}

			/// <summary>
			/// Starts the component.
			/// </summary>
			protected virtual void OnStart()
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
		}
	}
}
