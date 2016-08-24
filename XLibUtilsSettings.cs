/*******************************************************************************
 * XLibUtilsSettings.cs
 *
 * Copyright (c) Moreno Seri (moreno.seri@gmail.com)
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
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
