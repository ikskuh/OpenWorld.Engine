using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines a localization asset.
	/// </summary>
	[AssetExtension(".loc")]
	public class Localization : Asset
	{
		private readonly Dictionary<string, string> translations = new Dictionary<string, string>();

		/// <summary>
		/// Loads the localization definition.
		/// </summary>
		protected override void Load(AssetLoadContext context, System.IO.Stream stream, string extensionHint)
		{
			var culture = CultureInfo.CurrentCulture;
			Log.WriteLine("Loading localization for {0} - {1}", culture.ThreeLetterISOLanguageName, culture.EnglishName);

			XmlDocument doc = new XmlDocument();
			doc.Load(stream);

			this.translations.Clear();
			foreach(XmlElement localized in doc.GetElementsByTagName("localized"))
			{
				string id = localized.GetAttribute("id");
				if (string.IsNullOrWhiteSpace(id))
				{
					Log.WriteLine("Localization {0} missing an element id.", context.Name);
					continue;
				}
				XmlElement translation = localized[culture.ThreeLetterISOLanguageName];
				if(translation == null)
				{
					Log.WriteLine("Localization {0}.{1} is missing a translation. Using default translation.", context.Name, id);
					this.translations.Add(id, "<" + id + ">");
				}
				else
				{
					this.translations.Add(id, translation.InnerText);
				}
			}
			// TODO: Set properties
			foreach(var property in this.GetType().GetProperties())
			{
				if (!property.CanWrite)
					continue;
				var attribs = (IDAttribute[])property.GetCustomAttributes(typeof(IDAttribute), false);
				if (attribs.Length != 1)
					continue;

				property.SetValue(this, this[attribs[0].ID], new object[0]);
			}
		}

		/// <summary>
		/// Gets a translated text.
		/// </summary>
		/// <param name="id">The id of the text.</param>
		/// <returns>id translated to the text.</returns>
		public string this[string id]
		{
			get
			{
				if (this.translations.ContainsKey(id))
					return this.translations[id];
				else
					return "<" + id + ">";
			}
		}
	}
}
