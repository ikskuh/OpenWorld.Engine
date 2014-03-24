using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Provides methods to log messages.
	/// </summary>
	public static class Log
	{
		private static LogDelegate FinalWrite = (s) =>
			{
				Console.Write(s);
#if DEBUG
				System.Diagnostics.Trace.Write(s);
#endif
			};

		/// <summary>
		/// Sets the logging method.
		/// </summary>
		/// <param name="delegate">The delegate that is used for writing into the log.</param>
		public static void SetDelegate(LogDelegate @delegate)
		{
			if (@delegate == null)
				throw new ArgumentNullException("delegate");
			Log.FinalWrite = @delegate;
		}

		/// <summary>
		/// Writes a string into the log.
		/// </summary>
		/// <param name="message">The string to be written.</param>
		public static void Write(string message)
		{
			if (Log.FinalWrite != null)
				Log.FinalWrite(message);
		}

		/// <summary>
		/// Writes a formatted string into the log.
		/// </summary>
		/// <param name="format">The formatted string to be written.</param>
		/// <param name="args">The arguments for the string formatting.</param>
		public static void Write(string format, params object[] args)
		{
			Log.Write(string.Format(System.Globalization.CultureInfo.InvariantCulture, format, args));
		}

		/// <summary>
		/// Writes a line into the log.
		/// </summary>
		/// <param name="message">The line to be written.</param>
		public static void WriteLine(string message)
		{
			Log.Write(message + Environment.NewLine);
		}

		/// <summary>
		/// Writes a formatted line into the log.
		/// </summary>
		/// <param name="format">The formatted line to be written.</param>
		/// <param name="args">The arguments for the string formatting.</param>
		public static void WriteLine(string format, params object[] args)
		{
			Log.WriteLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, format, args));
		}

		/// <summary>
		/// Logs an error message.
		/// </summary>
		/// <param name="msg">The error message to be logged.</param>
		public static void Error(string msg)
		{
			Log.Error(msg, null);
		}

		/// <summary>
		/// Logs an error message with an additional Exception.
		/// </summary>
		/// <param name="msg">The error message to be logged.</param>
		/// <param name="ex">The exception that triggered the error.</param>
		public static void Error(string msg, Exception ex)
		{
			Log.WriteLine(msg);
			if (ex != null)
				Log.WriteLine("{0}", ex);
		}
	}

	/// <summary>
	/// Represents a method to log something.
	/// </summary>
	/// <param name="message">The message to be logged.</param>
	public delegate void LogDelegate(string message);
}
