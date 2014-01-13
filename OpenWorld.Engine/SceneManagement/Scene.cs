using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Defines a 3D scene.
	/// </summary>
	public sealed class Scene : IEnumerable<SceneNode>
	{
		readonly SceneNode root = null;

		/// <summary>
		/// Instantiates a new scene.
		/// </summary>
		public Scene()
		{
			this.root = new SceneNode();
		}

		/// <summary>
		/// Gets an enumerator over all scene nodes.
		/// </summary>
		/// <returns></returns>
		public IEnumerator<SceneNode> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
