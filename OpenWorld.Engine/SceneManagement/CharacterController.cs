using BulletSharp;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Defines a character controller.
	/// </summary>
	public sealed class CharacterController : Component
	{
		const float characterHeight = 1.0f;
		const float characterWidth = 0.5f;
		const float stepHeight = 0.40f;

		ConvexShape shape;
		PairCachingGhostObject ghostObject;
		KinematicCharacterController kcc;

		/// <summary>
		/// Creates a new character controller.
		/// </summary>
		public CharacterController()
		{
			this.FallSpeed = 1.5f;
			this.MaxJumpHeight = 10.0f;
		}

		/// <summary>
		/// Starts the character controller.
		/// </summary>
		/// <param name="time"></param>
		protected override void OnStart(GameTime time)
		{
			base.OnStart(time);
			if (this.Node.Scene.PhysicsEnabled)
			{
				this.shape = new CapsuleShape(characterWidth, characterHeight);

				this.ghostObject = new PairCachingGhostObject();
				this.ghostObject.WorldTransform = this.Node.Transform.WorldTransform.ToBullet();
				this.ghostObject.CollisionShape = this.shape;
				this.ghostObject.CollisionFlags = CollisionFlags.CharacterObject;

				this.kcc = new KinematicCharacterController(this.ghostObject, this.shape, stepHeight);
				this.kcc.SetUpAxis(1);

				this.Node.Scene.World.AddCollisionObject(
					this.ghostObject, 
					CollisionFilterGroups.CharacterFilter, 
					CollisionFilterGroups.StaticFilter | CollisionFilterGroups.DefaultFilter);
				this.Node.Scene.World.AddAction(this.kcc);
			}
		}

		/// <summary>
		/// Updates the character controller.
		/// </summary>
		/// <param name="time"></param>
		protected override void OnUpdate(GameTime time)
		{
			base.OnUpdate(time);
			if (this.Node.Scene.PhysicsEnabled)
			{
				this.kcc.SetFallSpeed(this.FallSpeed);
				this.kcc.SetJumpSpeed(this.JumpSpeed);
				this.kcc.SetMaxJumpHeight(this.MaxJumpHeight);
				this.kcc.SetWalkDirection(new BulletSharp.Vector3(
					this.WalkDirection.X,
					this.WalkDirection.Y,
					this.WalkDirection.Z));

				var transform = this.ghostObject.WorldTransform.ToOpenTK();
				this.Node.Transform.WorldTransform = transform;
			}
		}

		/// <summary>
		/// Stops the character controller.
		/// </summary>
		/// <param name="time"></param>
		protected override void OnStop(GameTime time)
		{
			base.OnStop(time);

			if (this.Node.Scene.PhysicsEnabled)
			{
				this.Node.Scene.World.RemoveCollisionObject(this.ghostObject);
				this.Node.Scene.World.RemoveAction(this.kcc);

				if (this.kcc != null)
					this.kcc.Dispose();
				if (this.ghostObject != null)
					this.ghostObject.Dispose();
				if (this.shape != null)
					this.shape.Dispose();

				this.kcc = null;
				this.ghostObject = null;
				this.shape = null;
			}
		}

		/// <summary>
		/// Gets or sets the walk direction.
		/// </summary>
		public OpenTK.Vector3 WalkDirection { get; set; }

		/// <summary>
		/// Gets or sets the jump speed.
		/// </summary>
		public float JumpSpeed { get; set; }

		/// <summary>
		/// Gets or sets the fall speed.
		/// </summary>
		public float FallSpeed { get; set; }

		/// <summary>
		/// Gets or sets the maximum jump height.
		/// </summary>
		public float MaxJumpHeight { get; set; }
	}
}
