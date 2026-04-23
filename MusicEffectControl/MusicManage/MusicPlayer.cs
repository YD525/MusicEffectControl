
using AudioSwitcher.AudioApi.CoreAudio;
using MusicEffectControl.ConvertManage;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Windows.Interop;
using System.Windows.Media;
using System.Xml.Linq;
using static MusicEffectControl.MusicManage.MusicsAdo;

namespace MusicEffectControl.MusicManage
{
  

    public class MusicPlayer
    {
        

        public static byte[] ToArray(Stream stream)
        {
            byte[] buffer = new byte[4096];
            int reader = 0;
            MemoryStream memoryStream = new MemoryStream();
            while ((reader = stream.Read(buffer, 0, buffer.Length)) != 0)
                memoryStream.Write(buffer, 0, reader);
            return memoryStream.ToArray();
        }

        public static WaveStream ConvertMp3ToWav(string _inPath_)
        {
            using (Mp3FileReader mp3 = new Mp3FileReader(_inPath_))
            {
                using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3))
                {
                    return pcm;
                }
            }
        }

        public static double GetCurrentVolume()
        {
            return SoundSelect.DefDevice.Volume;
        }

        public static void SetCurrentVolume(double Volume)
        {
            SoundSelect.DefDevice.Volume = Volume;
            SoundSelect.DefDevice.Mute(false);
        }


        public static MemoryStream GetStream(string Path)
        {
            FileInfo info = new FileInfo(Path);
            FileStream fs = info.OpenRead();
            byte[] buffer = new byte[info.Length + 1];
            fs.Read(buffer, 0, buffer.Length);
            MemoryStream ms = new MemoryStream(buffer);
            ms.Write(buffer, 0, buffer.Length);
            return ms;
        }

        public MemoryStream CutMp3(string filePath, int start, int end)
        {
            MemoryStream MemStream = new MemoryStream();

            var s = TimeSpan.FromSeconds(start);
            var e = TimeSpan.FromSeconds(end);
            try
            {
                using (var reader = new Mp3FileReader(filePath))
                {
                    using (var writer = MemStream)
                    {
                        Mp3Frame frame;
  
                        while ((frame = reader.ReadNextFrame()) != null)
                            if (reader.CurrentTime >= s)
                            {
                                if (reader.CurrentTime <= e)
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

            return MemStream;
        }

        public static bool CanLoop = false;

        public static string CurrentPlayName = "";
        public static string CurrentPath = "";

        public static MediaPlayer CurrentPlayer = new MediaPlayer();

        public static void PlaySound(string Name,string Path,string TimeRange)
        {
            CurrentPlayName = Name;
            CurrentPath = Path;
            if (TimeRange.Trim().Length > 0)
            {
                if (File.Exists(DeFine.GetFullPath(@"\Resources\" + "Change_" + CurrentPlayName + ".mp3")))
                {
                    CurrentPath = DeFine.GetFullPath(@"\Resources\" + "Change_" + CurrentPlayName + ".mp3");
                }
            }
            CurrentPlayer.Open(new Uri(CurrentPath));
            CurrentPlayer.Play();
            DeFine.WorkingWin.Title = "Playing " + CurrentPlayName;
            CurrentPlayer.MediaEnded += PlayEnd;
        }

        public static void PlayEnd(object sender, EventArgs e)
        {
            if (CanLoop)
            {
                CurrentPlayer.Open(new Uri(CurrentPath));
                CurrentPlayer.Play();
                DeFine.WorkingWin.Title = "Playing " + CurrentPlayName;
            }
            else
            {
                CurrentPlayName = string.Empty;
                DeFine.WorkingWin.Title = "MusicEffectControl";
            }
        }
        public static void EndMusic()
        {
            CurrentPlayer.Stop();

            CurrentPath = string.Empty;
            CurrentPlayName = string.Empty;

            DeFine.WorkingWin.Title = "MusicEffectControl";
        }

        public static void PauseMusic(bool State)
        {
            if (State)
            {
                CurrentPlayer.Pause();
            }
            else
            {
                CurrentPlayer.Play();
            }
        }

        public void HttpDownload(string Url, string SavePath)
        {
            NextTry:
            try
            {
                using (var Web = new WebClient())
                {
                    var GetTask = Web.DownloadFileTaskAsync(Url, SavePath);
                    GetTask.Wait();
                }
            }
            catch
            {
                Thread.Sleep(1000);
                goto NextTry;
            }
        }
    }


    public class SoundSelect
    {
        public static IEnumerable<CoreAudioDevice> Devices;
        public static CoreAudioDevice DefDevice;
        public static void Init()
        {
            Devices = new CoreAudioController().GetPlaybackDevices();
            DefDevice = new CoreAudioController().GetDefaultDevice(AudioSwitcher.AudioApi.DeviceType.Playback,AudioSwitcher.AudioApi.Role.Console);
        }

        public static List<MMDevice> GetPlayDevice()
        {
            MMDeviceEnumerator enumerator;
            enumerator = new MMDeviceEnumerator();
            IEnumerable<MMDevice> playBackList;
            playBackList = enumerator.EnumerateAudioEndPoints(DataFlow.Render, NAudio.CoreAudioApi.DeviceState.Active).ToArray();
            return playBackList.ToList();
        }

        public static MMDevice GetDefPlayDevice()
        {
            var enumerator = new MMDeviceEnumerator();
            return enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
        }

        public static bool ChangePlayDevice(string FullName)
        {
            foreach (CoreAudioDevice d in Devices)
            {
                if (FullName.Equals(d.FullName.ToString()))
                {
                    DefDevice = d;
                    d.SetAsDefault();
                    return true;
                } 
            }
            return false;
        }
    }
}
