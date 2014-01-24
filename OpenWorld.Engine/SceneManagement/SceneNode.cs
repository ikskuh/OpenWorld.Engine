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
		private Scene scene;

		/// <summary>
		/// Creates a new scene node.
		/// </summary>
		public SceneNode()
		{
			this.transform = new Transform(this);
			this.components = new Dictionary<Type, Component>();
			this.componentCollection = new ComponentCollection(this);
			this.Material = new Material();
		}

		/// <summary>
		/// Updates the node.
		/// </summary>
		/// <param name="time">Time snapshot</param>
		internal void DoUpdate(GameTime time)
		{
			foreach (var comp in this.Components)
				if (comp.Enabled)
					comp.Update(time);
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
			foreach (var comp in this.Components)
				if (comp.Enabled)
					comp.PreRender(time, renderer);

			foreach (var comp in this.Components)
				if (comp.Enabled)
					comp.Render(time, renderer);

			// Pass the draw call to all children.
			foreach (var child in this.Children)
				child.DoDraw(time, renderer);

			foreach (var comp in this.Components)
				if (comp.Enabled)
					comp.PostRender(time, renderer);
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

		/// <summary>
		/// Gets or sets the material of this node.
		/// </summary>
		public Material Material { get; set; }

		/// <summary>
		/// Gets a value that indicates wheater this nood is a root node or not.
		/// </summary>
		public bool IsRoot { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public Scene Scene
		{
			get
			{
				if (this.scene != null)
					return this.scene;
				if (this.Parent != null)
					return this.Parent.Scene;
				return null;
			}
		}

		/// <summary>
		/// Creates a root node for a scene.
		/// </summary>
		/// <param name="scene"></param>
		/// <returns></returns>
		internal static SceneNode CreateRoot(Scene scene)
		{
			return new SceneNode()
			{
				IsRoot = true,
				scene = scene
			};
		}
	}
}
