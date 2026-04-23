using MusicEffectControl.ConvertManage;
using MusicEffectControl.MusicManage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static MusicEffectControl.MusicManage.MusicsAdo;

namespace MusicEffectControl
{
    /// <summary>
    /// Interaction logic for MusicAdder.xaml
    /// </summary>
    public partial class MusicAdder : Window
    {
        public MusicAdder()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LocalTags.Items.Clear();

            LocalTags.Items.Add("DIY音效");

            LocalTags.SelectedValue = "DIY音效";

            var GetTags = YiXunApi.QueryTypes();
            if (GetTags != null)
            {
                foreach (var Get in GetTags)
                {
                    Tags.Items.Add(Get.type);
                }
            }
        }

        public WebMusic[] SearchCache = null;
        private void Tags_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox)
            {
                string GetSelectValue = ConvertHelper.ObjToStr((sender as ListBox).SelectedValue);
                var GetMusics = YiXunApi.SearchMusic(GetSelectValue);
                SearchCache = GetMusics;
                MusicList.Items.Clear();

                foreach (var GetItem in GetMusics)
                {
                    MusicList.Items.Add(new
                    {
                        ID = GetItem.id,
                        MusicName = GetItem.name
                    });
                }
            }
        }

        public object LockerAdd = new object();
        private void AddMusicFromWeb(object sender, RoutedEventArgs e)
        {
            string GetTag = ConvertHelper.ObjToStr(LocalTags.SelectedValue);
            if (GetTag.Trim().Length > 0)
            {
                if (SearchCache != null)
                {
                    foreach (var GetItem in MusicList.SelectedItems)
                    {
                        string ID = ConvertHelper.ObjToStr(MusicList.SelectedItem.GetType().GetProperty("ID").GetValue(GetItem, null).ToString());

                        foreach (var GetWebSource in SearchCache)
                        {
                            if (GetWebSource.id.Equals(ID))
                            {
                                string SetLocalPath = DeFine.GetFullPath(@"\Resources\" + GetWebSource.name);

                                new Thread(() =>
                                {
                                    lock (LockerAdd)
                                    {
                                        if (!File.Exists(SetLocalPath))
                                        {
                                            new MusicPlayer().HttpDownload(GetWebSource.url, SetLocalPath);
                                        }

                                        string GetName = GetWebSource.name.Split('.')[0];
                                        MusicsAdo.AddMusic(new MusicsAdo.MusicItem(
                                            GetTag,
                                            GetName,
                                            GetWebSource.len,
                                            0,
                                            "",
                                            SetLocalPath,
                                            ""
                                            ));
                                        DeFine.WorkingWin.Dispatcher.Invoke(new Action(() => {
                                            DeFine.WorkingWin.ReSetTable(GetTag);
                                        }));
                                    }
                                }).Start();
                            }
                        }
                    }
                }
            }
        }

        private void AddLocal(object sender, RoutedEventArgs e)
        {
            string GetTag = ConvertHelper.ObjToStr(LocalTags.SelectedValue);

            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string FilePath = openFileDialog1.FileName;
                string FileName = FilePath.Substring(FilePath.LastIndexOf(@"\") + @"\".Length);
                string GetName = FileName.Split('.')[0];

                MusicsAdo.AddMusic(new MusicItem(
                                          GetTag,
                                          GetName,
                                          ConvertHelper.ObjToInt(new FileInfo(FilePath).Length/1024),
                                          0,
                                          "",
                                          FilePath,
                                          ""
                                          ));
            }
            DeFine.WorkingWin.ReSetTable(GetTag);
        }
    }
}
