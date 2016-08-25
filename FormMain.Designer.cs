using System.Windows.Forms;

namespace XLibUtils
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.radFont = new System.Windows.Forms.RadioButton();
			this.radCharsGlyphs = new System.Windows.Forms.RadioButton();
			this.radImageGlyph = new System.Windows.Forms.RadioButton();
			this.txtSource = new System.Windows.Forms.TextBox();
			this.btnSource = new System.Windows.Forms.Button();
			this.line1 = new System.Windows.Forms.PictureBox();
			this.lblCharsL = new System.Windows.Forms.Label();
			this.txtChars = new System.Windows.Forms.TextBox();
			this.btnLetters = new System.Windows.Forms.Button();
			this.btnDigits = new System.Windows.Forms.Button();
			this.btnASCII = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.lblCPL = new System.Windows.Forms.Label();
			this.cmbCP = new System.Windows.Forms.ComboBox();
			this.btnExtended = new System.Windows.Forms.Button();
			this.line2 = new System.Windows.Forms.PictureBox();
			this.lblBPPL = new System.Windows.Forms.Label();
			this.trkBPP = new System.Windows.Forms.TrackBar();
			this.lblContrastL = new System.Windows.Forms.Label();
			this.trkContrast = new System.Windows.Forms.TrackBar();
			this.lblChars = new XLibUtils.FormMain.CustomLabel();
			this.chkClearType = new System.Windows.Forms.CheckBox();
			this.chkCentered = new System.Windows.Forms.CheckBox();
			this.line3 = new System.Windows.Forms.PictureBox();
			this.lblFolderL = new System.Windows.Forms.Label();
			this.txtFolder = new System.Windows.Forms.TextBox();
			this.btnFolder = new System.Windows.Forms.Button();
			this.lblSymbolL = new System.Windows.Forms.Label();
			this.txtSymbol = new System.Windows.Forms.TextBox();
			this.btnBuild = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.line1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.line2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkBPP)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkContrast)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.line3)).BeginInit();
			this.SuspendLayout();
			// 
			// radFont
			// 
			this.radFont.AutoSize = true;
			this.radFont.Location = new System.Drawing.Point(6, 6);
			this.radFont.Name = "radFont";
			this.radFont.Size = new System.Drawing.Size(46, 17);
			this.radFont.TabIndex = 0;
			this.radFont.TabStop = true;
			this.radFont.Text = "Font";
			this.radFont.UseVisualStyleBackColor = true;
			this.radFont.CheckedChanged += new System.EventHandler(this.radFont_CheckedChanged);
			// 
			// radCharsGlyphs
			// 
			this.radCharsGlyphs.AutoSize = true;
			this.radCharsGlyphs.Location = new System.Drawing.Point(96, 6);
			this.radCharsGlyphs.Name = "radCharsGlyphs";
			this.radCharsGlyphs.Size = new System.Drawing.Size(85, 17);
			this.radCharsGlyphs.TabIndex = 1;
			this.radCharsGlyphs.TabStop = true;
			this.radCharsGlyphs.Text = "Chars glyphs";
			this.radCharsGlyphs.UseVisualStyleBackColor = true;
			this.radCharsGlyphs.CheckedChanged += new System.EventHandler(this.radCharsGlyphs_CheckedChanged);
			// 
			// radImageGlyph
			// 
			this.radImageGlyph.AutoSize = true;
			this.radImageGlyph.Location = new System.Drawing.Point(186, 6);
			this.radImageGlyph.Name = "radImageGlyph";
			this.radImageGlyph.Size = new System.Drawing.Size(82, 17);
			this.radImageGlyph.TabIndex = 2;
			this.radImageGlyph.TabStop = true;
			this.radImageGlyph.Text = "Image glyph";
			this.radImageGlyph.UseVisualStyleBackColor = true;
			this.radImageGlyph.CheckedChanged += new System.EventHandler(this.radImageGlyph_CheckedChanged);
			// 
			// txtSource
			// 
			this.txtSource.Location = new System.Drawing.Point(6, 24);
			this.txtSource.Name = "txtSource";
			this.txtSource.ReadOnly = true;
			this.txtSource.Size = new System.Drawing.Size(324, 20);
			this.txtSource.TabIndex = 3;
			this.txtSource.WordWrap = false;
			// 
			// btnSource
			// 
			this.btnSource.FlatAppearance.BorderSize = 0;
			this.btnSource.Location = new System.Drawing.Point(336, 24);
			this.btnSource.Name = "btnSource";
			this.btnSource.Size = new System.Drawing.Size(24, 21);
			this.btnSource.TabIndex = 4;
			this.btnSource.Text = "...";
			this.btnSource.UseVisualStyleBackColor = true;
			this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
			// 
			// line1
			// 
			this.line1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.line1.BackColor = System.Drawing.Color.Maroon;
			this.line1.Location = new System.Drawing.Point(0, 54);
			this.line1.Name = "line1";
			this.line1.Size = new System.Drawing.Size(366, 1);
			this.line1.TabIndex = 31;
			this.line1.TabStop = false;
			// 
			// lblCharsL
			// 
			this.lblCharsL.AutoSize = true;
			this.lblCharsL.Location = new System.Drawing.Point(6, 60);
			this.lblCharsL.Name = "lblCharsL";
			this.lblCharsL.Size = new System.Drawing.Size(199, 13);
			this.lblCharsL.TabIndex = 5;
			this.lblCharsL.Text = "Characters (use <char=ascii> for custom)";
			// 
			// txtChars
			// 
			this.txtChars.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtChars.Location = new System.Drawing.Point(6, 78);
			this.txtChars.Multiline = true;
			this.txtChars.Name = "txtChars";
			this.txtChars.Size = new System.Drawing.Size(354, 104);
			this.txtChars.TabIndex = 6;
			this.txtChars.TextChanged += new System.EventHandler(this.txtChars_TextChanged);
			// 
			// btnLetters
			// 
			this.btnLetters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnLetters.Location = new System.Drawing.Point(6, 186);
			this.btnLetters.Name = "btnLetters";
			this.btnLetters.Size = new System.Drawing.Size(84, 24);
			this.btnLetters.TabIndex = 7;
			this.btnLetters.Text = "Letters";
			this.btnLetters.UseVisualStyleBackColor = true;
			this.btnLetters.Click += new System.EventHandler(this.btnLetters_Click);
			// 
			// btnDigits
			// 
			this.btnDigits.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDigits.Location = new System.Drawing.Point(96, 186);
			this.btnDigits.Name = "btnDigits";
			this.btnDigits.Size = new System.Drawing.Size(84, 24);
			this.btnDigits.TabIndex = 8;
			this.btnDigits.Text = "Digits";
			this.btnDigits.UseVisualStyleBackColor = true;
			this.btnDigits.Click += new System.EventHandler(this.btnDigits_Click);
			// 
			// btnASCII
			// 
			this.btnASCII.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnASCII.Location = new System.Drawing.Point(186, 186);
			this.btnASCII.Name = "btnASCII";
			this.btnASCII.Size = new System.Drawing.Size(84, 24);
			this.btnASCII.TabIndex = 9;
			this.btnASCII.Text = "ASCII";
			this.btnASCII.UseVisualStyleBackColor = true;
			this.btnASCII.Click += new System.EventHandler(this.btnASCII_Click);
			// 
			// btnClear
			// 
			this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnClear.Location = new System.Drawing.Point(276, 186);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(84, 24);
			this.btnClear.TabIndex = 10;
			this.btnClear.Text = "Clear";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// lblCPL
			// 
			this.lblCPL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblCPL.AutoSize = true;
			this.lblCPL.Location = new System.Drawing.Point(6, 216);
			this.lblCPL.Name = "lblCPL";
			this.lblCPL.Size = new System.Drawing.Size(136, 13);
			this.lblCPL.TabIndex = 11;
			this.lblCPL.Text = "Extended ASCII code page";
			// 
			// cmbCP
			// 
			this.cmbCP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmbCP.FormattingEnabled = true;
			this.cmbCP.Location = new System.Drawing.Point(6, 234);
			this.cmbCP.Name = "cmbCP";
			this.cmbCP.Size = new System.Drawing.Size(174, 21);
			this.cmbCP.TabIndex = 12;
			// 
			// btnExtended
			// 
			this.btnExtended.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnExtended.Location = new System.Drawing.Point(186, 228);
			this.btnExtended.Name = "btnExtended";
			this.btnExtended.Size = new System.Drawing.Size(84, 24);
			this.btnExtended.TabIndex = 13;
			this.btnExtended.Text = "Extended";
			this.btnExtended.UseVisualStyleBackColor = true;
			this.btnExtended.Click += new System.EventHandler(this.btnExtended_Click);
			// 
			// line2
			// 
			this.line2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.line2.BackColor = System.Drawing.Color.Maroon;
			this.line2.Location = new System.Drawing.Point(0, 264);
			this.line2.Name = "line2";
			this.line2.Size = new System.Drawing.Size(366, 1);
			this.line2.TabIndex = 32;
			this.line2.TabStop = false;
			// 
			// lblBPPL
			// 
			this.lblBPPL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblBPPL.AutoSize = true;
			this.lblBPPL.Location = new System.Drawing.Point(6, 276);
			this.lblBPPL.Name = "lblBPPL";
			this.lblBPPL.Size = new System.Drawing.Size(66, 13);
			this.lblBPPL.TabIndex = 14;
			this.lblBPPL.Text = "Bits per pixel";
			// 
			// trkBPP
			// 
			this.trkBPP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.trkBPP.AutoSize = false;
			this.trkBPP.Location = new System.Drawing.Point(6, 294);
			this.trkBPP.Name = "trkBPP";
			this.trkBPP.Size = new System.Drawing.Size(174, 36);
			this.trkBPP.TabIndex = 15;
			this.trkBPP.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.trkBPP.ValueChanged += new System.EventHandler(this.trkBPP_ValueChanged);
			// 
			// lblContrastL
			// 
			this.lblContrastL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblContrastL.AutoSize = true;
			this.lblContrastL.Location = new System.Drawing.Point(186, 276);
			this.lblContrastL.Name = "lblContrastL";
			this.lblContrastL.Size = new System.Drawing.Size(46, 13);
			this.lblContrastL.TabIndex = 16;
			this.lblContrastL.Text = "Contrast";
			// 
			// trkContrast
			// 
			this.trkContrast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.trkContrast.AutoSize = false;
			this.trkContrast.Location = new System.Drawing.Point(186, 294);
			this.trkContrast.Name = "trkContrast";
			this.trkContrast.Size = new System.Drawing.Size(174, 36);
			this.trkContrast.TabIndex = 17;
			this.trkContrast.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.trkContrast.ValueChanged += new System.EventHandler(this.trkContrast_ValueChanged);
			// 
			// lblChars
			// 
			this.lblChars.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblChars.BackColor = System.Drawing.SystemColors.Window;
			this.lblChars.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblChars.Location = new System.Drawing.Point(6, 336);
			this.lblChars.Name = "lblChars";
			this.lblChars.Size = new System.Drawing.Size(354, 84);
			this.lblChars.TabIndex = 18;
			// 
			// chkClearType
			// 
			this.chkClearType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chkClearType.AutoSize = true;
			this.chkClearType.Location = new System.Drawing.Point(6, 426);
			this.chkClearType.Name = "chkClearType";
			this.chkClearType.Size = new System.Drawing.Size(73, 17);
			this.chkClearType.TabIndex = 19;
			this.chkClearType.Text = "Clear type";
			this.chkClearType.UseVisualStyleBackColor = true;
			this.chkClearType.CheckedChanged += new System.EventHandler(this.chkClearType_CheckedChanged);
			// 
			// chkCentered
			// 
			this.chkCentered.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chkCentered.AutoSize = true;
			this.chkCentered.Location = new System.Drawing.Point(96, 426);
			this.chkCentered.Name = "chkCentered";
			this.chkCentered.Size = new System.Drawing.Size(102, 17);
			this.chkCentered.TabIndex = 20;
			this.chkCentered.Text = "Centered glyphs";
			this.chkCentered.UseVisualStyleBackColor = true;
			this.chkCentered.CheckedChanged += new System.EventHandler(this.chkCentered_CheckedChanged);
			// 
			// line3
			// 
			this.line3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.line3.BackColor = System.Drawing.Color.Maroon;
			this.line3.Location = new System.Drawing.Point(0, 450);
			this.line3.Name = "line3";
			this.line3.Size = new System.Drawing.Size(366, 1);
			this.line3.TabIndex = 34;
			this.line3.TabStop = false;
			// 
			// lblFolderL
			// 
			this.lblFolderL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblFolderL.AutoSize = true;
			this.lblFolderL.Location = new System.Drawing.Point(6, 462);
			this.lblFolderL.Name = "lblFolderL";
			this.lblFolderL.Size = new System.Drawing.Size(68, 13);
			this.lblFolderL.TabIndex = 21;
			this.lblFolderL.Text = "Output folder";
			// 
			// txtFolder
			// 
			this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.txtFolder.Location = new System.Drawing.Point(6, 480);
			this.txtFolder.Name = "txtFolder";
			this.txtFolder.ReadOnly = true;
			this.txtFolder.Size = new System.Drawing.Size(324, 20);
			this.txtFolder.TabIndex = 22;
			this.txtFolder.WordWrap = false;
			// 
			// btnFolder
			// 
			this.btnFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnFolder.FlatAppearance.BorderSize = 0;
			this.btnFolder.Location = new System.Drawing.Point(336, 480);
			this.btnFolder.Name = "btnFolder";
			this.btnFolder.Size = new System.Drawing.Size(24, 21);
			this.btnFolder.TabIndex = 23;
			this.btnFolder.Text = "...";
			this.btnFolder.UseVisualStyleBackColor = true;
			this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
			// 
			// lblSymbolL
			// 
			this.lblSymbolL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblSymbolL.AutoSize = true;
			this.lblSymbolL.Location = new System.Drawing.Point(6, 510);
			this.lblSymbolL.Name = "lblSymbolL";
			this.lblSymbolL.Size = new System.Drawing.Size(70, 13);
			this.lblSymbolL.TabIndex = 24;
			this.lblSymbolL.Text = "Symbol name";
			// 
			// txtSymbol
			// 
			this.txtSymbol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.txtSymbol.Location = new System.Drawing.Point(6, 528);
			this.txtSymbol.Name = "txtSymbol";
			this.txtSymbol.Size = new System.Drawing.Size(174, 20);
			this.txtSymbol.TabIndex = 25;
			this.txtSymbol.WordWrap = false;
			this.txtSymbol.TextChanged += new System.EventHandler(this.txtSymbol_TextChanged);
			// 
			// btnBuild
			// 
			this.btnBuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBuild.FlatAppearance.BorderSize = 0;
			this.btnBuild.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnBuild.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnBuild.Location = new System.Drawing.Point(276, 522);
			this.btnBuild.Name = "btnBuild";
			this.btnBuild.Size = new System.Drawing.Size(84, 24);
			this.btnBuild.TabIndex = 26;
			this.btnBuild.Text = "Build";
			this.btnBuild.UseVisualStyleBackColor = true;
			this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(367, 553);
			this.Controls.Add(this.radFont);
			this.Controls.Add(this.radCharsGlyphs);
			this.Controls.Add(this.radImageGlyph);
			this.Controls.Add(this.txtSource);
			this.Controls.Add(this.btnSource);
			this.Controls.Add(this.line1);
			this.Controls.Add(this.lblCharsL);
			this.Controls.Add(this.txtChars);
			this.Controls.Add(this.btnLetters);
			this.Controls.Add(this.btnDigits);
			this.Controls.Add(this.btnASCII);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.lblCPL);
			this.Controls.Add(this.cmbCP);
			this.Controls.Add(this.btnExtended);
			this.Controls.Add(this.line2);
			this.Controls.Add(this.lblBPPL);
			this.Controls.Add(this.trkBPP);
			this.Controls.Add(this.lblContrastL);
			this.Controls.Add(this.trkContrast);
			this.Controls.Add(this.lblChars);
			this.Controls.Add(this.chkClearType);
			this.Controls.Add(this.chkCentered);
			this.Controls.Add(this.line3);
			this.Controls.Add(this.lblFolderL);
			this.Controls.Add(this.txtFolder);
			this.Controls.Add(this.btnFolder);
			this.Controls.Add(this.lblSymbolL);
			this.Controls.Add(this.txtSymbol);
			this.Controls.Add(this.btnBuild);
			this.MinimumSize = new System.Drawing.Size(383, 513);
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "eXtended Library Utils";
			this.Load += new System.EventHandler(this.FormMain_Load);
			((System.ComponentModel.ISupportInitialize)(this.line1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.line2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trkBPP)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trkContrast)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.line3)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private RadioButton radFont;
		private RadioButton radCharsGlyphs;
		private RadioButton radImageGlyph;
		private TextBox txtSource;
		private Button btnSource;
		private PictureBox line1;
		private Label lblCharsL;
		private TextBox txtChars;
		private Button btnLetters;
		private Button btnDigits;
		private Button btnASCII;
		private Button btnClear;
		private Label lblCPL;
		private ComboBox cmbCP;
		private Button btnExtended;
		private PictureBox line2;
		private Label lblBPPL;
		private TrackBar trkBPP;
		private Label lblContrastL;
		private TrackBar trkContrast;
		private CustomLabel lblChars;
		private CheckBox chkClearType;
		private CheckBox chkCentered;
		private PictureBox line3;
		private Label lblFolderL;
		private TextBox txtFolder;
		private Button btnFolder;
		private Label lblSymbolL;
		private TextBox txtSymbol;
		private Button btnBuild;
    }
}

