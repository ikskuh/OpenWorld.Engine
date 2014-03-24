using BulletSharp;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine
{
	/// <summary>
	/// A class with extension methods.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Checks if the position is in the box.
		/// </summary>
		/// <param name="box">The box</param>
		/// <param name="pos">The position</param>
		/// <returns>True if pos is in box.</returns>
		public static bool Contains(this Box2 box, Vector2 pos)
		{
			return box.Contains(pos.X, pos.Y);
		}

		/// <summary>
		/// Checks if xy is in the box.
		/// </summary>
		/// <param name="box">The box</param>
		/// <param name="x">x-coordinate of the point</param>
		/// <param name="y">y-coordinate of the point</param>
		/// <returns>True if (x,y) is in box.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
		public static bool Contains(this Box2 box, float x, float y)
		{
			return x >= box.Left && x <= box.Right && y >= box.Top && y <= box.Bottom;
		}

		/// <summary>
		/// Translates a Box2.
		/// </summary>
		/// <param name="box">The box to be translated.</param>
		/// <param name="pos">Offset</param>
		/// <returns></returns>
		public static Box2 Translate(this Box2 box, Vector2 pos)
		{
			return Translate(box, pos.X, pos.Y);
		}

		/// <summary>
		/// Translates a Box2.
		/// </summary>
		/// <param name="box">The box to be translated.</param>
		/// <param name="x">X offset</param>
		/// <param name="y">Y offset</param>
		/// <returns></returns>
		public static Box2 Translate(this Box2 box, float x, float y)
		{
			box.Left += x;
			box.Right += x;
			box.Top += y;
			box.Bottom += y;
			return box;
		}

		/// <summary>
		/// Converts a Bullet vector.
		/// </summary>
		/// <param name="vector"></param>
		/// <returns></returns>
		public static OpenTK.Vector3 ToOpenTK(this BulletSharp.Vector3 vector)
		{
			return new OpenTK.Vector3(vector.X, vector.Y, vector.Z);
		}

		/// <summary>
		/// Converts a OpenTK vector.
		/// </summary>
		/// <param name="vector"></param>
		/// <returns></returns>
		public static BulletSharp.Vector3 ToBullet(this OpenTK.Vector3 vector)
		{
			return new BulletSharp.Vector3(vector.X, vector.Y, vector.Z);
		}

		/// <summary>
		/// Converts an OpenTK matrix to a Bullet matrix.
		/// </summary>
		/// <param name="matrix">The matrix to convert.</param>
		/// <returns>The converted matrix.</returns>
		public static Matrix ToBullet(this Matrix4 matrix)
		{
			return new Matrix()
			{
				M11 = matrix.M11,
				M12 = matrix.M12,
				M13 = matrix.M13,
				M14 = matrix.M14,

				M21 = matrix.M21,
				M22 = matrix.M22,
				M23 = matrix.M23,
				M24 = matrix.M24,

				M31 = matrix.M31,
				M32 = matrix.M32,
				M33 = matrix.M33,
				M34 = matrix.M34,

				M41 = matrix.M41,
				M42 = matrix.M42,
				M43 = matrix.M43,
				M44 = matrix.M44,
			};
		}


		/// <summary>
		/// Converts a Bullet matrix to an OpenTK matrix.
		/// </summary>
		/// <param name="matrix">The matrix to convert.</param>
		/// <returns>The converted matrix.</returns>
		public static Matrix4 ToOpenTK(this Matrix matrix)
		{
			return new Matrix4()
			{
				Row0 = new OpenTK.Vector4(matrix.M11, matrix.M12, matrix.M13, matrix.M14),
				Row1 = new OpenTK.Vector4(matrix.M21, matrix.M22, matrix.M23, matrix.M24),
				Row2 = new OpenTK.Vector4(matrix.M31, matrix.M32, matrix.M33, matrix.M34),
				Row3 = new OpenTK.Vector4(matrix.M41, matrix.M42, matrix.M43, matrix.M44),
			};
		}

		/// <summary>
		/// Converts an object to a given type.
		/// </summary>
		/// <typeparam name="T">Conversion type</typeparam>
		/// <param name="obj">Object to convert.</param>
		/// <returns>Converted object</returns>
		public static T As<T>(this object obj)
			where T : class
		{
			return obj as T;
		}

		/// <summary>
		/// Converts degrees to radians.
		/// </summary>
		/// <param name="flt"></param>
		/// <returns></returns>
		public static float ToRadians(this float flt)
		{
			return MathHelper.DegreesToRadians(flt);
		}

		/// <summary>
		/// Converts radians to degrees.
		/// </summary>
		/// <param name="flt"></param>
		/// <returns></returns>
		public static float ToDegrees(this float flt)
		{
			return MathHelper.RadiansToDegrees(flt);
		}
	}
}
