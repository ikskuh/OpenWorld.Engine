using OpenTK;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Represents a floating point color value with 3 color channles and one alpha channels.
	/// </summary>
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public partial struct Color : IEquatable<Color>, IXmlSerializable
	{
		private float r;
		private float g;
		private float b;
		private float a;

		/// <summary>
		/// Instantiates a new color.
		/// </summary>
		/// <param name="r">Red value</param>
		/// <param name="g">Green value</param>
		/// <param name="b">Blue value</param>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "r")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "g")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "b")]
		public Color(float r, float g, float b)
			: this(r, g, b, 1.0f)
		{

		}

		/// <summary>
		/// Instantiates a new color.
		/// </summary>
		/// <param name="r">Red value</param>
		/// <param name="g">Green value</param>
		/// <param name="b">Blue value</param>
		/// <param name="a">Alpha value</param>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "r")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "g")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "b")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "a")]
		public Color(float r, float g, float b, float a)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			if (obj.GetType() != typeof(Color)) return false;
			return this.Equals((Color)obj);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(Color other)
		{
			return
				this.r == other.r &&
				this.g == other.g &&
				this.b == other.b &&
				this.a == other.a;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return this.r.GetHashCode() ^ this.g.GetHashCode() ^ this.b.GetHashCode() ^ this.a.GetHashCode();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0} {1} {2} {3}", this.r, this.g, this.b, this.a);
		}

		/// <summary>
		/// Gets or sets the red value.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "R")]
		public float R
		{
			get { return r; }
			set { r = value; }
		}

		/// <summary>
		/// Gets or sets the green value.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "G")]
		public float G
		{
			get { return g; }
			set { g = value; }
		}

		/// <summary>
		/// Gets or sets the blue value.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "B")]
		public float B
		{
			get { return b; }
			set { b = value; }
		}

		/// <summary>
		/// Gets or sets the alpha value.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "A")]
		public float A
		{
			get { return a; }
			set { a = value; }
		}

		#region Static Methods

		/// <summary>
		/// Creates a color from byte values.
		/// </summary>
		/// <param name="r">Red value</param>
		/// <param name="g">Green value</param>
		/// <param name="b">Blue value</param>
		/// <returns>Color.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "b"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "g"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "r")]
		public static Color FromRgb(byte r, byte g, byte b)
		{
			return new Color(r / 255.0f, g / 255.0f, b / 255.0f, 1.0f);
		}

		/// <summary>
		/// Creates a color from byte values.
		/// </summary>
		/// <param name="r">Red value</param>
		/// <param name="g">Green value</param>
		/// <param name="b">Blue value</param>
		/// <param name="a">Alpha value</param>
		/// <returns>Color.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "a"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "b"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "r"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "g")]
		public static Color FromRgb(byte r, byte g, byte b, byte a)
		{
			return new Color(r / 255.0f, g / 255.0f, b / 255.0f, a / 255.0f);
		}

		#endregion
		#region Implicit Conversions

		/// <summary>
		/// Converts the color to a System.Drawing.Color.
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public static implicit operator Color(System.Drawing.Color color)
		{
			return new Color(
				color.R / 255.0f,
				color.G / 255.0f,
				color.B / 255.0f,
				color.A / 255.0f);
		}

		/// <summary>
		/// Converts the color to a OpenTK.Vector3.
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public static implicit operator Color(OpenTK.Vector3 color)
		{
			return new Color(color.X, color.Y, color.Z, 1.0f);
		}

		/// <summary>
		/// Converts a OpenTK.Vector4 to a Color.
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public static implicit operator Color(OpenTK.Vector4 color)
		{
			return new Color(color.X, color.Y, color.Z, color.W);
		}

		/// <summary>
		/// Converts a OpenTK.Graphics.Color4 to a Color.
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public static implicit operator Color(OpenTK.Graphics.Color4 color)
		{
			return new Color(color.R, color.G, color.B, color.A);
		}

		/// <summary>
		/// Converts a float value to a grayscale color.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static implicit operator Color(float value)
		{
			return new Color(value, value, value, 1.0f);
		}

		/// <summary>
		/// Converts a Color to a OpenTK.Graphics.Color4.
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public static implicit operator OpenTK.Graphics.Color4(Color color)
		{
			return new OpenTK.Graphics.Color4(color.r, color.g, color.b, color.a);
		}

		#endregion

		#region Operators

		/// <summary>
		/// Adds two color values.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static Color Add(Color left, Color right)
		{
			return new Color(
						 left.r + right.r,
						 left.g + right.g,
						 left.b + right.g,
						 left.a + right.a);
		}

		/// <summary>
		/// Subtracts two color values.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static Color Subtract(Color left, Color right)
		{
			return new Color(
						 left.r - right.r,
						 left.g - right.g,
						 left.b - right.g,
						 left.a - right.a);
		}

		/// <summary>
		/// Multiplies two color values.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static Color Multiply(Color left, Color right)
		{
			return new Color(
						 left.r * right.r,
						 left.g * right.g,
						 left.b * right.g,
						 left.a * right.a);
		}

		/// <summary>
		/// Adds two color values.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static Color operator +(Color left, Color right)
		{
			return Add(left, right);
		}

		/// <summary>
		/// Subtracts two color values.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static Color operator -(Color left, Color right)
		{
			return Subtract(left, right);
		}

		/// <summary>
		/// Multiplies two color values.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static Color operator *(Color left, Color right)
		{
			return Multiply(left, right);
		}

		/// <summary>
		/// Compares for equality.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator ==(Color left, Color right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// Compares for inequality.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator !=(Color left, Color right)
		{
			return !left.Equals(right);
		}

		#endregion

		/// <summary>
		/// Tries to parse a color from a string.
		/// </summary>
		/// <param name="text">Text that contains the text.</param>
		/// <param name="color">Parsed result or black.</param>
		/// <returns>False if failed.</returns>
		public static bool TryParse(string text, out Color color)
		{
			color = Color.Black;
			if (string.IsNullOrWhiteSpace(text)) return false;
			string[] parts = text.Split(';');
			if (parts.Length != 3 && parts.Length != 4) return false;

			color.r = float.Parse(parts[0].Trim(), CultureInfo.InvariantCulture);
			color.g = float.Parse(parts[1].Trim(), CultureInfo.InvariantCulture);
			color.b = float.Parse(parts[2].Trim(), CultureInfo.InvariantCulture);
			if (parts.Length == 4)
				color.a = float.Parse(parts[3].Trim(), CultureInfo.InvariantCulture);
			else
				color.a = 1.0f;

			return true;
		}

		#region IXmlSerializable

		System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		void IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
		{
			string value = reader.ReadString();
			Color.TryParse(value, out this);
		}

		void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
		{
			string result = string.Format(
				CultureInfo.InvariantCulture, 
				"{0};{1};{2};{3}",
				this.R,
				this.G,
				this.B,
				this.A);

			writer.WriteString(result);
		}

		#endregion
	}
}
