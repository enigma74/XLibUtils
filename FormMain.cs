/*******************************************************************************
 * FormMain.cs
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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace XLibUtils
{
	public partial class FormMain : Form
	{
		/// <summary>Custom format handler</summary>
		/// <param name="g"></param>
		/// <returns></returns>
		private delegate StringFormat CustomFormatHandler(Graphics g);

		/// <summary>Char info</summary>
		private class CharInfo
		{
			/// <summary>ASCII code</summary>
			public readonly byte ASCII;
			/// <summary>Char</summary>
			public readonly char Ch;
			/// <summary>Cell size</summary>
			public Size Size;
			/// <summary>Glyph data</summary>
			public List<byte> Data;

			// *********************************************************************

			/// <summary>Constructor</summary>
			/// <param name="ascii">ASCII code</param>
			/// <param name="ch">Char</param>
			public CharInfo(byte ascii, char ch)
			{
				ASCII = ascii;
				Ch = ch;
			}
		}

		/// <summary>Chars data</summary>
		private class CharsData
		{
			/// <summary>Max font size</summary>
			public Size Size;
			/// <summary>Space width</summary>
			public int SpaceWidth;
			/// <summary>Chars info</summary>
			public CharInfo[] CIS;
			/// <summary>Font info</summary>
			public byte[] FontInfo;
			/// <summary>Font size</summary>
			public int FontSize;
		}

		/// <summary>Source generation class</summary>
		private class SourceWriter : StreamWriter
		{
			/// <summary>Tabs</summary>
			private int m_t;

			// *********************************************************************

			/// <summary></summary>
			/// <param name="file"></param>
			public SourceWriter(string file)
				: base(file)
			{
				BeginHeader(Path.GetFileName(file));
			}

			// *********************************************************************

			/// <summary></summary>
			private string Tabs
			{
				get { return new string('\t', m_t); }
			}

			/// <summary></summary>
			/// <param name="fmt"></param>
			/// <param name="pars"></param>
			public void CodeLine(string fmt, params object[] pars)
			{
				string line = pars.Length > 0 ? string.Format(fmt, pars) : fmt;
				WriteLine("{0}{1}", Tabs, line);
			}

			/// <summary></summary>
			public void OpenCurly()
			{
				CodeLine("{");
				m_t++;
			}

			/// <summary></summary>
			/// <param name="semicolon"></param>
			public void CloseCurly(bool semicolon = false)
			{
				if (m_t > 0)
					m_t--;
				CodeLine("}}{0}", semicolon ? ";" : "");
			}

			///// <summary></summary>
			//public void Separator()
			//{
			//	WriteLine();
			//	CodeLine("// *********************************************************************");
			//	WriteLine();
			//}

			/// <summary></summary>
			/// <param name="file"></param>
			private void BeginHeader(string file)
			{
				WriteLine("/*******************************************************************************");
				WriteLine(" * {0}", file);
				WriteLine(" * Generated with XUI Utils (written by Moreno Seri (moreno.seri@gmail.com))");
			}

			/// <summary></summary>
			public void EndHeader()
			{
				WriteLine(" *******************************************************************************/");
				WriteLine();
			}

			/// <summary></summary>
			/// <param name="b"></param>
			/// <returns></returns>
			public string Hex(byte b)
			{
				return string.Format("0x{0}", b.ToString("X2"));
			}

			/// <summary></summary>
			/// <param name="data"></param>
			public void CodeData(IEnumerable<byte> data)
			{
				int n = 0;
				foreach (byte b in data)
				{
					Write("{0}{1},", n == 0 ? Tabs : " ", Hex(b));
					n++;
					if (n == 8)
					{
						WriteLine();
						n = 0;
					}
				}
				if (n > 0)
					WriteLine();
			}
		}

		/// <summary>Source builder class</summary>
		private class SourceBuilder : IDisposable
		{
			/// <summary>Disposed flag</summary>
			private bool m_disposed;

			// *********************************************************************

			/// <summary>Constructor</summary>
			/// <param name="folder"></param>
			/// <param name="symbol"></param>
			public SourceBuilder(string folder, string symbol)
			{
				if (!Directory.Exists(folder))
					throw new DirectoryNotFoundException("Plese select a valid output folder");
				bool ok = symbol.Length > 0;
				if (ok)
					foreach (char ch in symbol)
						if (!char.IsLetterOrDigit(ch))
						{
							ok = false;
							break;
						}
				if (!ok)
					throw new InvalidOperationException("Plese specify a valid symbol name");
				Symbol = symbol;
				Func<string, string> _File = _ext => Path.Combine(folder, string.Format("{0}.{1}", symbol, _ext));
				H = new SourceWriter(_File("h"));
				H.WriteLine(" *");
				CPP = new SourceWriter(_File("cpp"));
			}

			/// <summary>Destructor</summary>
			~SourceBuilder()
			{
				Dispose(false);
			}

			// *********************************************************************

			/// <summary></summary>
			/// <param name="disposing"></param>
			private void Dispose(bool disposing)
			{
				if (m_disposed)
					return;
				if (H != null)
					H.Dispose();
				if (CPP != null)
					CPP.Dispose();
				m_disposed = true;
			}

			/// <summary>Disposing</summary>
			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}

			/// <summary>Symbol</summary>
			public string Symbol { get; private set; }
			/// <summary>Header generator</summary>
			public readonly SourceWriter H;
			/// <summary>CPP generator</summary>
			public readonly SourceWriter CPP;

			/// <summary>Ends both files headers</summary>
			/// <param name="bpp"></param>
			/// <param name="memorysize"></param>
			public void EndHeaders(int bpp, int memorysize)
			{
				H.WriteLine(" * - Bits per pixel: {0}", bpp);
				H.WriteLine(" * - Memory size: {0}", memorysize > 1024
					? string.Format("{0} KB", ((float)memorysize / 1024).ToString("0.00"))
					: string.Format("{0} B", memorysize));
				H.EndHeader();
				H.WriteLine("#ifndef {0}_h", Symbol);
				H.WriteLine("#define {0}_h", Symbol);
				H.WriteLine();
				CPP.EndHeader();
				CPP.CodeLine("#include \"XGraph.h\"");
				CPP.CodeLine("#include \"{0}.h\"", Symbol);
				CPP.WriteLine();
				CPP.CodeLine("const PROGMEM uint8_t _{0}[] =", Symbol);
				CPP.OpenCurly();
			}

			/// <summary>Complete files</summary>
			public void CompleteFiles()
			{
				H.CodeLine("/// {0}", Symbol);
				H.CodeLine("extern const XFlashData {0};", Symbol);
				H.WriteLine();
				H.WriteLine("#endif");
				CPP.CloseCurly(true);
				CPP.WriteLine();
				CPP.CodeLine("const XFlashData {0}(_{0}, sizeof(_{0}));", Symbol);
			}

			/// <summary>Writes glyph code</summary>
			/// <param name="bpp"></param>
			/// <param name="memorysize"></param>
			/// <param name="data"></param>
			public void WriteGlyph(int bpp, int memorysize, IEnumerable<byte> data)
			{
				EndHeaders(bpp, memorysize);
				CPP.CodeLine("// flags, width, height, bitmap");
				CPP.CodeData(data);
				CompleteFiles();
			}
		}

		/// <summary>Custom label</summary>
		private class CustomLabel : Label
		{
			/// <summary></summary>
			public CustomFormatHandler CustomFormat;

			/// <summary></summary>
			/// <param name="e"></param>
			protected override void OnPaint(PaintEventArgs e)
			{
				Graphics g = e.Graphics;
				StringFormat sf = CustomFormat != null
					? CustomFormat(g)
					: StringFormat.GenericDefault;
				g.DrawString(Text, Font, new SolidBrush(ForeColor), ClientRectangle, sf);
			}
		}

		// *********************************************************************

		/// <summary>By code update</summary>
		private bool m_code = true;
		/// <summary>Letters</summary>
		private const string s_letters = "abcdefghijklmnopqrstuvwxyz";
		/// <summary>Numbers</summary>
		private const string s_numbers = "0123456789";
		/// <summary>ASCII</summary>
		private readonly string m_ascii;
		/// <summary>Extended ASCII</summary>
		private readonly byte[] m_ext;

		/// <summary>Settings</summary>
		private XLibUtilsSettings m_sts;
		/// <summary>Font</summary>
		private Font m_font;
		/// <summary>Font name</summary>
		private string m_fontname = "";
		/// <summary>Glyph flags</summary>
		private byte m_flags;

		/// <summary>Unicode codepage</summary>
		private readonly Encoding m_unicode = Encoding.Unicode;
		/// <summary>Codepage</summary>
		private Encoding m_cp;

		// *********************************************************************

		/// <summary>Constructor</summary>
		public FormMain()
		{
			InitializeComponent();
			// ASCII initialization
			m_ascii = "";
			for (char ch = (char)0x21; ch <= 0x7e; ch++)
				m_ascii += ch;
			// Extended ASCII initialization
			List<byte> ext = new List<byte>();
			for (int b = 0x80; b <= 0xff; b++)
				ext.Add((byte)b);
			m_ext = ext.ToArray();
		}

		// *********************************************************************

		/// <summary></summary>
		/// <param name="message"></param>
		/// <param name="info"></param>
		private void Alert(string message, bool info = false)
		{
			MessageBox.Show(this, message, Text, MessageBoxButtons.OK,
				info ? MessageBoxIcon.Information : MessageBoxIcon.Exclamation);
		}

		/// <summary></summary>
		private TypeConverter FontConverter
		{
			get { return TypeDescriptor.GetConverter(typeof(Font)); }
		}

		/// <summary></summary>
		private void UpdateCP()
		{
			try
			{
				int cp = int.Parse(cmbCP.Text);
				if (cp <= 0)
					throw new InvalidOperationException("Insert a valid code page");
				m_cp = Encoding.GetEncoding(cp);
				m_sts.Font.CodePage = m_cp.CodePage;
			}
			catch
			{
				cmbCP.Text = m_sts.Font.CodePage.ToString();
				throw;
			}
		}

		/// <summary></summary>
		private void UpdateSample()
		{
			int bpp = 0;
			int contrast = 0;
			switch (m_sts.Type)
			{
				case XLibType.Font:
					bpp = m_sts.Font.BPP;
					contrast = m_sts.Font.Contrast;
					break;
				case XLibType.CharsGlyphs:
					bpp = m_sts.CharsGlyphs.BPP;
					contrast = m_sts.CharsGlyphs.Contrast;
					break;
				case XLibType.ImageGlyph:
					bpp = m_sts.ImageGlyph.BPP;
					break;
			}
			lblBPPL.Text = string.Format("Bits per pixel: {0}", bpp);
			lblContrastL.Text = string.Format("Contrast: {0}", contrast);
			chkClearType.Enabled = (m_sts.Type != XLibType.ImageGlyph) && (bpp > 1);
			if (m_sts.Type != XLibType.ImageGlyph)
				lblChars.Refresh();
		}

		/// <summary></summary>
		/// <param name="type"></param>
		private void UpdateControls(bool type = false)
		{
			m_code = true;
			// update type by controls
			if (type)
				m_sts.Type = radFont.Checked
					? XLibType.Font
					: (radCharsGlyphs.Checked ? XLibType.CharsGlyphs : XLibType.ImageGlyph);
			// font update
			Action<string> _SetFont = _font =>
			{
				m_font = FontConverter.ConvertFromInvariantString(_font) as Font
					?? new Font("Courier New", 9.75f);
				lblChars.Font = txtChars.Font = (Font)m_font.Clone();
				m_fontname = m_font.Name;
				if (m_font.Bold)
					m_fontname += " Bold";
				if (m_font.Italic)
					m_fontname += " Italic";
				if (m_font.Underline)
					m_fontname += " Underline";
				if (m_font.Strikeout)
					m_fontname += " Strikeout";
				m_fontname += string.Format(", {0}pt", Math.Round(m_font.Size));
			};
			// updating controls
			bool font = false;
			bool glyphs = false;
			string chars = "";
			Func<int, int> _BPP = _bpp =>
			{
				if ((_bpp < 1) || (_bpp > 4))
					_bpp = 2;
				trkBPP.Value = _bpp;
				return _bpp;
			};
			Func<int, int> _Contrast = _contrast =>
			{
				if ((_contrast < 0) || (_contrast > 12))
					_contrast = 4;
				trkContrast.Value = _contrast;
				return _contrast;
			};
			bool cleartype = false;
			string symbol = "";
			switch (m_sts.Type)
			{
				case XLibType.Font:
					glyphs = font = true;
					if (!type)
						radFont.Checked = true;
					XLibFont fsts = m_sts.Font;
					_SetFont(fsts.Font);
					chars = fsts.Chars;
					fsts.BPP = _BPP(fsts.BPP);
					fsts.Contrast = _Contrast(fsts.Contrast);
					cleartype = fsts.ClearType;
					symbol = fsts.Symbol;
					break;
				case XLibType.CharsGlyphs:
					glyphs = true;
					if (!type)
						radCharsGlyphs.Checked = true;
					XLibCharsGlyphs cgsts = m_sts.CharsGlyphs;
					_SetFont(cgsts.Font);
					chars = cgsts.Chars;
					cgsts.BPP = _BPP(cgsts.BPP);
					cgsts.Contrast = _Contrast(cgsts.Contrast);
					cleartype = cgsts.ClearType;
					symbol = cgsts.Symbol;
					break;
				case XLibType.ImageGlyph:
					if (!type)
						radImageGlyph.Checked = true;
					XLibImageGlyph imsts = m_sts.ImageGlyph;
					imsts.BPP = _BPP(imsts.BPP);
					_Contrast(0);
					symbol = imsts.Symbol;
					break;
			}
			txtSource.Text = glyphs ? m_fontname : m_sts.ImageGlyph.Image;
			lblCharsL.Text = "Characters" + (font ? " (use <char=ascii> for custom)" : "");
			lblCharsL.Enabled = glyphs;
			txtChars.Text = chars;
			txtChars.Enabled = glyphs;
			btnLetters.Enabled = glyphs;
			btnDigits.Enabled = glyphs;
			btnASCII.Enabled = glyphs;
			btnClear.Enabled = glyphs;
			lblCPL.Enabled = font;
			cmbCP.Text = font ? m_sts.Font.CodePage.ToString() : "";
			cmbCP.Enabled = font;
			btnExtended.Enabled = font;
			lblContrastL.Enabled = glyphs;
			trkContrast.Enabled = glyphs;
			lblChars.ForeColor = glyphs ? Color.Black : SystemColors.GrayText;
			lblChars.BackColor = glyphs ? Color.White : SystemColors.Control;
			chkClearType.Checked = cleartype;
			//chkClearType.Enabled = glyphs;
			chkCentered.Checked = font && m_sts.Font.Centered;
			chkCentered.Enabled = font;
			UpdateSample();
			txtFolder.Text = font ? m_sts.FontsFolder : m_sts.GlyphsFolder;
			txtSymbol.Text = symbol;
			m_code = false;
		}

		// *********************************************************************

		/// <summary></summary>
		/// <param name="ch"></param>
		/// <returns></returns>
		private byte GetASCII(char ch)
		{
			byte[] b = Encoding.Convert(m_unicode, m_cp, m_unicode.GetBytes(new[] { ch }));
			return b.Length == 1 ? b[0] : (byte)0;
		}

		///// <summary></summary>
		///// <param name="ascii"></param>
		///// <returns></returns>
		//private char GetChar(byte ascii)
		//{
		//	string str = m_unicode.GetString(Encoding.Convert(m_cp, m_unicode, new[] { ascii }));
		//	return str.Length > 0 ? str[0] : '\0';
		//}

		/// <summary></summary>
		/// <param name="font"></param>
		/// <returns></returns>
		private CharInfo[] SortedChars(bool font)
		{
			string str = txtChars.Text;
			if (str.Length == 0)
				return null;
			if (font)
				UpdateCP();
			// custom chars [char=ascii] (for font)
			Hashtable m_ccs = new Hashtable();
			if (font)
			{
				string sp = @"<(?<customChar>.*?)=(?<customASCII>.*?)>";
				Regex regex = new Regex(sp, RegexOptions.Multiline);
				MatchCollection matches = regex.Matches(str);
				int removed = 0;
				foreach (Match match in matches)
				{
					string v = match.Groups["customChar"].Value;
					if (v.Length != 1)
						continue;
					char ch = v[0];
					v = match.Groups["customASCII"].Value;
					byte ascii;
					if (v.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
					{
						v = v.Substring(2, v.Length - 2);
						if (!byte.TryParse(v, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out ascii))
							continue;
					}
					else if (!byte.TryParse(v, out ascii))
						continue;
					str = str.Remove(match.Index - removed, match.Length);
					removed += match.Length;
					m_ccs.Add(ch, ascii);
				}
			}
			// create list of valid chars
			List<CharInfo> cis = new List<CharInfo>();
			foreach (char ch in str)
			{
				switch (ch)
				{
					case '\0':
					case ' ':
					case '\n':
					case '\r':
						continue;
				}
				byte ascii = 0;
				if (font)
				{
					ascii = m_ccs.ContainsKey(ch) ? (byte)m_ccs[ch] : GetASCII(ch);
					if ((ascii == 0) || (cis.Find(_ci => _ci.ASCII == ascii) != null))
						continue;
				}
				else if (cis.Find(_ci => _ci.Ch == ch) != null)
					continue;
				cis.Add(new CharInfo(ascii, ch));
			}
			int count = cis.Count;
			if (count == 0)
				return null;
			// sorts chars
			if (font)
				cis.Sort((_a, _b) => _a.ASCII.CompareTo(_b.ASCII));
			else
				cis.Sort((_a, _b) => _a.Ch.CompareTo(_b.Ch));
			return cis.ToArray();
		}

		/// <summary></summary>
		/// <param name="bitmap"></param>
		/// <param name="bpp"></param>
		/// <param name="cropped"></param>
		/// <param name="skipdata"></param>
		/// <returns></returns>
		private List<byte> CroppedGlyphData(Bitmap bitmap, int bpp, out Rectangle cropped, bool skipdata = false)
		{
			cropped = new Rectangle();
			int max = (1 << bpp) - 1;
			Func<Bitmap, int, int, int> _PixelAlpha = (_bitmap, _x, _y) =>
			{
				Color _c = _bitmap.GetPixel(_x, _y);
				int _g = (_c.R * 30 + _c.G * 59 + _c.B * 11) / 100;
				return max - (int)Math.Round((float)max * _g / 0xff);
			};
			// find glyph
			int w = bitmap.Width;
			int h = bitmap.Height;
			Func<int, bool> _YIsEmpty = _x =>
			{
				for (int _y = 0; _y < h; _y++)
					if (_PixelAlpha(bitmap, _x, _y) > 0)
						return false;
				return true;
			};
			int l;
			for (l = 0; l < w; l++)
				if (!_YIsEmpty(l))
					break;
			int r;
			for (r = 0; r < w; r++)
				if (!_YIsEmpty(w - r - 1))
					break;
			Func<int, bool> _XIsEmpty = _y =>
			{
				for (int _x = 0; _x < w; _x++)
					if (_PixelAlpha(bitmap, _x, _y) > 0)
						return false;
				return true;
			};
			int t;
			for (t = 0; t < h; t++)
				if (!_XIsEmpty(t))
					break;

			int b;
			for (b = 0; b < h; b++)
				if (!_XIsEmpty(h - b - 1))
					break;
			if (l >= w)
				return null;
			// crop glyph
			List<byte> data = new List<byte>();
			cropped = new Rectangle(l, t, w - l - r, h - t - b);
			if (skipdata)
				return data;
			using (Bitmap glyph = bitmap.Clone(cropped, bitmap.PixelFormat))
			{
				// glyph size
				data.Add((byte)cropped.Width);
				data.Add((byte)cropped.Height);
				// glyph bitmap
				uint bits = 0;
				int count = 0;
				for (int y = 0; y < cropped.Height; y++)
					for (int x = 0; x < cropped.Width; x++)
					{
						bits <<= bpp;
						bits |= (byte)_PixelAlpha(glyph, x, y);
						count += bpp;
						if (count >= 8)
						{
							int d = count - 8;
							data.Add((byte)(bits >> d));
							count = d;
						}
					}
				if (count > 0)
					data.Add((byte)(bits << (8 - count)));
			}
			return data;
		}

		/// <summary></summary>
		/// <param name="g"></param>
		private StringFormat TextFormat(Graphics g)
		{
			int bpp;
			bool cleartype;
			if (m_sts.Type == XLibType.Font)
			{
				bpp = m_sts.Font.BPP;
				cleartype = m_sts.Font.ClearType;
				g.TextContrast = m_sts.Font.Contrast;
			}
			else
			{
				bpp = m_sts.CharsGlyphs.BPP;
				cleartype = m_sts.CharsGlyphs.ClearType;
				g.TextContrast = m_sts.Font.Contrast;
			}
			g.TextRenderingHint = bpp == 1
				? TextRenderingHint.SingleBitPerPixelGridFit
				: (cleartype
					? TextRenderingHint.ClearTypeGridFit
					: TextRenderingHint.AntiAliasGridFit);
			StringFormat sf = new StringFormat();
			sf.Alignment = StringAlignment.Near;
			sf.LineAlignment = StringAlignment.Near;
			sf.FormatFlags = StringFormatFlags.FitBlackBox;
			return sf;
		}

		/// <summary></summary>
		/// <param name="font"></param>
		/// <returns></returns>
		private CharsData BuildCharsInfo(bool font)
		{
			// reading sorted chars
			CharInfo[] scis = SortedChars(font);
			if (scis == null)
				throw new InvalidOperationException("Please insert characters to use");
			// building data
			CharsData cd = new CharsData();
			List<CharInfo> cis = new List<CharInfo>();
			List<byte> info = new List<byte>();
			Rectangle cell = new Rectangle(new Point(), TextRenderer.MeasureText("W", m_font));
			using (Bitmap bitmap = new Bitmap(cell.Width, cell.Height))
			using (Graphics g = Graphics.FromImage(bitmap))
			{
				int bpp = font ? m_sts.Font.BPP : m_sts.CharsGlyphs.BPP;
				StringFormat sf = TextFormat(g);
				int top = int.MaxValue;
				int bottom = 0;
				int fontsize = 0;
				byte prev = 0x00;
				foreach (CharInfo ci in scis)
				{
					// drawing char
					string ch = ci.Ch.ToString();
					g.Clear(Color.White);
					g.DrawString(ch, m_font, Brushes.Black, cell, sf);
					// building data
					Rectangle cropped;
					ci.Data = CroppedGlyphData(bitmap, bpp, out cropped);
					if (ci.Data == null)
						continue;
					cis.Add(ci);
					// glyph metrics
					ci.Size.Width = cropped.Width;
					ci.Size.Height = cropped.Height;
					if (!font)
					{
						// inserting flags
						ci.Data.Insert(0, m_flags);
						continue;
					}
					// char and missing intermediate chars index
					if (prev != 0x00)
						for (int b = prev + 1; b < ci.ASCII; b++)
						{
							info.Add(0x00);
							info.Add(0x00);
						}
					prev = ci.ASCII;
					info.Add((byte)(fontsize & 0xff));
					info.Add((byte)((fontsize >> 8) & 0xff));
					// finding correct metrics
					Point offset;
					using (Bitmap ob = new Bitmap(cell.Width, cell.Height))
					using (Graphics og = Graphics.FromImage(ob))
					{
						// offset
						og.Clear(Color.White);
						og.TextRenderingHint = (bpp > 1) && m_sts.Font.ClearType
							? TextRenderingHint.ClearTypeGridFit
							: TextRenderingHint.AntiAliasGridFit;
						TextRenderer.DrawText(og, ch, m_font, cell, Color.Black, Color.White,
							TextFormatFlags.NoPadding);
						Rectangle oc;
						CroppedGlyphData(ob, bpp, out oc, true);
						offset = oc.Location;
						// width
						ci.Size.Width = TextRenderer.MeasureText(og, ch, m_font, cell.Size,
							TextFormatFlags.NoPadding).Width;
						// space width
						if (cd.SpaceWidth == 0)
						{
							cd.SpaceWidth = TextRenderer.MeasureText(og, " ", m_font, cell.Size,
								TextFormatFlags.NoPadding).Width;
							cd.Size.Width = cd.SpaceWidth;
						}
					}
					if (ci.Size.Width > cd.Size.Width)
						cd.Size.Width = ci.Size.Width;
					// inserting width and offset
					ci.Data.InsertRange(0, new[]
					{
						(byte)ci.Size.Width,
						(byte)(m_sts.Font.Centered ? (ci.Size.Width - cropped.Width) / 2 : offset.X),
						(byte)cropped.Y
					});
					fontsize += ci.Data.Count;
					// vertical metrics
					if (cropped.Y < top)
						top = cropped.Y;
					int cb = cropped.Y + cropped.Height - 1;
					if (cb > bottom)
						bottom = cb;
				}
				if (cis.Count == 0)
					throw new InvalidOperationException("No valid character found");
				if (font)
				{
					// fixing y offsets
					foreach (CharInfo ci in cis)
						ci.Data[2] -= (byte)top;
					cd.Size.Height = bottom - top + 1;
					// completing font info
					info.InsertRange(0, new[]
					{
						m_flags, (byte)cd.Size.Width, (byte)cd.Size.Height,
						(byte)cd.SpaceWidth, cis[0].ASCII, cis[cis.Count - 1].ASCII
					});
					cd.FontInfo = info.ToArray();
					// font size
					cd.FontSize = cd.FontInfo.Length + fontsize;
				}
				cd.CIS = cis.ToArray();
			}
			return cd;
		}

		// *********************************************************************

		/// <summary></summary>
		/// <param name="cd"></param>
		private void BuildFont(CharsData cd)
		{
			string folder = m_sts.FontsFolder;
			XLibFont sts = m_sts.Font;
			string symbol = sts.Symbol;
			using (SourceBuilder sb = new SourceBuilder(folder, symbol))
			{
				sb.H.WriteLine(" * - Font: {0}{1}{2}", m_fontname,
					(sts.BPP > 1) && sts.ClearType ? ", clear type" : "",
					sts.BPP > 1 ? string.Format(", contrast {0}", sts.Contrast) : "");
				sb.H.WriteLine(" * - Max size: {0}x{1}", cd.Size.Width, cd.Size.Height);
				sb.H.WriteLine(" * - Space width: {0}", cd.SpaceWidth);
				sb.H.WriteLine(" * - Char range: {0} - {1}",
					sb.H.Hex(cd.CIS[0].ASCII), sb.H.Hex(cd.CIS[cd.CIS.Length - 1].ASCII));
				sb.EndHeaders(sts.BPP, cd.FontSize);
				sb.CPP.CodeLine("// flags, max width, height, space width, start ascii, end ascii, indexes");
				sb.CPP.CodeData(cd.FontInfo);
				sb.CPP.CodeLine("// cell width, x offset, y offset, width, height, bitmap");
				foreach (CharInfo ci in cd.CIS)
				{
					sb.CPP.CodeLine("// {0}{1}",
						sb.CPP.Hex(ci.ASCII),
						ci.Ch != 127 ? string.Format(" '{0}'", ci.Ch) : "");
					sb.CPP.CodeData(ci.Data);
				}
				sb.CompleteFiles();
			}
		}

		/// <summary></summary>
		/// <param name="cd"></param>
		private void BuildCharsGliphs(CharsData cd)
		{
			string folder = m_sts.GlyphsFolder;
			XLibCharsGlyphs sts = m_sts.CharsGlyphs;
			foreach (CharInfo ci in cd.CIS)
			{
				string unicode = ((int)ci.Ch).ToString("X4");
				using (SourceBuilder sb = new SourceBuilder(folder, string.Format("{0}U{1}", sts.Symbol, unicode)))
				{
					sb.H.WriteLine(" * - Font: {0}{1}{2}", m_fontname,
						(sts.BPP > 1) && sts.ClearType ? ", clear type" : "",
						sts.BPP > 1 ? string.Format(", contrast {0}", sts.Contrast) : "");
					sb.H.WriteLine(" * - Glyph: (U+{0})", unicode);
					sb.H.WriteLine(" * - Size: {0}x{1}", ci.Size.Width, ci.Size.Height);
					sb.WriteGlyph(sts.BPP, ci.Data.Count, ci.Data);
				}
			}
		}

		/// <summary></summary>
		private void BuildImageGlyph()
		{
			string folder = m_sts.GlyphsFolder;
			XLibImageGlyph sts = m_sts.ImageGlyph;
			if (!File.Exists(sts.Image))
				throw new FileNotFoundException("Plese select a source glyph image");
			// bitmap data
			List<byte> data;
			Rectangle cropped;
			using (Bitmap bitmap = new Bitmap(sts.Image, true))
			{
				data = CroppedGlyphData(bitmap, sts.BPP, out cropped);
				if (data == null)
					throw new FormatException("The glyph image is empty");
			}
			data.Insert(0, m_flags);
			// creating source
			using (SourceBuilder sb = new SourceBuilder(folder, sts.Symbol))
			{
				sb.H.WriteLine(" * - Image: {0}", sts.Image);
				sb.H.WriteLine(" * - Size: {0}x{1}", cropped.Width, cropped.Height);
				sb.WriteGlyph(sts.BPP, data.Count, data);
			}
		}

		/// <summary></summary>
		private void Build()
		{
			try
			{
				Cursor = Cursors.WaitCursor;
				bool font = false;
				bool chars = false;
				int bpp = 0;
				switch (m_sts.Type)
				{
					case XLibType.Font:
						chars = font = true;
						bpp = m_sts.Font.BPP;
						break;
					case XLibType.CharsGlyphs:
						chars = true;
						bpp = m_sts.CharsGlyphs.BPP;
						break;
					case XLibType.ImageGlyph:
						bpp = m_sts.ImageGlyph.BPP;
						break;
				}
				// flags: only bpp at the moment
				m_flags = (byte)(bpp - 1);
				if (chars)
				{
					CharsData cd = BuildCharsInfo(font);
					if (font)
						BuildFont(cd);
					else
						BuildCharsGliphs(cd);
				}
				else
					BuildImageGlyph();
			}
			catch (Exception ex)
			{
				Cursor = Cursors.Default;
				Alert(ex.Message);
			}
			finally
			{
				Cursor = Cursors.Default;
			}
		}

		// *********************************************************************

		/// <summary></summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FormMain_Load(object sender, EventArgs e)
		{
			try
			{
				cmbCP.Items.Add("1252");
				cmbCP.Items.Add("437");
				trkBPP.Minimum = 1;
				trkBPP.Maximum = 4;
				trkBPP.SmallChange = trkBPP.LargeChange = 1;
				trkContrast.Minimum = 0;
				trkContrast.Maximum = 12;
				trkContrast.SmallChange = trkContrast.LargeChange = 1;
				lblChars.CustomFormat = TextFormat;
				m_sts = XLibUtilsSettings.Load();
				UpdateControls();
			}
			catch (Exception ex)
			{
				Alert(ex.Message);
				Close();
			}
		}

		/// <summary></summary>
		/// <param name="e"></param>
		protected override void OnClosing(CancelEventArgs e)
		{
			try
			{
				if (m_sts != null)
					m_sts.Save();
			}
			catch (Exception ex)
			{
				Alert(ex.Message);
			}
			base.OnClosing(e);
		}

		// *********************************************************************

		/// <summary></summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radFont_CheckedChanged(object sender, EventArgs e)
		{
			if (!m_code)
				UpdateControls(true);
		}

		/// <summary></summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radCharsGlyphs_CheckedChanged(object sender, EventArgs e)
		{
			if (!m_code)
				UpdateControls(true);
		}

		/// <summary></summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radImageGlyph_CheckedChanged(object sender, EventArgs e)
		{
			if (!m_code)
				UpdateControls(true);
		}

		/// <summary></summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSource_Click(object sender, EventArgs e)
		{
			try
			{
				if (m_sts.Type != XLibType.ImageGlyph)
				{
					using (FontDialog fd = new FontDialog())
					{
						m_code = true;
						fd.Font = m_font;
						if (fd.ShowDialog(this) != DialogResult.OK)
							return;
						if (m_sts.Type == XLibType.Font)
							m_sts.Font.Font = FontConverter.ConvertToInvariantString(fd.Font);
						else
							m_sts.CharsGlyphs.Font = FontConverter.ConvertToInvariantString(fd.Font);
					}
				}
				else
				{
					using (OpenFileDialog ofd = new OpenFileDialog())
					{
						m_code = true;
						ofd.Filter = "Image files (*.png; *.gif; *.bmp; *.jpg; *.jpeg)|*.png;*.gif;*.bmp;*.jpg;*.jpeg";
						ofd.FileName = m_sts.ImageGlyph.Image;
						if (ofd.ShowDialog(this) != DialogResult.OK)
							return;
						m_sts.ImageGlyph.Image = ofd.FileName;
					}
				}
				UpdateControls();
			}
			catch (Exception ex)
			{
				Alert(ex.Message);
			}
			finally
			{
				m_code = false;
			}
		}

		/// <summary></summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtChars_TextChanged(object sender, EventArgs e)
		{
			lblChars.Text = txtChars.Text;
			if (m_code)
				return;
			if (m_sts.Type == XLibType.Font)
				m_sts.Font.Chars = txtChars.Text;
			else
				m_sts.CharsGlyphs.Chars = txtChars.Text;
		}

		/// <summary></summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnLetters_Click(object sender, EventArgs e)
		{
			txtChars.Text += s_letters.ToUpper() + s_letters.ToLower();
		}

		/// <summary></summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDigits_Click(object sender, EventArgs e)
		{
			txtChars.Text += s_numbers;
		}

		/// <summary></summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnASCII_Click(object sender, EventArgs e)
		{
			txtChars.Text += m_ascii;
		}

		/// <summary></summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnClear_Click(object sender, EventArgs e)
		{
			txtChars.Text = "";
		}

		/// <summary></summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnExtended_Click(object sender, EventArgs e)
		{
			try
			{
				UpdateCP();
				txtChars.Text += m_unicode.GetString(Encoding.Convert(m_cp, m_unicode, m_ext));
			}
			catch (Exception ex)
			{
				Alert(ex.Message);
			}
		}

		/// <summary></summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void trkBPP_ValueChanged(object sender, EventArgs e)
		{
			if (m_code)
				return;
			switch (m_sts.Type)
			{
				case XLibType.Font:
					m_sts.Font.BPP = trkBPP.Value;
					break;
				case XLibType.CharsGlyphs:
					m_sts.CharsGlyphs.BPP = trkBPP.Value;
					break;
				case XLibType.ImageGlyph:
					m_sts.ImageGlyph.BPP = trkBPP.Value;
					break;
			}
			UpdateSample();
		}

		/// <summary></summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void trkContrast_ValueChanged(object sender, EventArgs e)
		{
			if (m_code)
				return;
			if (m_sts.Type == XLibType.Font)
				m_sts.Font.Contrast = trkContrast.Value;
			else
				m_sts.CharsGlyphs.Contrast = trkContrast.Value;
			UpdateSample();
		}

		/// <summary></summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void chkClearType_CheckedChanged(object sender, EventArgs e)
		{
			if (m_code)
				return;
			if (m_sts.Type == XLibType.Font)
				m_sts.Font.ClearType = chkClearType.Checked;
			else
				m_sts.CharsGlyphs.ClearType = chkClearType.Checked;
			UpdateSample();
		}

		/// <summary></summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void chkCentered_CheckedChanged(object sender, EventArgs e)
		{
			if (m_code)
				return;
			m_sts.Font.Centered = chkCentered.Checked;
		}

		/// <summary></summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnFolder_Click(object sender, EventArgs e)
		{
			try
			{
				using (FolderBrowserDialog fbd = new FolderBrowserDialog())
				{
					fbd.SelectedPath = txtFolder.Text;
					if (fbd.ShowDialog(this) != DialogResult.OK)
						return;
					txtFolder.Text = fbd.SelectedPath;
					if (m_sts.Type == XLibType.Font)
						m_sts.FontsFolder = fbd.SelectedPath;
					else
						m_sts.GlyphsFolder = fbd.SelectedPath;
				}
			}
			catch (Exception ex)
			{
				Alert(ex.Message);
			}
		}

		/// <summary></summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtSymbol_TextChanged(object sender, EventArgs e)
		{
			if (m_code)
				return;
			string symbol = txtSymbol.Text.Trim();
			switch (m_sts.Type)
			{
				case XLibType.Font:
					m_sts.Font.Symbol = symbol;
					break;
				case XLibType.CharsGlyphs:
					m_sts.CharsGlyphs.Symbol = symbol;
					break;
				case XLibType.ImageGlyph:
					m_sts.ImageGlyph.Symbol = symbol;
					break;
			}
		}

		/// <summary></summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnBuild_Click(object sender, EventArgs e)
		{
			Build();
		}
	}
}
