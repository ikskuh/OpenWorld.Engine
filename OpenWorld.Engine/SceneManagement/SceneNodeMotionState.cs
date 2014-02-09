using BulletSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	class SceneNodeMotionState : MotionState
	{
		readonly SceneNode node;

		public SceneNodeMotionState(SceneNode node)
		{
			this.node = node;
		}

		public override Matrix WorldTransform
		{
			get
			{
				return this.node.Transform.WorldTransform.ToBullet();
			}
			set
			{
				this.node.Transform.WorldTransform = value.ToOpenTK();
			}
		}
	}
}
