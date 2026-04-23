namespace Sound_Editor {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.originalPlayTimer = new System.Windows.Forms.Timer(this.components);
            this.spectrumTimer = new System.Windows.Forms.Timer(this.components);
            this.recordingTimer = new System.Windows.Forms.Timer(this.components);
            this.changePositionTimer = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.audioStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.audioRate = new System.Windows.Forms.ToolStripStatusLabel();
            this.audioSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.audioLength = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.originalCurrentTime = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.spectrogramVisualizationTab = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.spectrumViewer = new Sound_Editor.SpectrumViewer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.originalSpectrogramViewer = new Sound_Editor.SpectrogramViewer();
            this.originalVizualizationTab = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.originalWaveViewer = new Sound_Editor.SEWaveViewer();
            this.trackBarOriginal = new System.Windows.Forms.TrackBar();
            this.button4 = new System.Windows.Forms.Button();
            this.ReSet = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.CutTimeByStart = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.CutTimeByEnd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.spectrogramVisualizationTab.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage10.SuspendLayout();
            this.originalVizualizationTab.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOriginal)).BeginInit();
            this.SuspendLayout();
            // 
            // originalPlayTimer
            // 
            this.originalPlayTimer.Tick += new System.EventHandler(this.originalPlayTimer_Tick);
            // 
            // spectrumTimer
            // 
            this.spectrumTimer.Interval = 50;
            this.spectrumTimer.Tick += new System.EventHandler(this.spectrumTimer_Tick);
            // 
            // changePositionTimer
            // 
            this.changePositionTimer.Tick += new System.EventHandler(this.changePositionTimer_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.audioStatus,
            this.audioRate,
            this.audioSize,
            this.audioLength});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1125);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(1696, 32);
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // audioStatus
            // 
            this.audioStatus.Name = "audioStatus";
            this.audioStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.audioStatus.Size = new System.Drawing.Size(1434, 25);
            this.audioStatus.Spring = true;
            this.audioStatus.Text = "完毕";
            this.audioStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.audioStatus.ToolTipText = "地位";
            // 
            // audioRate
            // 
            this.audioRate.AutoSize = false;
            this.audioRate.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.audioRate.Name = "audioRate";
            this.audioRate.Size = new System.Drawing.Size(94, 25);
            this.audioRate.Text = "0 Hz";
            this.audioRate.ToolTipText = "采样频率";
            // 
            // audioSize
            // 
            this.audioSize.AutoSize = false;
            this.audioSize.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.audioSize.Name = "audioSize";
            this.audioSize.Size = new System.Drawing.Size(75, 25);
            this.audioSize.Text = "0 MB";
            this.audioSize.ToolTipText = "音频大小";
            // 
            // audioLength
            // 
            this.audioLength.AutoSize = false;
            this.audioLength.Name = "audioLength";
            this.audioLength.Size = new System.Drawing.Size(70, 25);
            this.audioLength.Text = "00:00:000";
            this.audioLength.ToolTipText = "期间";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(38, 38);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.originalCurrentTime,
            this.toolStripButton2,
            this.toolStripButton1,
            this.toolStripButton3,
            this.toolStripSeparator2,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripButton6,
            this.toolStripButton7});
            this.toolStrip2.Location = new System.Drawing.Point(17, 432);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(1662, 96);
            this.toolStrip2.TabIndex = 27;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // originalCurrentTime
            // 
            this.originalCurrentTime.AutoSize = false;
            this.originalCurrentTime.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.originalCurrentTime.ForeColor = System.Drawing.Color.Indigo;
            this.originalCurrentTime.Name = "originalCurrentTime";
            this.originalCurrentTime.Size = new System.Drawing.Size(276, 47);
            this.originalCurrentTime.Text = "00:00:000";
            this.originalCurrentTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(42, 91);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.ToolTipText = "停止";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.AutoSize = false;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(38, 38);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.ToolTipText = "继续";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(42, 91);
            this.toolStripButton3.Text = "toolStripButton3";
            this.toolStripButton3.ToolTipText = "暂停";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 96);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(34, 91);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(42, 91);
            this.toolStripButton5.Text = "toolStripButton5";
            this.toolStripButton5.ToolTipText = "快退";
            this.toolStripButton5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolStripButton5_MouseDown);
            this.toolStripButton5.MouseUp += new System.Windows.Forms.MouseEventHandler(this.toolStripButton5_MouseUp);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(42, 91);
            this.toolStripButton6.Text = "toolStripButton6";
            this.toolStripButton6.ToolTipText = "快进";
            this.toolStripButton6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolStripButton6_MouseDown);
            this.toolStripButton6.MouseUp += new System.Windows.Forms.MouseEventHandler(this.toolStripButton6_MouseUp);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(34, 91);
            // 
            // spectrogramVisualizationTab
            // 
            this.spectrogramVisualizationTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spectrogramVisualizationTab.Controls.Add(this.tabPage3);
            this.spectrogramVisualizationTab.Controls.Add(this.tabPage10);
            this.spectrogramVisualizationTab.Location = new System.Drawing.Point(17, 502);
            this.spectrogramVisualizationTab.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.spectrogramVisualizationTab.Name = "spectrogramVisualizationTab";
            this.spectrogramVisualizationTab.SelectedIndex = 0;
            this.spectrogramVisualizationTab.Size = new System.Drawing.Size(1662, 553);
            this.spectrogramVisualizationTab.TabIndex = 28;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.spectrumViewer);
            this.tabPage3.Controls.Add(this.tableLayoutPanel1);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1654, 520);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "光谱";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // spectrumViewer
            // 
            this.spectrumViewer.Audio = null;
            this.spectrumViewer.BackColor = System.Drawing.Color.Black;
            this.spectrumViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spectrumViewer.Location = new System.Drawing.Point(0, 0);
            this.spectrumViewer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.spectrumViewer.Name = "spectrumViewer";
            this.spectrumViewer.PenColor = System.Drawing.Color.Red;
            this.spectrumViewer.PenWidth = 2;
            this.spectrumViewer.Size = new System.Drawing.Size(1654, 520);
            this.spectrumViewer.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tabPage10
            // 
            this.tabPage10.AutoScroll = true;
            this.tabPage10.AutoScrollMinSize = new System.Drawing.Size(0, 512);
            this.tabPage10.Controls.Add(this.originalSpectrogramViewer);
            this.tabPage10.Location = new System.Drawing.Point(4, 29);
            this.tabPage10.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Size = new System.Drawing.Size(1290, 516);
            this.tabPage10.TabIndex = 1;
            this.tabPage10.Text = "频谱图";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // originalSpectrogramViewer
            // 
            this.originalSpectrogramViewer.Area = null;
            this.originalSpectrogramViewer.Audio = null;
            this.originalSpectrogramViewer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(19)))), ((int)(((byte)(1)))));
            this.originalSpectrogramViewer.Count = 0;
            this.originalSpectrogramViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.originalSpectrogramViewer.Location = new System.Drawing.Point(0, 0);
            this.originalSpectrogramViewer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.originalSpectrogramViewer.MinimumSize = new System.Drawing.Size(0, 788);
            this.originalSpectrogramViewer.Name = "originalSpectrogramViewer";
            this.originalSpectrogramViewer.Size = new System.Drawing.Size(1290, 788);
            this.originalSpectrogramViewer.StartPosition = ((long)(0));
            this.originalSpectrogramViewer.TabIndex = 1;
            // 
            // originalVizualizationTab
            // 
            this.originalVizualizationTab.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.originalVizualizationTab.Controls.Add(this.tabPage2);
            this.originalVizualizationTab.Location = new System.Drawing.Point(13, 14);
            this.originalVizualizationTab.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.originalVizualizationTab.Name = "originalVizualizationTab";
            this.originalVizualizationTab.SelectedIndex = 0;
            this.originalVizualizationTab.Size = new System.Drawing.Size(1670, 417);
            this.originalVizualizationTab.TabIndex = 26;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.AutoScrollMinSize = new System.Drawing.Size(848, 0);
            this.tabPage2.Controls.Add(this.originalWaveViewer);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1662, 384);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "编辑";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // originalWaveViewer
            // 
            this.originalWaveViewer.Audio = null;
            this.originalWaveViewer.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.originalWaveViewer.BackColor = System.Drawing.Color.Black;
            this.originalWaveViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.originalWaveViewer.Location = new System.Drawing.Point(0, 0);
            this.originalWaveViewer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.originalWaveViewer.Name = "originalWaveViewer";
            this.originalWaveViewer.penColor = System.Drawing.Color.DodgerBlue;
            this.originalWaveViewer.PenWidth = 1F;
            this.originalWaveViewer.SamplesPerPixel = 128;
            this.originalWaveViewer.Size = new System.Drawing.Size(1662, 384);
            this.originalWaveViewer.Spectrogram = null;
            this.originalWaveViewer.Spectrum = null;
            this.originalWaveViewer.StartPosition = ((long)(0));
            this.originalWaveViewer.TabIndex = 0;
            this.originalWaveViewer.WaveStream = null;
            // 
            // trackBarOriginal
            // 
            this.trackBarOriginal.Location = new System.Drawing.Point(1524, 450);
            this.trackBarOriginal.Name = "trackBarOriginal";
            this.trackBarOriginal.Size = new System.Drawing.Size(10, 69);
            this.trackBarOriginal.TabIndex = 29;
            this.trackBarOriginal.Scroll += new System.EventHandler(this.trackBarOriginal_Scroll);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1495, 462);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(128, 38);
            this.button4.TabIndex = 56;
            this.button4.Text = "重置回源文件";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // ReSet
            // 
            this.ReSet.Location = new System.Drawing.Point(1251, 462);
            this.ReSet.Name = "ReSet";
            this.ReSet.Size = new System.Drawing.Size(50, 38);
            this.ReSet.TabIndex = 55;
            this.ReSet.Text = "重设";
            this.ReSet.UseVisualStyleBackColor = true;
            this.ReSet.Click += new System.EventHandler(this.button5_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1349, 462);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(128, 38);
            this.button3.TabIndex = 54;
            this.button3.Text = "保存更改";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(600, 471);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 20);
            this.label1.TabIndex = 50;
            this.label1.Text = "开始";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1199, 462);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(46, 38);
            this.button2.TabIndex = 53;
            this.button2.Text = "->";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // CutTimeByStart
            // 
            this.CutTimeByStart.Location = new System.Drawing.Point(647, 468);
            this.CutTimeByStart.Multiline = true;
            this.CutTimeByStart.Name = "CutTimeByStart";
            this.CutTimeByStart.ReadOnly = true;
            this.CutTimeByStart.Size = new System.Drawing.Size(216, 26);
            this.CutTimeByStart.TabIndex = 48;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(869, 462);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 38);
            this.button1.TabIndex = 52;
            this.button1.Text = "->";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CutTimeByEnd
            // 
            this.CutTimeByEnd.Location = new System.Drawing.Point(977, 468);
            this.CutTimeByEnd.Name = "CutTimeByEnd";
            this.CutTimeByEnd.ReadOnly = true;
            this.CutTimeByEnd.Size = new System.Drawing.Size(216, 26);
            this.CutTimeByEnd.TabIndex = 49;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(930, 474);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 20);
            this.label2.TabIndex = 51;
            this.label2.Text = "结束";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1696, 1157);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.ReSet);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.CutTimeByStart);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CutTimeByEnd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackBarOriginal);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.spectrogramVisualizationTab);
            this.Controls.Add(this.originalVizualizationTab);
            this.Controls.Add(this.statusStrip1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(1062, 801);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sound Editor 左键跳到指定音频位置 右键设置裁剪坐标";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.spectrogramVisualizationTab.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage10.ResumeLayout(false);
            this.originalVizualizationTab.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOriginal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer originalPlayTimer;
        private System.Windows.Forms.Timer spectrumTimer;
        private System.Windows.Forms.Timer recordingTimer;
        private System.Windows.Forms.Timer changePositionTimer;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel audioStatus;
        private System.Windows.Forms.ToolStripStatusLabel audioRate;
        private System.Windows.Forms.ToolStripStatusLabel audioSize;
        private System.Windows.Forms.ToolStripStatusLabel audioLength;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel originalCurrentTime;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.TabControl spectrogramVisualizationTab;
        private System.Windows.Forms.TabPage tabPage3;
        private SpectrumViewer spectrumViewer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabPage tabPage10;
        private SpectrogramViewer originalSpectrogramViewer;
        private System.Windows.Forms.TabControl originalVizualizationTab;
        private System.Windows.Forms.TabPage tabPage2;
        private SEWaveViewer originalWaveViewer;
        private System.Windows.Forms.TrackBar trackBarOriginal;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button ReSet;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.TextBox CutTimeByStart;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox CutTimeByEnd;
        private System.Windows.Forms.Label label2;
    }
}

