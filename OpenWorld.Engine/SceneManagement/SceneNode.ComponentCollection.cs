using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	partial class SceneNode
	{
		/// <summary>
		/// Defines a collection of scene node components.
		/// </summary>
		public sealed class ComponentCollection : IEnumerable<Component>
		{
			private readonly SceneNode parent = null;

			internal ComponentCollection(SceneNode parent)
			{
				if (parent == null)
					throw new ArgumentNullException("parent");
				this.parent = parent;
			}

			/// <summary>
			/// Adds a new component to the scene node.
			/// </summary>
			/// <typeparam name="T">Type of the component.</typeparam>
			/// <returns></returns>
			public T Add<T>()
				where T : Component
			{
				return Component.Add<T>(this.parent);
			}

			/// <summary>
			/// Gets a component of the scene node.
			/// </summary>
			/// <typeparam name="T">Type of the component.</typeparam>
			/// <remarks>If there is no component fitting the exact type, the first fitting subclass component will be returned.</remarks>
			/// <returns>Component or null if none.</returns>
			public T Get<T>()
				where T : Component
			{
				return (T)this.Get(typeof(T));
			}

			/// <summary>
			/// Gets a component of the scene node.
			/// </summary>
			/// <param name="type">Type of the component.</param>
			/// <remarks>If there is no component fitting the exact type, the first fitting subclass component will be returned.</remarks>
			/// <returns>Component or null if none.</returns>
			public Component Get(Type type)
			{
				if (!type.IsSubclassOf(typeof(Component)))
					throw new InvalidOperationException("Invalid type.");
				if (this.parent.components.ContainsKey(type))
					return this.parent.components[type];
				foreach(var component in parent.components)
				{
					if (component.Key.IsSubclassOf(type))
						return component.Value;
				}
				return null;
			}

			/// <summary>
			/// Removes a component from the scene node.
			/// </summary>
			/// <typeparam name="T">Type of the component.</typeparam>
			public void Remove<T>()
				where T : Component
			{
				Component.Remove<T>(this.parent);
			}

			/// <summary>
			/// Gets an enumerator over all components.
			/// </summary>
			/// <returns></returns>
			public IEnumerator<Component> GetEnumerator()
			{
				return this.parent.components.Values.GetEnumerator();
			}

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}
		}
	}
}
