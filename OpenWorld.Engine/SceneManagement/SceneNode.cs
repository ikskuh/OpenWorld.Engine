using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Defines a part of a scene.
	/// </summary>
	public sealed partial class SceneNode : DiGraph<SceneNode>
	{
		private readonly Transform transform;
		private readonly Dictionary<Type, Component> components;
		private readonly ComponentCollection componentCollection;

		/// <summary>
		/// Occurs when the node is updated.
		/// </summary>
		public event EventHandler<SceneNodeUpdateEventArgs> Update;

		/// <summary>
		/// Occurs when the node is drawn.
		/// </summary>
		public event EventHandler<SceneNodeDrawEventArgs> Draw;

		/// <summary>
		/// Creates a new scene node.
		/// </summary>
		public SceneNode()
		{
			this.transform = new Transform();
			this.components = new Dictionary<Type, Component>();
			this.componentCollection = new ComponentCollection(this);
		}

		/// <summary>
		/// Updates the node.
		/// </summary>
		/// <param name="time">Time snapshot</param>
		internal void DoUpdate(GameTime time)
		{
			if (this.Update != null)
				this.Update(this, new SceneNodeUpdateEventArgs(time));
			foreach(var comp in this.Components)
			{
				comp.Update();
			}
			// Pass the call method to all children.
			foreach (var child in this.Children)
				child.DoUpdate(time);
		}

		/// <summary>
		/// Draws the node.
		/// </summary>
		/// <param name="time">Time snapshot</param>
		/// <param name="renderer">The renderer that draws the node.</param>
		internal void DoDraw(GameTime time, SceneRenderer renderer)
		{
			if (this.Draw != null)
				this.Draw(this, new SceneNodeDrawEventArgs(time, renderer));
			// Pass the draw call to all children.
			foreach (var child in this.Children)
				child.DoDraw(time, renderer);
		}

		/// <summary>
		/// Gets the transform of this node.
		/// </summary>
		public Transform Transform { get { return this.transform; } }

		/// <summary>
		/// Gets a collection of all components.
		/// </summary>
		public ComponentCollection Components
		{
			get { return componentCollection; }
		} 
	}
}
