using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Provides methods to change the viewport.
	/// </summary>
	public sealed class Viewport
	{
		static ThreadLocal<Viewport> current = new ThreadLocal<Viewport>(() => new Viewport());

		/// <summary>
		/// Pushes the current viewport settings on the stack.
		/// </summary>
		public static void Push()
		{
			current.Value.stack.Push(current.Value.viewport);
		}

		/// <summary>
		/// Pops a viewport from the stack and makes it current.
		/// </summary>
		public static void Pop()
		{
			if (current.Value.stack.Count > 0)
				Viewport.Area = current.Value.stack.Pop();
			else
				Viewport.Area = GetDefault();
		}

		/// <summary>
		/// Gets or sets the current viewport area.
		/// </summary>
		public static Box2i Area
		{
			get
			{
				return current.Value.viewport;
			}
			set
			{
				current.Value.viewport = value;
				GL.Viewport(
					current.Value.viewport.Top,
					current.Value.viewport.Left,
					current.Value.viewport.Width,
					current.Value.viewport.Height);
			}
		}

		static Box2i GetDefault()
		{
			return new Box2i(0, 0, Game.Current.Width, Game.Current.Height);
		}

		Stack<Box2i> stack = new Stack<Box2i>();
		Box2i viewport;

		private Viewport()
		{
			viewport = GetDefault();
		}
	}
}
