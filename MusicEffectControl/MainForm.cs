using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using NAudio;
using NAudio.Wave;
using NAudio.Dsp;
using NAudio.Codecs;
using Sound_Editor.Enums;
using MusicEffectControl;
using MusicEffectControl.MusicManage;
using MusicEffectControl.ConvertManage;
using System.Threading;

namespace Sound_Editor
{

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
    public partial class MainForm : Form
      {
          public MainForm()
          {
              InitializeComponent();
          }

          public static Position originalPosition = null;
          public static TimePeriod allocatedPeriod = null;


          private List<AudioFile> files = new List<AudioFile>();
          private AudioFile currentAudio = null;
          private WaveOut output = null;

          private Directions direction;
          private int tmpCount = 0;


          private WaveFileWriter waveWriter = null;

          public string CurrentFile = "";

          public int CurrentRowid = 0;

          public void SetMp3File(int Rowid,string Source,string TimeRange)
          {
              if (TimeRange.Trim().Length > 0)
              {
                  if (TimeRange.Contains("-"))
                  {
                      long GetStart = ConvertHelper.ObjToLong(TimeRange.Split('-')[0]);
                      long GetEnd = ConvertHelper.ObjToLong(TimeRange.Split('-')[1]);

                      originalWaveViewer.CutStart = new TimeSpan(GetStart);
                      originalWaveViewer.CutEnd = new TimeSpan(GetEnd);

                      DeFine.ActiveEdit.CutTimeByStart.Text = (originalWaveViewer.CutStart.Ticks * 100).ToString();
                      DeFine.ActiveEdit.CutTimeByEnd.Text = (originalWaveViewer.CutEnd.Ticks * 100).ToString();
                      originalWaveViewer.FitToScreen();
                  }
              }
              CurrentRowid = Rowid;

              AudioFile file = null;
              Mp3FileReader reader = new Mp3FileReader(Source);
              WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(reader);
              BlockAlignReductionStream stream = new BlockAlignReductionStream(pcm);
              file = new MP3File(reader, stream, Source);

              files.Add(file);

              if (files.Count == 1)
              {
                  this.initAudio(file);
              }
              CurrentFile = Source;
          }

          void CutMp3(string filePath, string outputPath, TimeSpan start, TimeSpan end)
          {
              try
              {
                  start = new TimeSpan(start.Ticks * 100);
                  end = new TimeSpan(end.Ticks * 100);
                  //读取mp3音频文件
                  using (var reader = new Mp3FileReader(filePath))
                  {
                      //创建输出剪辑文件
                      using (var writer = File.Create(outputPath))
                      {
                          Mp3Frame frame;

                          //遍历音频每一帧

                          while ((frame = reader.ReadNextFrame()) != null)

                              if (reader.CurrentTime >= start)
                              {

                                  if (reader.CurrentTime <= end)
                                  {
                                      //时间数值属于音频时长正常范围 写入文件

                                      writer.Write(frame.RawData, 0, frame.RawData.Length);
                                  }

                                  else
                                  {
                                      //超出音频时间范围跳出

                                      break;

                                  }
                              }
                      }
                  }
              }
              catch (Exception ex)
              {

              }

          }

          public TimeSpan StartTime = new TimeSpan();
          public TimeSpan EndTime = new TimeSpan();

          private void MainForm_Load(object sender, EventArgs e)
          {
              spectrumViewer.PenColor = Color.GreenYellow;
              spectrumViewer.PenWidth = 2;

              originalSpectrogramViewer.Area = spectrogramVisualizationTab.TabPages[1];

              originalPosition = new Position(originalCurrentTime);
              originalPosition.CurrentTime = new TimeSpan(0);


              StartTime = new TimeSpan(0);
              EndTime = new TimeSpan(0);



              output = new WaveOut();
              output.Volume = 1f;
              output.PlaybackStopped += Output_PlaybackStopped;
          }



          private void initAudio(AudioFile f)
          {
              this.currentAudio = f;
              try
              {
                  output.Init(f.Stream);
              }
              catch (Exception)
              {
                  MessageBox.Show("该文件无法播放.", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  this.files.Remove(f);

                  return;
              }
              originalSpectrogramViewer.Audio = f;

              originalWaveViewer.Spectrogram = originalSpectrogramViewer;
              originalWaveViewer.Spectrum = spectrumViewer;
              originalWaveViewer.Audio = f;
              originalWaveViewer.FitToScreen();

              spectrumViewer.Audio = f;


              originalVizualizationTab.TabPages[0].Text = "编辑: " + f.Name + "." + f.Format;
              audioRate.Text = f.SampleRate + " Hz";
              audioSize.Text = Math.Round(f.Size, 1).ToString() + " MB";
              audioLength.Text = Position.getTimeString(f.Duration);
          }



          // Pause
          private void toolStripButton3_Click(object sender, EventArgs e)
          {
              if (output != null && currentAudio != null)
              {
                  if (output.PlaybackState == PlaybackState.Playing)
                  {
                      output.Pause();
                      originalPlayTimer.Enabled = false;
                      spectrumTimer.Enabled = false;
                      audioStatus.Text = "暂停: " + currentAudio.Name + "." + currentAudio.Format;
                  }
              }
          }

          // Play
          private void toolStripButton1_Click(object sender, EventArgs e)
          {
              if (output != null && currentAudio != null)
              {
                  if (output.PlaybackState != PlaybackState.Playing)
                  {
                      output.Play();
                      originalPlayTimer.Enabled = true;
                      spectrumTimer.Enabled = true;
                      audioStatus.Text = "回放: " + currentAudio.Name + "." + currentAudio.Format;
                  }
              }
          }
          public void End()
          {
              if (output != null && currentAudio != null)
              {
                  currentAudio.Stream.Position = 0;
                  output.Stop();
                  originalPlayTimer.Enabled = false;
                  originalPosition.CurrentTime = new TimeSpan(0);
                  spectrumTimer.Enabled = false;
                  audioStatus.Text = "已停止: " + currentAudio.Name + "." + currentAudio.Format;
              }
          }
          // Stop
          private void toolStripButton2_Click(object sender, EventArgs e)
          {
              StartTime = new TimeSpan(0);
              EndTime = new TimeSpan(0);
              originalWaveViewer.FitToScreen();

              End();
          }

          private void Output_PlaybackStopped(object sender, StoppedEventArgs e)
          {
              originalPlayTimer.Stop();
              spectrumTimer.Stop();
              audioStatus.Text = "已停止: " + currentAudio.Name + "." + currentAudio.Format;
              currentAudio.Stream.Position = 0;
          }



          /* Методы перемотки */

    // Back
    private void back()
        {
            long position = this.currentAudio.Stream.Position;
            if (currentAudio.Format == AudioFormats.MP3)
            {
                MP3File file = currentAudio as MP3File;
                position = file.Reader.Position;
            }
            position = position - this.currentAudio.Stream.WaveFormat.AverageBytesPerSecond;
            if (position < 0)
            {
                position = 0;
                this.currentAudio.Stream.CurrentTime = new TimeSpan(0);
            }
            else
            {
                this.currentAudio.Stream.CurrentTime.Subtract(new TimeSpan(0, 0, 1));
            }
            this.currentAudio.Stream.Position = position;
            if (output.PlaybackState != PlaybackState.Playing)
            {
                originalPosition.CurrentTime = this.currentAudio.Stream.CurrentTime;
            }
        }

        private void toolStripButton5_MouseDown(object sender, MouseEventArgs e)
        {
            if (output != null && this.currentAudio != null)
            {
                this.direction = Directions.BACK;
                changePositionTimer.Start();
            }
        }

        private void toolStripButton5_MouseUp(object sender, MouseEventArgs e)
        {
            changePositionTimer.Stop();
        }

        // Forward
        private void forward()
        {
            long position = this.currentAudio.Stream.Position;
            if (currentAudio.Format == AudioFormats.MP3)
            {
                MP3File file = currentAudio as MP3File;
                position = file.Reader.Position;
            }
            if (position + this.currentAudio.Stream.WaveFormat.AverageBytesPerSecond > this.currentAudio.Stream.Length) return;
            this.currentAudio.Stream.Position = position + this.currentAudio.Stream.WaveFormat.AverageBytesPerSecond;
            this.currentAudio.Stream.CurrentTime.Add(new TimeSpan(0, 0, 1));
            if (output.PlaybackState != PlaybackState.Playing)
            {
                originalPosition.CurrentTime = this.currentAudio.Stream.CurrentTime;
            }
        }

        private void toolStripButton6_MouseDown(object sender, MouseEventArgs e)
        {
            if (output != null && this.currentAudio != null)
            {
                this.direction = Directions.FORWARD;
                changePositionTimer.Start();
            }
        }

        private void toolStripButton6_MouseUp(object sender, MouseEventArgs e)
        {
            changePositionTimer.Stop();
        }

        private void changePositionTimer_Tick(object sender, EventArgs e)
        {
            if (this.direction == Directions.BACK)
            {
                this.back();
            }
            else if (this.direction == Directions.FORWARD)
            {
                this.forward();
            }
        }

        private void DisposeWave()
        {
            if (output != null)
            {
                if (output.PlaybackState == PlaybackState.Playing)
                {
                    output.Stop();
                }
                output.Dispose();
                output = null;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisposeWave();
            foreach (AudioFile file in this.files)
            {
                file.Stream.Dispose();
                file.Stream = null;
                if (file.Format == AudioFormats.MP3)
                {
                    MP3File mp3file = file as MP3File;
                    mp3file.Reader.Dispose();
                    mp3file.Reader = null;
                }
                else
                {
                    WaveFile wavfile = file as WaveFile;
                    wavfile.Reader.Dispose();
                    wavfile.Reader = null;
                }
            }
            for (int i = this.tmpCount; i > 0; i--)
            {
                File.Delete("tmp_" + i + ".wav");
            }
        }

        private void originalPlayTimer_Tick(object sender, EventArgs e)
        {
            if (currentAudio == null) return;
            TimeSpan currentTime = new TimeSpan();
            if (currentAudio.Format == AudioFormats.MP3)
            {
                MP3File file = currentAudio as MP3File;
                currentTime = file.Reader.CurrentTime;
            }
            else if (currentAudio.Format == AudioFormats.WAV)
            {
                WaveFile file = currentAudio as WaveFile;
                currentTime = file.Reader.CurrentTime;
            }
            originalPosition.CurrentTime = currentTime;

            double GetRate = (double)((double)originalPosition.CurrentTime.Ticks / (double)EndTime.Ticks);
            try
            {
                if (DeFine.CanReSet)
                {
                    originalWaveViewer.DrawRedLine(Convert.ToInt32(originalWaveViewer.DrawWidth * GetRate));
                    originalWaveViewer.Refresh();
                }
            }
            catch { }
        }

        private void trackBarOriginal_Scroll(object sender, EventArgs e)
        {
            if (output != null)
            {
                output.Volume = trackBarOriginal.Value / 10f;
            }
        }

        private void spectrumTimer_Tick(object sender, EventArgs e)
        {
            spectrumViewer.Refresh();
        }

        private void compressedSpectrumTimer_Tick(object sender, EventArgs e)
        {
        }





        private void SourceStream_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (this.waveWriter == null) return;
            this.waveWriter.Write(e.Buffer, 0, e.BytesRecorded);
            this.waveWriter.Flush();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (output.PlaybackState != PlaybackState.Playing)
            {
                output.Play();
                originalPlayTimer.Enabled = true;
                spectrumTimer.Enabled = true;
            }
            originalWaveViewer.WaveStream.Position = originalWaveViewer.CutStart.Ticks;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (output.PlaybackState != PlaybackState.Playing)
            {
                output.Play();
                originalPlayTimer.Enabled = true;
                spectrumTimer.Enabled = true;
            }
            originalWaveViewer.WaveStream.Position = originalWaveViewer.CutEnd.Ticks;
        }


        private void button5_Click(object sender, EventArgs e)
        {
            CutTimeByStart.Text = "";
            CutTimeByEnd.Text = "";
            originalWaveViewer.CutEnd = new TimeSpan(0);
            originalWaveViewer.CutStart = new TimeSpan(0);
            originalWaveViewer.FitToScreen();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string GetFileName = CurrentFile.Substring(CurrentFile.LastIndexOf(@"\")+ @"\".Length);
            string SetPath = DeFine.GetFullPath(@"\Resources\" + "Change_" + GetFileName);

            if (File.Exists(SetPath))
            {
                File.Delete(SetPath);
            }

            CutMp3(CurrentFile, SetPath, originalWaveViewer.CutStart, originalWaveViewer.CutEnd);

            var GetMusicItem = MusicsAdo.GetFullInFoByRowid(CurrentRowid);

            GetMusicItem.TimeRange = originalWaveViewer.CutStart.Ticks + "-" + originalWaveViewer.CutEnd.Ticks;

            MusicsAdo.ModifyMusic(GetMusicItem);

            this.Close();
        }




        private void button4_Click(object sender, EventArgs e)
        {
            var GetMusicItem = MusicsAdo.GetFullInFoByRowid(CurrentRowid);

            GetMusicItem.TimeRange = "";

            MusicsAdo.ModifyMusic(GetMusicItem);

            this.Close();
        }








        // Resample



        // Encode G.711





    }
}
