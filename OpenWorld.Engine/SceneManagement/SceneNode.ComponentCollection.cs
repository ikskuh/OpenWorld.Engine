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
				where T: Component
			{
				return Component.Add<T>(this.parent);
			}

			/// <summary>
			/// Gets a component of the scene node.
			/// </summary>
			/// <typeparam name="T">Type of the component.</typeparam>
			/// <returns>Component or null if none.</returns>
			public T Get<T>()
				where T : Component
			{
				if (!this.parent.components.ContainsKey(typeof(T)))
					return null;
				return (T)this.parent.components[typeof(T)];
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
