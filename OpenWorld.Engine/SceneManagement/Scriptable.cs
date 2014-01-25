using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Defines a component that enables lua scriptable objects.
	/// </summary>
	public class Scriptable : SceneNode.Component
	{
		Lua lua;
		private string script;

		/// <summary>
		/// Starts the component.
		/// </summary>
		protected override void OnStart(GameTime time)
		{
			base.OnStart(time);
			if (this.lua == null)
				return;
			var fn = this.lua.GetFunction("start");
			if (fn == null)
				return;
			fn.Call(this);
		}

		/// <summary>
		/// Updates the component every frame.
		/// </summary>
		protected override void OnUpdate(GameTime time)
		{
			base.OnUpdate(time);
			if (this.lua == null)
				return;
			var fn = this.lua.GetFunction("update");
			if (fn == null)
				return;
			fn.Call(this, time);
		}

		/// <summary>
		/// Stops the component.
		/// </summary>
		protected override void OnStop(GameTime time)
		{
			base.OnStop(time);
			if (this.lua == null)
				return;
			var fn = this.lua.GetFunction("stop");
			if (fn == null)
				return;
			fn.Call(this, time);
		}

		/// <summary>
		/// Gets or sets the script of the component.
		/// <remarks>If the script is set, the lua engine will be recreated.</remarks>
		/// </summary>
		public string Script
		{
			get { return this.script; }
			set
			{
				this.script = value;
				if (this.lua != null)
					this.lua.Dispose();
				this.lua = new Lua();
				this.lua.DoString(this.script);
			}
		}
	}
}
