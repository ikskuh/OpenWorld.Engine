using Jitter.Collision.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Defines a rigid body object.
	/// </summary>
	[RequiredComponent(typeof(Shape))]
	public sealed class RigidBody : SceneNode.Component
	{
		Jitter.Dynamics.RigidBody rigidBody;


		/// <summary>
		/// Starts the component.
		/// </summary>
		protected override void OnStart()
		{
			if (this.rigidBody == null)
			{
				var shape = this.Node.Components.Get<Shape>();
				if (shape == null)
					throw new NullReferenceException("Could not find a shape component for the rigid body.");
				this.rigidBody = new Jitter.Dynamics.RigidBody(
					shape.GetShape(),
					this.Node.Material.JitterMaterial,
					false);
			}

			if (this.Node.Scene.PhysicsEnabled)
			{
				this.Node.Scene.World.AddBody(this.rigidBody);
			}
		}


		/// <summary>
		/// Updates the component every frame.
		/// </summary>
		protected override void OnUpdate()
		{
			this.Node.Transform.SetMatrix(this.rigidBody.Orientation.ToOpenTK(this.rigidBody.Position));
		}


		/// <summary>
		/// Stops the component.
		/// </summary>
		protected override void OnStop()
		{
			if (this.Node.Scene.PhysicsEnabled)
			{
				this.Node.Scene.World.RemoveBody(this.rigidBody);
			}
		}
	}
}
