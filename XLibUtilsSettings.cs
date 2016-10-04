/*******************************************************************************
 * XLibUtilsSettings.cs
 *
 * Copyright (c) Moreno Seri (moreno.seri@gmail.com)
 *
 * This file is part of XLib.
 *
 * XLib is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * XLib is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *******************************************************************************/

using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace XLibUtils
{
	/// <summary>Configuration type</summary>
	public enum XLibType
	{
		Font,
		CharsGlyphs,
		ImageGlyph
	}

	/// <summary>Font params</summary>
	[Serializable]
	public class XLibFont
	{
		public string Font = "Courier New, 9.75pt";
		public string Chars = "";
		public int CodePage = 1252;
		public int BPP = 2;
		public int Contrast = 4;
		public bool ClearType = true;
		public bool Centered = false;
		public string Symbol = "";
	}

	/// <summary>Chars glyphs params</summary>
	[Serializable]
	public class XLibCharsGlyphs
	{
		public string Font = "Courier New, 9.75pt";
		public string Chars = "";
		public int BPP = 2;
		public int Contrast = 4;
		public bool ClearType = true;
		public string Symbol = "";
	}

	/// <summary>Image glyph params</summary>
	[Serializable]
	public class XLibImageGlyph
	{
		public string Image = "";
		public int BPP = 2;
		public string Symbol = "";
	}

	/// <summary>Configuration params</summary>
	[Serializable]
	public class XLibUtilsSettings
	{
		public XLibType Type = XLibType.Font;
		public XLibFont Font = new XLibFont();
		public XLibCharsGlyphs CharsGlyphs = new XLibCharsGlyphs();
		public XLibImageGlyph ImageGlyph = new XLibImageGlyph();
		public string FontsFolder = "";
		public string GlyphsFolder = "";

		// *********************************************************************

		/// <summary>Configuration path</summary>
		private static string ConfigPath
		{
			get { return Path.ChangeExtension(Application.ExecutablePath, "config"); }
		}

		/// <summary>Load settings</summary>
		/// <returns>Settings</returns>
		public static XLibUtilsSettings Load()
		{
			string path = ConfigPath;
			bool exists = File.Exists(path);
			using (FileStream fs = new FileStream(path, exists ? FileMode.Open : FileMode.Create))
			{
				XmlSerializer xs = new XmlSerializer(typeof(XLibUtilsSettings));
				XLibUtilsSettings settings = exists
					? (XLibUtilsSettings)xs.Deserialize(fs)
					: new XLibUtilsSettings();
				if (!exists)
					xs.Serialize(fs, settings);
				return settings;
			}
		}

		/// <summary>Save settings</summary>
		public void Save()
		{
			using (FileStream fs = new FileStream(ConfigPath, FileMode.Create))
			{
				XmlSerializer xs = new XmlSerializer(typeof(XLibUtilsSettings));
				xs.Serialize(fs, this);
			}
		}
	}
}
