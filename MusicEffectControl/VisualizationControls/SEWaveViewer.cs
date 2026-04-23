using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using NAudio.Wave;
using MusicEffectControl;
using System.IO;

namespace Sound_Editor {
    /*
  LICENSE
  -------
  Copyright (C) 2007-2010 Ray Molenkamp

  This source code is provided 'as-is', without any express or implied
  warranty.  In no event will the authors be held liable for any damages
  arising from the use of this source code or the software it produces.

  Permission is granted to anyone to use this source code for any purpose,
  including commercial applications, and to alter it and redistribute it
  freely, subject to the following restrictions:

  1. The origin of this source code must not be misrepresented; you must not
     claim that you wrote the original source code.  If you use this source code
     in a product, an acknowledgment in the product documentation would be
     appreciated but is not required.
  2. Altered source versions must be plainly marked as such, and must not be
     misrepresented as being the original source code.
  3. This notice may not be removed or altered from any source distribution.
  */

    //This class is an improvement based on the source code of Ray Molenkamp.

    public class SEWaveViewer : System.Windows.Forms.UserControl {
        public SpectrogramViewer Spectrogram { get; set; }
        public SpectrumViewer Spectrum { get; set; }
        public Color penColor { get; set; }
        public float PenWidth { get; set; }

        private System.ComponentModel.Container components = null;

        private WaveStream waveStream;  // WaveFileReader or MP3FileReader
        private AudioFile audio;
        private int samplesPerPixel = 128;
        private long startPosition;
        private int bytesPerSample;
        private double millisecondsPerSample;

        public SEWaveViewer() {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
            this.DoubleBuffered = true;

            this.penColor = Color.DodgerBlue;
            this.PenWidth = 1;
        }

        public void FitToScreen() {
            if (waveStream == null || this.Width == 0) return;
            int samples = (int)(waveStream.Length / bytesPerSample);
            startPosition = 0;
            SamplesPerPixel = samples / this.Width;
            millisecondsPerSample = samples / audio.Duration.TotalMilliseconds;
            DeFine.ActiveEdit.StartTime = new TimeSpan(0);
            DeFine.ActiveEdit.EndTime = new TimeSpan(0,0,0,0, (int)(samples / millisecondsPerSample));
            Spectrogram.StartPosition = 0;
            Spectrogram.Count = (int)(this.WaveStream.Length / 2) / 1024;
        }

        public void Zoom(int leftSample, int rightSample) {
            startPosition = leftSample * bytesPerSample;
            int samples = (rightSample - leftSample);
            SamplesPerPixel = samples / this.Width;
            if (this.inverseMouseDrag) {
                DeFine.ActiveEdit.StartTime = new TimeSpan(0, 0, 0, 0, (int)(startPosition / bytesPerSample / millisecondsPerSample));
                DeFine.ActiveEdit.EndTime = clickPosition;
            } else {
                DeFine.ActiveEdit.StartTime = clickPosition;
                DeFine.ActiveEdit.EndTime = new TimeSpan(0, 0, 0, 0, (int)(startPosition / bytesPerSample / millisecondsPerSample + samples / millisecondsPerSample));
            }
            // Указываем контролу спектрограммы начальный сэмпл и их количество для отображения
            Spectrogram.StartPosition = startPosition / 2;
            Spectrogram.Count = (samples * 2) / 1024;
        }

        private Point mousePos, startPos;
        private bool mouseDrag = false;
        private bool inverseMouseDrag = false;
        private TimeSpan clickPosition = new TimeSpan();

        public TimeSpan CutStart = new TimeSpan(0);
        public TimeSpan CutEnd = new TimeSpan(0);

        protected override void OnMouseDown(MouseEventArgs e) {
            if (this.WaveStream == null) return;
            if (e.Button == MouseButtons.Left) 
            {
                startPos = e.Location;
                if (startPos.X < 0 || startPos.X > this.Width) return;
                WaveStream.Position = StartPosition + startPos.X * bytesPerSample * samplesPerPixel;
                MainForm.originalPosition.CurrentTime = WaveStream.CurrentTime;
            }
            if (e.Button == MouseButtons.Right)
            {
                startPos = e.Location;
                if (startPos.X < 0 || startPos.X > this.Width) return;

                if (CutStart.Ticks == 0)
                {
                    CutStart = new TimeSpan(StartPosition + startPos.X * bytesPerSample * samplesPerPixel);
                    DeFine.ActiveEdit.CutTimeByStart.Text = new TimeSpan(CutStart.Ticks * 100).ToString();
                }
                else
                { 
                    var GetStart = new TimeSpan(StartPosition + startPos.X * bytesPerSample * samplesPerPixel);
                    if (GetStart < CutStart)
                    {
                        CutStart = new TimeSpan(StartPosition + startPos.X * bytesPerSample * samplesPerPixel);
                        DeFine.ActiveEdit.CutTimeByStart.Text = new TimeSpan(CutStart.Ticks * 100).ToString();
                    }
                    else
                    if (GetStart > CutStart)
                    {
                        CutEnd = new TimeSpan(StartPosition + startPos.X * bytesPerSample * samplesPerPixel);
                        DeFine.ActiveEdit.CutTimeByEnd.Text = new TimeSpan(CutEnd.Ticks * 100).ToString();

                        FitToScreen();
                    }
                }
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e) {
           
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            
            base.OnMouseUp(e);

           
        }

       

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
            this.FitToScreen();
        }

        private void DrawVerticalLine(int x)
        {
            ControlPaint.DrawReversibleLine(PointToScreen(new Point(x, 0)), PointToScreen(new Point(x, Height)), Color.White);
        }

        public void DrawRedLine(int x)
        {
            ControlPaint.DrawReversibleLine(PointToScreen(new Point(x, 0)), PointToScreen(new Point(x, Height)), Color.Red);
        }


        public void DrawOrangeLine(int x)
        {
            ControlPaint.DrawReversibleLine(PointToScreen(new Point(x, 0)), PointToScreen(new Point(x, Height)), Color.Orange);
        }

        public AudioFile Audio {
            get {
                return audio;
            }
            set {
                audio = value;
                if (audio != null) {
                    if (audio.Format == Enums.AudioFormats.MP3) {
                        MP3File file = audio as MP3File;
                        WaveStream = file.Reader;
                    } else if (audio.Format == Enums.AudioFormats.WAV) {
                        WaveFile file = audio as WaveFile;
                        WaveStream = file.Reader;
                    }
                }
            }
        }

        public WaveStream WaveStream {
            get {
                return waveStream;
            }
            set {
                waveStream = value;
                if (waveStream != null) {
                    bytesPerSample = (waveStream.WaveFormat.BitsPerSample / 8) * waveStream.WaveFormat.Channels;
                }
                this.Invalidate();
            }
        }

        public int SamplesPerPixel {
            get {
                return samplesPerPixel;
            }
            set {
                samplesPerPixel = Math.Max(1, value);
                this.Invalidate();
            }
        }

        public long StartPosition {
            get {
                return startPosition;
            }
            set {
                startPosition = value;
            }
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        public int DrawWidth = 0;
        protected override void OnPaint(PaintEventArgs e) {
            if (waveStream == null) {
                base.OnPaint(e);
                return;
            }
            try {
                int bytesToRead = samplesPerPixel * bytesPerSample;
                byte[] waveData = new byte[bytesToRead];
                long position = startPosition + (e.ClipRectangle.Left * bytesPerSample * samplesPerPixel);
                DrawWidth = e.ClipRectangle.Right;
                using (Pen linePen = new Pen(this.penColor, this.PenWidth)) {
                    for (float x = e.ClipRectangle.X; x < e.ClipRectangle.Right; x += 1) {
                        short low = 0;
                        short high = 0;
                        for (int i = 0; i < bytesToRead; i++) {
                            waveData[i] = Audio.Samples[position + i];
                        }
                        position += bytesToRead;
                        for (int n = 0; n < bytesToRead; n += 2) {
                            short sample = BitConverter.ToInt16(waveData, n);
                            if (sample < low) low = sample;
                            if (sample > high) high = sample;
                        }
                        float lowPercent = ((((float)low) - short.MinValue) / ushort.MaxValue);
                        float highPercent = ((((float)high) - short.MinValue) / ushort.MaxValue);

                        if (this.CutEnd.Ticks > 0)
                        {
                            if (position >= this.CutStart.Ticks && position <= this.CutEnd.Ticks)
                            {
                                Pen CutPen = new Pen(Color.Orange, 3);

                                e.Graphics.DrawLine(CutPen, x, this.Height * lowPercent, x, this.Height * highPercent);
                            }
                            else
                            {
                                e.Graphics.DrawLine(linePen, x, this.Height * lowPercent, x, this.Height * highPercent);
                            }
                        }
                        else
                        {
                            e.Graphics.DrawLine(linePen, x, this.Height * lowPercent, x, this.Height * highPercent);
                        }
                    }
                }
            } catch (Exception) {
                //MessageBox.Show("Невозможно отобразить уровнеграмму", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } finally {
                base.OnPaint(e);
            }
        }

        private void SEWaveViewer_Load(object sender, EventArgs e)
        {

        }

        #region Component Designer generated code
        private void InitializeComponent() {
            this.SuspendLayout();
            // 
            // SEWaveViewer
            // 
            this.Name = "SEWaveViewer";
            this.Load += new System.EventHandler(this.SEWaveViewer_Load);
            this.ResumeLayout(false);

        }
        #endregion
    }
}