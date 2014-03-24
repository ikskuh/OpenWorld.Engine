using BulletSharp;
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
	public sealed class RigidBody : Component
	{
		bool isKinematic;
		BulletSharp.RigidBody rigidBody;


		/// <summary>
		/// Starts the component.
		/// </summary>
		protected override void OnStart(GameTime time)
		{
			if (this.rigidBody == null)
			{
				var shape = this.Node.Components.Get<Shape>();
				if (shape == null)
					throw new NullReferenceException("Could not find a shape component for the rigid body.");

				var constructionInfo = new RigidBodyConstructionInfo(
					this.Mass,
					new SceneNodeMotionState(this.Node),
					shape.GetShape());

				this.rigidBody = new BulletSharp.RigidBody(constructionInfo);
				this.rigidBody.UserObject = this;
				this.IsKinematic = this.isKinematic; // Just make sure the body gets its correct rigid body state.
			}

			if (this.Node.Scene.PhysicsEnabled)
			{
				this.Node.Scene.World.AddRigidBody(this.rigidBody);
			}
		}


		/// <summary>
		/// Stops the component.
		/// </summary>
		protected override void OnStop(GameTime time)
		{
			if (this.Node.Scene.PhysicsEnabled)
			{
				this.Node.Scene.World.RemoveRigidBody(this.rigidBody);
			}
		}

		/// <summary>
		/// Gets or sets the mass of the rigid body.
		/// </summary>
		public float Mass { get; set; }

		/// <summary>
		/// Gets or sets value that determines if the rigid body is kinematic or not.
		/// </summary>
		public bool IsKinematic
		{
			get
			{
				return this.isKinematic;
			}
			set
			{
				this.isKinematic = value;
				if (this.rigidBody != null)
				{
					if (this.isKinematic)
						this.rigidBody.CollisionFlags |= CollisionFlags.KinematicObject;
					else
						this.rigidBody.CollisionFlags &= ~CollisionFlags.KinematicObject;
				}
			}
		}

	}
}
