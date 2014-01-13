using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines a singleton.
	/// </summary>
	/// <typeparam name="T">Type name of the singleton object.</typeparam>
	public abstract class Singleton<T>
		where T : class
	{
		private static bool allowCreation = false;
		private static T instance;

		/// <summary>
		/// Gets the instance of the singleton.
		/// </summary>
		public static T Instance
		{
			get
			{
				if (Singleton<T>.instance == null)
				{
					Singleton<T>.allowCreation = true;
					Singleton<T>.instance = Activator.CreateInstance<T>();
					Singleton<T>.allowCreation = false;
				}
				return Singleton<T>.instance;
			}
		}

		/// <summary>
		/// Instantiates the singleton once.
		/// </summary>
		protected Singleton()
		{
			if (!Singleton<T>.allowCreation)
				throw new InvalidOperationException("Cannot instantiate a singleton object. Use Singleton.Instance instead.");
		}
	}
}
