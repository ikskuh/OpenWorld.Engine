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
		protected override void OnStart()
		{
			base.OnStart();
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
		protected override void OnUpdate()
		{
			base.OnUpdate();
			if (this.lua == null)
				return;
			var fn = this.lua.GetFunction("update");
			if (fn == null)
				return;
			fn.Call(this);
		}

		/// <summary>
		/// Stops the component.
		/// </summary>
		protected override void OnStop()
		{
			base.OnStop();
			if (this.lua == null)
				return;
			var fn = this.lua.GetFunction("stop");
			if (fn == null)
				return;
			fn.Call(this);
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
