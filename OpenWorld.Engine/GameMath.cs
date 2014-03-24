using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Provides math methods for use in game. Uses float instead of double
	/// </summary>
	public static class GameMath
	{
		/// <summary>
		/// PI
		/// </summary>
		public const float PI = (float)Math.PI;

		/// <summary>
		/// Clamps a value to the range [min; max].
		/// </summary>
		/// <param name="value">The value to clamp.</param>
		/// <param name="min">The inclusive lower bound.</param>
		/// <param name="max">The inclusive upper bound.</param>
		/// <returns>The clamped value.</returns>
		public static float Clamp(float value, float min, float max)
		{
			if (value < min) return min;
			if (value > max) return max;
			return value;
		}

		/// <summary>
		/// Clamps the value to the range [0; 1].
		/// </summary>
		/// <param name="value">Value to clamp.</param>
		/// <returns>Clamped value.</returns>
		public static float Clamp(float value)
		{
			return GameMath.Clamp(value, 0.0f, 1.0f);
		}

		/// <summary>
		/// Converts degrees to radians.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static float ToRadians(float value)
		{
			return MathHelper.DegreesToRadians(value);
		}

		/// <summary>
		/// Converts radians to degrees.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static float ToDegrees(float value)
		{
			return MathHelper.RadiansToDegrees(value);
		}

		/// <summary>
		/// Sinus
		/// </summary>
		/// <param name="angle"></param>
		/// <returns></returns>
		public static float Sin(float angle)
		{
			return (float)Math.Sin(ToRadians(angle));
		}

		/// <summary>
		/// Cosinus
		/// </summary>
		/// <param name="angle"></param>
		/// <returns></returns>
		public static float Cos(float angle)
		{
			return (float)Math.Cos(ToRadians(angle));
		}

		/// <summary>
		/// Tangens
		/// </summary>
		/// <param name="angle"></param>
		/// <returns></returns>
		public static float Tan(float angle)
		{
			return (float)Math.Tan(ToRadians(angle));
		}

		/// <summary>
		/// Power
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
		public static float Pow(float x, float y)
		{
			return (float)Math.Pow(x, y);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
		public static float ATan2(float x, float y)
		{
			return ToDegrees((float)Math.Atan2(x, y));
		}

		/// <summary>
		/// Square root
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static float Sqrt(float value)
		{
			return (float)Math.Sqrt(value);
		}

		/// <summary>
		/// Creates a rotation quaternion for euler angles.
		/// </summary>
		/// <param name="pan"></param>
		/// <param name="tilt"></param>
		/// <param name="roll"></param>
		/// <returns></returns>
		public static Quaternion CreateRotation(float pan, float tilt, float roll)
		{
			pan = pan.ToRadians();
			tilt = tilt.ToRadians();
			roll = roll.ToRadians();
			float num9 = roll * 0.5f;
			float num6 = (float)Math.Sin((double)num9);
			float num5 = (float)Math.Cos((double)num9);
			float num8 = tilt * 0.5f;
			float num4 = (float)Math.Sin((double)num8);
			float num3 = (float)Math.Cos((double)num8);
			float num7 = pan * 0.5f;
			float num2 = (float)Math.Sin((double)num7);
			float num = (float)Math.Cos((double)num7);
			Quaternion result = new Quaternion();
			result.X = ((num * num4) * num5) + ((num2 * num3) * num6);
			result.Y = ((num2 * num3) * num5) - ((num * num4) * num6);
			result.Z = ((num * num3) * num6) - ((num2 * num4) * num5);
			result.W = ((num * num3) * num5) + ((num2 * num4) * num6);
			result.Normalize();
			return result;
		}
	}
}
