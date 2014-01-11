using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine
{
	partial struct Color
	{
		/// <summary>
		/// Returns a transparent black.
		/// </summary>
		public static Color Transparent { get { return new Color(0.0f, 0.0f, 0.0f, 0.0f); } }

		/// <summary>
		/// Returns the color Pink
		/// </summary>
		public static Color Pink { get { return Color.FromRgb(255, 192, 203); } }

		/// <summary>
		/// Returns the color LightPink
		/// </summary>
		public static Color LightPink { get { return Color.FromRgb(255, 182, 193); } }

		/// <summary>
		/// Returns the color HotPink
		/// </summary>
		public static Color HotPink { get { return Color.FromRgb(255, 105, 180); } }

		/// <summary>
		/// Returns the color DeepPink
		/// </summary>
		public static Color DeepPink { get { return Color.FromRgb(255, 20, 147); } }

		/// <summary>
		/// Returns the color PaleVioletRed
		/// </summary>
		public static Color PaleVioletRed { get { return Color.FromRgb(219, 112, 147); } }

		/// <summary>
		/// Returns the color MediumVioletRed
		/// </summary>
		public static Color MediumVioletRed { get { return Color.FromRgb(199, 21, 133); } }

		/// <summary>
		/// Returns the color LightSalmon
		/// </summary>
		public static Color LightSalmon { get { return Color.FromRgb(255, 160, 122); } }

		/// <summary>
		/// Returns the color Salmon
		/// </summary>
		public static Color Salmon { get { return Color.FromRgb(250, 128, 114); } }

		/// <summary>
		/// Returns the color DarkSalmon
		/// </summary>
		public static Color DarkSalmon { get { return Color.FromRgb(233, 150, 122); } }

		/// <summary>
		/// Returns the color LightCoral
		/// </summary>
		public static Color LightCoral { get { return Color.FromRgb(240, 128, 128); } }

		/// <summary>
		/// Returns the color IndianRed
		/// </summary>
		public static Color IndianRed { get { return Color.FromRgb(205, 92, 92); } }

		/// <summary>
		/// Returns the color Crimson
		/// </summary>
		public static Color Crimson { get { return Color.FromRgb(220, 20, 60); } }

		/// <summary>
		/// Returns the color FireBrick
		/// </summary>
		public static Color FireBrick { get { return Color.FromRgb(178, 34, 34); } }

		/// <summary>
		/// Returns the color DarkRed
		/// </summary>
		public static Color DarkRed { get { return Color.FromRgb(139, 0, 0); } }

		/// <summary>
		/// Returns the color Red
		/// </summary>
		public static Color Red { get { return Color.FromRgb(255, 0, 0); } }

		/// <summary>
		/// Returns the color OrangeRed
		/// </summary>
		public static Color OrangeRed { get { return Color.FromRgb(255, 69, 0); } }

		/// <summary>
		/// Returns the color Tomato
		/// </summary>
		public static Color Tomato { get { return Color.FromRgb(255, 99, 71); } }

		/// <summary>
		/// Returns the color Coral
		/// </summary>
		public static Color Coral { get { return Color.FromRgb(255, 127, 80); } }

		/// <summary>
		/// Returns the color DarkOrange
		/// </summary>
		public static Color DarkOrange { get { return Color.FromRgb(255, 140, 0); } }

		/// <summary>
		/// Returns the color Orange
		/// </summary>
		public static Color Orange { get { return Color.FromRgb(255, 165, 0); } }

		/// <summary>
		/// Returns the color Gold
		/// </summary>
		public static Color Gold { get { return Color.FromRgb(255, 215, 0); } }

		/// <summary>
		/// Returns the color Yellow
		/// </summary>
		public static Color Yellow { get { return Color.FromRgb(255, 255, 0); } }

		/// <summary>
		/// Returns the color LightYellow
		/// </summary>
		public static Color LightYellow { get { return Color.FromRgb(255, 255, 224); } }

		/// <summary>
		/// Returns the color LemonChiffon
		/// </summary>
		public static Color LemonChiffon { get { return Color.FromRgb(255, 250, 205); } }

		/// <summary>
		/// Returns the color LightGoldenrodYellow
		/// </summary>
		public static Color LightGoldenrodYellow { get { return Color.FromRgb(250, 250, 210); } }

		/// <summary>
		/// Returns the color PapayaWhip
		/// </summary>
		public static Color PapayaWhip { get { return Color.FromRgb(255, 239, 213); } }

		/// <summary>
		/// Returns the color Moccasin
		/// </summary>
		public static Color Moccasin { get { return Color.FromRgb(255, 228, 181); } }

		/// <summary>
		/// Returns the color PeachPuff
		/// </summary>
		public static Color PeachPuff { get { return Color.FromRgb(255, 218, 185); } }

		/// <summary>
		/// Returns the color PaleGoldenrod
		/// </summary>
		public static Color PaleGoldenrod { get { return Color.FromRgb(238, 232, 170); } }

		/// <summary>
		/// Returns the color Khaki
		/// </summary>
		public static Color Khaki { get { return Color.FromRgb(240, 230, 140); } }

		/// <summary>
		/// Returns the color DarkKhaki
		/// </summary>
		public static Color DarkKhaki { get { return Color.FromRgb(189, 183, 107); } }

		/// <summary>
		/// Returns the color Cornsilk
		/// </summary>
		public static Color Cornsilk { get { return Color.FromRgb(255, 248, 220); } }

		/// <summary>
		/// Returns the color BlanchedAlmond
		/// </summary>
		public static Color BlanchedAlmond { get { return Color.FromRgb(255, 235, 205); } }

		/// <summary>
		/// Returns the color Bisque
		/// </summary>
		public static Color Bisque { get { return Color.FromRgb(255, 228, 196); } }

		/// <summary>
		/// Returns the color NavajoWhite
		/// </summary>
		public static Color NavajoWhite { get { return Color.FromRgb(255, 222, 173); } }

		/// <summary>
		/// Returns the color Wheat
		/// </summary>
		public static Color Wheat { get { return Color.FromRgb(245, 222, 179); } }

		/// <summary>
		/// Returns the color BurlyWood
		/// </summary>
		public static Color BurlyWood { get { return Color.FromRgb(222, 184, 135); } }

		/// <summary>
		/// Returns the color Tan
		/// </summary>
		public static Color Tan { get { return Color.FromRgb(210, 180, 140); } }

		/// <summary>
		/// Returns the color RosyBrown
		/// </summary>
		public static Color RosyBrown { get { return Color.FromRgb(188, 143, 143); } }

		/// <summary>
		/// Returns the color SandyBrown
		/// </summary>
		public static Color SandyBrown { get { return Color.FromRgb(244, 164, 96); } }

		/// <summary>
		/// Returns the color Goldenrod
		/// </summary>
		public static Color Goldenrod { get { return Color.FromRgb(218, 165, 32); } }

		/// <summary>
		/// Returns the color DarkGoldenrod
		/// </summary>
		public static Color DarkGoldenrod { get { return Color.FromRgb(184, 134, 11); } }

		/// <summary>
		/// Returns the color Peru
		/// </summary>
		public static Color Peru { get { return Color.FromRgb(205, 133, 63); } }

		/// <summary>
		/// Returns the color Chocolate
		/// </summary>
		public static Color Chocolate { get { return Color.FromRgb(210, 105, 30); } }

		/// <summary>
		/// Returns the color SaddleBrown
		/// </summary>
		public static Color SaddleBrown { get { return Color.FromRgb(139, 69, 19); } }

		/// <summary>
		/// Returns the color Sienna
		/// </summary>
		public static Color Sienna { get { return Color.FromRgb(160, 82, 45); } }

		/// <summary>
		/// Returns the color Brown
		/// </summary>
		public static Color Brown { get { return Color.FromRgb(165, 42, 42); } }

		/// <summary>
		/// Returns the color Maroon
		/// </summary>
		public static Color Maroon { get { return Color.FromRgb(128, 0, 0); } }

		/// <summary>
		/// Returns the color DarkOliveGreen
		/// </summary>
		public static Color DarkOliveGreen { get { return Color.FromRgb(85, 107, 47); } }

		/// <summary>
		/// Returns the color Olive
		/// </summary>
		public static Color Olive { get { return Color.FromRgb(128, 128, 0); } }

		/// <summary>
		/// Returns the color OliveDrab
		/// </summary>
		public static Color OliveDrab { get { return Color.FromRgb(107, 142, 35); } }

		/// <summary>
		/// Returns the color YellowGreen
		/// </summary>
		public static Color YellowGreen { get { return Color.FromRgb(154, 205, 50); } }

		/// <summary>
		/// Returns the color LimeGreen
		/// </summary>
		public static Color LimeGreen { get { return Color.FromRgb(50, 205, 50); } }

		/// <summary>
		/// Returns the color Lime
		/// </summary>
		public static Color Lime { get { return Color.FromRgb(0, 255, 0); } }

		/// <summary>
		/// Returns the color LawnGreen
		/// </summary>
		public static Color LawnGreen { get { return Color.FromRgb(124, 252, 0); } }

		/// <summary>
		/// Returns the color Chartreuse
		/// </summary>
		public static Color Chartreuse { get { return Color.FromRgb(127, 255, 0); } }

		/// <summary>
		/// Returns the color GreenYellow
		/// </summary>
		public static Color GreenYellow { get { return Color.FromRgb(173, 255, 47); } }

		/// <summary>
		/// Returns the color SpringGreen
		/// </summary>
		public static Color SpringGreen { get { return Color.FromRgb(0, 255, 127); } }

		/// <summary>
		/// Returns the color MediumSpringGreen
		/// </summary>
		public static Color MediumSpringGreen { get { return Color.FromRgb(0, 250, 154); } }

		/// <summary>
		/// Returns the color LightGreen
		/// </summary>
		public static Color LightGreen { get { return Color.FromRgb(144, 238, 144); } }

		/// <summary>
		/// Returns the color PaleGreen
		/// </summary>
		public static Color PaleGreen { get { return Color.FromRgb(152, 251, 152); } }

		/// <summary>
		/// Returns the color DarkSeaGreen
		/// </summary>
		public static Color DarkSeaGreen { get { return Color.FromRgb(143, 188, 143); } }

		/// <summary>
		/// Returns the color MediumSeaGreen
		/// </summary>
		public static Color MediumSeaGreen { get { return Color.FromRgb(60, 179, 113); } }

		/// <summary>
		/// Returns the color SeaGreen
		/// </summary>
		public static Color SeaGreen { get { return Color.FromRgb(46, 139, 87); } }

		/// <summary>
		/// Returns the color ForestGreen
		/// </summary>
		public static Color ForestGreen { get { return Color.FromRgb(34, 139, 34); } }

		/// <summary>
		/// Returns the color Green
		/// </summary>
		public static Color Green { get { return Color.FromRgb(0, 128, 0); } }

		/// <summary>
		/// Returns the color DarkGreen
		/// </summary>
		public static Color DarkGreen { get { return Color.FromRgb(0, 100, 0); } }

		/// <summary>
		/// Returns the color MediumAquamarine
		/// </summary>
		public static Color MediumAquamarine { get { return Color.FromRgb(102, 205, 170); } }

		/// <summary>
		/// Returns the color Aqua
		/// </summary>
		public static Color Aqua { get { return Color.FromRgb(0, 255, 255); } }

		/// <summary>
		/// Returns the color Cyan
		/// </summary>
		public static Color Cyan { get { return Color.FromRgb(0, 255, 255); } }

		/// <summary>
		/// Returns the color LightCyan
		/// </summary>
		public static Color LightCyan { get { return Color.FromRgb(224, 255, 255); } }

		/// <summary>
		/// Returns the color PaleTurquoise
		/// </summary>
		public static Color PaleTurquoise { get { return Color.FromRgb(175, 238, 238); } }

		/// <summary>
		/// Returns the color Aquamarine
		/// </summary>
		public static Color Aquamarine { get { return Color.FromRgb(127, 255, 212); } }

		/// <summary>
		/// Returns the color Turquoise
		/// </summary>
		public static Color Turquoise { get { return Color.FromRgb(64, 224, 208); } }

		/// <summary>
		/// Returns the color MediumTurquoise
		/// </summary>
		public static Color MediumTurquoise { get { return Color.FromRgb(72, 209, 204); } }

		/// <summary>
		/// Returns the color DarkTurquoise
		/// </summary>
		public static Color DarkTurquoise { get { return Color.FromRgb(0, 206, 209); } }

		/// <summary>
		/// Returns the color LightSeaGreen
		/// </summary>
		public static Color LightSeaGreen { get { return Color.FromRgb(32, 178, 170); } }

		/// <summary>
		/// Returns the color CadetBlue
		/// </summary>
		public static Color CadetBlue { get { return Color.FromRgb(95, 158, 160); } }

		/// <summary>
		/// Returns the color DarkCyan
		/// </summary>
		public static Color DarkCyan { get { return Color.FromRgb(0, 139, 139); } }

		/// <summary>
		/// Returns the color Teal
		/// </summary>
		public static Color Teal { get { return Color.FromRgb(0, 128, 128); } }

		/// <summary>
		/// Returns the color LightSteelBlue
		/// </summary>
		public static Color LightSteelBlue { get { return Color.FromRgb(176, 196, 222); } }

		/// <summary>
		/// Returns the color PowderBlue
		/// </summary>
		public static Color PowderBlue { get { return Color.FromRgb(176, 224, 230); } }

		/// <summary>
		/// Returns the color LightBlue
		/// </summary>
		public static Color LightBlue { get { return Color.FromRgb(173, 216, 230); } }

		/// <summary>
		/// Returns the color SkyBlue
		/// </summary>
		public static Color SkyBlue { get { return Color.FromRgb(135, 206, 235); } }

		/// <summary>
		/// Returns the color LightSkyBlue
		/// </summary>
		public static Color LightSkyBlue { get { return Color.FromRgb(135, 206, 250); } }

		/// <summary>
		/// Returns the color DeepSkyBlue
		/// </summary>
		public static Color DeepSkyBlue { get { return Color.FromRgb(0, 191, 255); } }

		/// <summary>
		/// Returns the color DodgerBlue
		/// </summary>
		public static Color DodgerBlue { get { return Color.FromRgb(30, 144, 255); } }

		/// <summary>
		/// Returns the color CornflowerBlue
		/// </summary>
		public static Color CornflowerBlue { get { return Color.FromRgb(100, 149, 237); } }

		/// <summary>
		/// Returns the color SteelBlue
		/// </summary>
		public static Color SteelBlue { get { return Color.FromRgb(70, 130, 180); } }

		/// <summary>
		/// Returns the color RoyalBlue
		/// </summary>
		public static Color RoyalBlue { get { return Color.FromRgb(65, 105, 225); } }

		/// <summary>
		/// Returns the color Blue
		/// </summary>
		public static Color Blue { get { return Color.FromRgb(0, 0, 255); } }

		/// <summary>
		/// Returns the color MediumBlue
		/// </summary>
		public static Color MediumBlue { get { return Color.FromRgb(0, 0, 205); } }

		/// <summary>
		/// Returns the color DarkBlue
		/// </summary>
		public static Color DarkBlue { get { return Color.FromRgb(0, 0, 139); } }

		/// <summary>
		/// Returns the color Navy
		/// </summary>
		public static Color Navy { get { return Color.FromRgb(0, 0, 128); } }

		/// <summary>
		/// Returns the color MidnightBlue
		/// </summary>
		public static Color MidnightBlue { get { return Color.FromRgb(25, 25, 112); } }

		/// <summary>
		/// Returns the color Lavender
		/// </summary>
		public static Color Lavender { get { return Color.FromRgb(230, 230, 250); } }

		/// <summary>
		/// Returns the color Thistle
		/// </summary>
		public static Color Thistle { get { return Color.FromRgb(216, 191, 216); } }

		/// <summary>
		/// Returns the color Plum
		/// </summary>
		public static Color Plum { get { return Color.FromRgb(221, 160, 221); } }

		/// <summary>
		/// Returns the color Violet
		/// </summary>
		public static Color Violet { get { return Color.FromRgb(238, 130, 238); } }

		/// <summary>
		/// Returns the color Orchid
		/// </summary>
		public static Color Orchid { get { return Color.FromRgb(218, 112, 214); } }

		/// <summary>
		/// Returns the color Fuchsia
		/// </summary>
		public static Color Fuchsia { get { return Color.FromRgb(255, 0, 255); } }

		/// <summary>
		/// Returns the color Magenta
		/// </summary>
		public static Color Magenta { get { return Color.FromRgb(255, 0, 255); } }

		/// <summary>
		/// Returns the color MediumOrchid
		/// </summary>
		public static Color MediumOrchid { get { return Color.FromRgb(186, 85, 211); } }

		/// <summary>
		/// Returns the color MediumPurple
		/// </summary>
		public static Color MediumPurple { get { return Color.FromRgb(147, 112, 219); } }

		/// <summary>
		/// Returns the color BlueViolet
		/// </summary>
		public static Color BlueViolet { get { return Color.FromRgb(138, 43, 226); } }

		/// <summary>
		/// Returns the color DarkViolet
		/// </summary>
		public static Color DarkViolet { get { return Color.FromRgb(148, 0, 211); } }

		/// <summary>
		/// Returns the color DarkOrchid
		/// </summary>
		public static Color DarkOrchid { get { return Color.FromRgb(153, 50, 204); } }

		/// <summary>
		/// Returns the color DarkMagenta
		/// </summary>
		public static Color DarkMagenta { get { return Color.FromRgb(139, 0, 139); } }

		/// <summary>
		/// Returns the color Purple
		/// </summary>
		public static Color Purple { get { return Color.FromRgb(128, 0, 128); } }

		/// <summary>
		/// Returns the color Indigo
		/// </summary>
		public static Color Indigo { get { return Color.FromRgb(75, 0, 130); } }

		/// <summary>
		/// Returns the color DarkSlateBlue
		/// </summary>
		public static Color DarkSlateBlue { get { return Color.FromRgb(72, 61, 139); } }

		/// <summary>
		/// Returns the color SlateBlue
		/// </summary>
		public static Color SlateBlue { get { return Color.FromRgb(106, 90, 205); } }

		/// <summary>
		/// Returns the color MediumSlateBlue
		/// </summary>
		public static Color MediumSlateBlue { get { return Color.FromRgb(123, 104, 238); } }

		/// <summary>
		/// Returns the color White
		/// </summary>
		public static Color White { get { return Color.FromRgb(255, 255, 255); } }

		/// <summary>
		/// Returns the color Snow
		/// </summary>
		public static Color Snow { get { return Color.FromRgb(255, 250, 250); } }

		/// <summary>
		/// Returns the color Honeydew
		/// </summary>
		public static Color Honeydew { get { return Color.FromRgb(240, 255, 240); } }

		/// <summary>
		/// Returns the color MintCream
		/// </summary>
		public static Color MintCream { get { return Color.FromRgb(245, 255, 250); } }

		/// <summary>
		/// Returns the color Azure
		/// </summary>
		public static Color Azure { get { return Color.FromRgb(240, 255, 255); } }

		/// <summary>
		/// Returns the color AliceBlue
		/// </summary>
		public static Color AliceBlue { get { return Color.FromRgb(240, 248, 255); } }

		/// <summary>
		/// Returns the color GhostWhite
		/// </summary>
		public static Color GhostWhite { get { return Color.FromRgb(248, 248, 255); } }

		/// <summary>
		/// Returns the color WhiteSmoke
		/// </summary>
		public static Color WhiteSmoke { get { return Color.FromRgb(245, 245, 245); } }

		/// <summary>
		/// Returns the color Seashell
		/// </summary>
		public static Color Seashell { get { return Color.FromRgb(255, 245, 238); } }

		/// <summary>
		/// Returns the color Beige
		/// </summary>
		public static Color Beige { get { return Color.FromRgb(245, 245, 220); } }

		/// <summary>
		/// Returns the color OldLace
		/// </summary>
		public static Color OldLace { get { return Color.FromRgb(253, 245, 230); } }

		/// <summary>
		/// Returns the color FloralWhite
		/// </summary>
		public static Color FloralWhite { get { return Color.FromRgb(255, 250, 240); } }

		/// <summary>
		/// Returns the color Ivory
		/// </summary>
		public static Color Ivory { get { return Color.FromRgb(255, 255, 240); } }

		/// <summary>
		/// Returns the color AntiqueWhite
		/// </summary>
		public static Color AntiqueWhite { get { return Color.FromRgb(250, 235, 215); } }

		/// <summary>
		/// Returns the color Linen
		/// </summary>
		public static Color Linen { get { return Color.FromRgb(250, 240, 230); } }

		/// <summary>
		/// Returns the color LavenderBlush
		/// </summary>
		public static Color LavenderBlush { get { return Color.FromRgb(255, 240, 245); } }

		/// <summary>
		/// Returns the color MistyRose
		/// </summary>
		public static Color MistyRose { get { return Color.FromRgb(255, 228, 225); } }

		/// <summary>
		/// Returns the color Gainsboro
		/// </summary>
		public static Color Gainsboro { get { return Color.FromRgb(220, 220, 220); } }

		/// <summary>
		/// Returns the color LightGray
		/// </summary>
		public static Color LightGray { get { return Color.FromRgb(211, 211, 211); } }

		/// <summary>
		/// Returns the color Silver
		/// </summary>
		public static Color Silver { get { return Color.FromRgb(192, 192, 192); } }

		/// <summary>
		/// Returns the color DarkGray
		/// </summary>
		public static Color DarkGray { get { return Color.FromRgb(169, 169, 169); } }

		/// <summary>
		/// Returns the color Gray
		/// </summary>
		public static Color Gray { get { return Color.FromRgb(128, 128, 128); } }

		/// <summary>
		/// Returns the color DimGray
		/// </summary>
		public static Color DimGray { get { return Color.FromRgb(105, 105, 105); } }

		/// <summary>
		/// Returns the color LightSlateGray
		/// </summary>
		public static Color LightSlateGray { get { return Color.FromRgb(119, 136, 153); } }

		/// <summary>
		/// Returns the color SlateGray
		/// </summary>
		public static Color SlateGray { get { return Color.FromRgb(112, 128, 144); } }

		/// <summary>
		/// Returns the color DarkSlateGray
		/// </summary>
		public static Color DarkSlateGray { get { return Color.FromRgb(47, 79, 79); } }

		/// <summary>
		/// Returns the color Black
		/// </summary>
		public static Color Black { get { return Color.FromRgb(0, 0, 0); } }
	}
}
