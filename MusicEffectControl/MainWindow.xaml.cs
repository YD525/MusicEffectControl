using MusicEffectControl.ConvertManage;
using MusicEffectControl.MusicManage;
using MusicEffectControl.UIManage;
using NAudio.CoreAudioApi;
using Sound_Editor;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicEffectControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void MusicEdit(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem)
            {
                MusicPlayer.EndMusic();
                int GetRowid = ConvertHelper.ObjToInt((sender as MenuItem).Tag);
                var GetMainForm = new MainForm();
                DeFine.ActiveEdit = GetMainForm;
                GetMainForm.Show();
                var GetInFo = MusicsAdo.GetFullInFoByRowid(GetRowid);
                GetMainForm.SetMp3File(GetRowid, GetInFo.Source, GetInFo.TimeRange);
            }
          
        }
        public void SetHotKey(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem)
            {
                int GetRowid = ConvertHelper.ObjToInt((sender as MenuItem).Tag);
                CheckHotKey NCheckHotKey = new CheckHotKey();
                NCheckHotKey.Owner = this;
                NCheckHotKey.SetChecker(GetRowid);
              
            }
        }

        public void DeleteMusic(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem)
            {
                int GetRowid = ConvertHelper.ObjToInt((sender as MenuItem).Tag);
                if (MusicsAdo.DeleteMusic(GetRowid))
                {
                    ReSetTable(CurrentTag);
                }
            }
        }

        public void TopSort(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem)
            {
                int GetRowid = ConvertHelper.ObjToInt((sender as MenuItem).Tag);
                if (MusicsAdo.SetMusicSortNo(GetRowid, MusicsAdo.GetMaxSortByType(CurrentTag) + 1))
                {
                    ReSetTable(CurrentTag);
                }
            }
        }

        public void BottomSort(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem)
            {
                int GetRowid = ConvertHelper.ObjToInt((sender as MenuItem).Tag);
                if (MusicsAdo.SetMusicSortNo(GetRowid, MusicsAdo.GetMinSortByType(CurrentTag) - 1))
                {
                    ReSetTable(CurrentTag);
                }
            }
        }

        public void UPSort(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem)
            {
                int GetRowid = ConvertHelper.ObjToInt((sender as MenuItem).Tag);
                var GetData = MusicHelper.GetMusicList(CurrentTag);

                for (int i = 0; i < GetData.Count; i++)
                {
                    if (GetData[i].Rowid == GetRowid)
                    {
                        if (i - 1 >= 0)
                        {
                            var GetTarget = GetData[i - 1];
                            GetData[i-1] = GetData[i];
                            GetData[i] = GetTarget;
                        }
                    }
                }
                int GetCount = (MusicsAdo.GetMusicCountByType(CurrentTag) * 2)+1;
                for (int i = 0; i < GetData.Count; i++)
                {
                    if (MusicsAdo.SetMusicSortNo(GetData[i].Rowid, GetCount))
                    {
                        GetCount = GetCount - 2;
                    }
                }

                ReSetTable(CurrentTag);
            }
        }

        public void DownSort(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem)
            {
                int GetRowid = ConvertHelper.ObjToInt((sender as MenuItem).Tag);
                var GetData = MusicHelper.GetMusicList(CurrentTag);

                for (int i = 0; i < GetData.Count; i++)
                {
                    if (GetData[i].Rowid == GetRowid)
                    {
                        if (i + 1 < GetData.Count)
                        {
                            var GetTargetSort = GetData[i + 1].SortNo;

                            if (MusicsAdo.SetMusicSortNo(GetRowid, GetTargetSort - 1))
                            {
                                ReSetTable(CurrentTag);
                                return;
                            }
                        }
                    }
                }
            }
        }

        public bool CanSet = false;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SoundSelect.Init();

            SoundDevice.Items.Clear();

            foreach (var Get in SoundSelect.Devices)
            {
                SoundDevice.Items.Add(Get.FullName);
            }
            SoundDevice.SelectedValue = SoundSelect.DefDevice.FullName;

            CanSet = true;
            DeFine.Init(this);

            if (DeFine.Setting.CanLoop)
            {
                CanLoop.IsChecked = true;
                MusicPlayer.CanLoop = true;
            }
            if (DeFine.Setting.UsingHotKey)
            {
                UsingHotKey = true;
                CanSetHotKey.IsChecked = true;
            }

            MusicVolumeControl.Dispatcher.BeginInvoke(new Action(() =>
            {
                EffectBar.Width = MusicVolumeControl.ActualWidth * (MusicPlayer.GetCurrentVolume() / 100);
            }));

            UIHelper.AnyMusicBlockClick += new Action<object, RoutedEventArgs>((Sender, E) =>
            {
                if (Sender is Button)
                {
                    int GetRowid = ConvertHelper.ObjToInt((Sender as Button).Tag);
                    var GetFullInFo = MusicsAdo.GetFullInFoByRowid(GetRowid);
                    MusicPlayer.PlaySound(GetFullInFo.MusicName,GetFullInFo.Source, GetFullInFo.TimeRange);
                }
            });

            UIHelper.AnyMusicBlockShowMenu += new Action<object, MouseButtonEventArgs>((Sender, E) =>
            {
                if (Sender is Button)
                {
                    int GetRowid = ConvertHelper.ObjToInt((Sender as Button).Tag);

                    ContextMenu NMenu = new ContextMenu();
                    NMenu.SetValue(ContextMenu.StyleProperty, Application.Current.Resources["DefaultContextMenu"]);

                    MenuItem UP = new MenuItem();
                    UP.Header = "上移";
                    UP.Click += UPSort;
                    UP.Tag = GetRowid;
                    NMenu.Items.Add(UP);

                    MenuItem Down = new MenuItem();
                    Down.Header = "下移";
                    Down.Click += DownSort;
                    Down.Tag = GetRowid;
                    NMenu.Items.Add(Down);

                    MenuItem Top = new MenuItem();
                    Top.Header = "置顶";
                    Top.Click += TopSort;
                    Top.Tag = GetRowid;
                    NMenu.Items.Add(Top);

                    MenuItem Bottom = new MenuItem();
                    Bottom.Header = "置底";
                    Bottom.Click += BottomSort;
                    Bottom.Tag = GetRowid;
                    NMenu.Items.Add(Bottom);

                    MenuItem HotKey = new MenuItem();
                    HotKey.Header = "自定义快捷键";
                    HotKey.Tag = GetRowid;
                    HotKey.Click += SetHotKey;
                    NMenu.Items.Add(HotKey);

                    MenuItem SetTimeRange = new MenuItem();
                    SetTimeRange.Header = "设置播放时间";
                    SetTimeRange.Click += MusicEdit;
                    SetTimeRange.Tag = GetRowid;
                    NMenu.Items.Add(SetTimeRange);

                    if (CurrentTag == "DIY音效")
                    {
                        MenuItem Delete = new MenuItem();
                        Delete.Header = "删除";
                        Delete.Tag = GetRowid;
                        Delete.Click += DeleteMusic;
                        NMenu.Items.Add(Delete);
                    }
                 
                    NMenu.IsOpen = true;
                }
            });
        }

        private void SetTopBtn(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                string GetBtnName = ConvertHelper.ObjToStr((sender as Button).Content);
                if (GetBtnName == "窗口置顶")
                {
                    this.Topmost = true;
                    (sender as Button).Content = "取消置顶";
                }
                else
                {
                    this.Topmost = false;
                    (sender as Button).Content = "窗口置顶";
                }
            }
        }

        private void AddMusic(object sender, RoutedEventArgs e)
        {
            new MusicAdder().Show();
        }

        private void SoundDevice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!CanSet) return;
            if (sender is ComboBox)
            {
                string GetFullName = ConvertHelper.ObjToStr((sender as ComboBox).SelectedValue);
                if (SoundSelect.ChangePlayDevice(GetFullName))
                {

                }
            }
        }

        public void ReSetTable(string Type)
        {
            List<Button> Btns = new List<Button>();
            foreach (var GetMusic in MusicHelper.GetMusicList(Type))
            {
                Btns.Add(UIHelper.AddMusicBlock(GetMusic.MusicName, GetMusic.Rowid,GetMusic.HotKey));
            }
            UIHelper.AddMusicFromControl(MusicLists, Btns);
        }

        public Border LastSelectBorder = null;
        public string CurrentTag = "主持音效";
        //Resources
        private void NavDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Grid)
            {
                Grid LockerGrid = (Grid)sender;
                Border SelectBorder = null;

                if (LastSelectBorder != null)
                {
                    LastSelectBorder.Opacity = 0.4;
                }

                foreach (var Get in LockerGrid.Children)
                {
                    if (Get is Border)
                    {
                        SelectBorder = Get as Border;
                        break;
                    }
                }

                SelectBorder.Opacity = 0.7;
                string GetTag = ConvertHelper.ObjToStr(LockerGrid.Tag);

                ReSetTable(GetTag);

                CurrentTag = GetTag;

                LastSelectBorder = SelectBorder;
            }
        }

        private void ChangeVolume(object sender, MouseButtonEventArgs e)
        {
            DontMove = false;
            new Thread(() =>
            {
                while (!DontMove)
                {
                    Thread.Sleep(100);
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        Point GetPoint = e.GetPosition(MusicVolumeControl);
                        if (GetPoint.X < 0)
                        {
                            GetPoint.X = 0;
                        }
                        if (GetPoint.X > MusicVolumeControl.ActualWidth)
                        {
                            GetPoint.X = MusicVolumeControl.ActualWidth;
                        }

                        EffectBar.Width = GetPoint.X;
                        double Rate = ConvertHelper.GetRate(GetPoint.X, MusicVolumeControl.ActualWidth);

                        MusicPlayer.SetCurrentVolume(Rate);
                    }));
                }
            }).Start();
        }

        bool DontMove = true;
        private void DontMoveVolume(object sender, MouseButtonEventArgs e)
        {
            DontMove = true;
        }

        private void MusicVolumeControl_MouseLeave(object sender, MouseEventArgs e)
        {
            DontMove = true;
        }

        public bool UsingHotKey = false;

        private void CanSetHotKey_Click(object sender, RoutedEventArgs e)
        {
            if (CanSetHotKey.IsChecked == true)
            {
                UsingHotKey = true;
            }
            else
            {
                UsingHotKey = false;
            }

            DeFine.Setting.UsingHotKey = UsingHotKey;
            DeFine.Setting.SetLocalSetting();
        }

        private void CanLoop_Click(object sender, RoutedEventArgs e)
        {
            if (CanLoop.IsChecked == true)
            {
                MusicPlayer.CanLoop = true;
            }
            else
            {
                MusicPlayer.CanLoop = false;
            }

            DeFine.Setting.CanLoop = MusicPlayer.CanLoop;
            DeFine.Setting.SetLocalSetting();
        }




        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (UsingHotKey)
            {
                var SearchData = MusicsAdo.QueryMusicHaveHotKey(CurrentTag);
                for (int i = 0; i < SearchData.Count; i++)
                {
                    if (SearchData[i].HotKey.Equals(e.Key.ToString()))
                    {
                        MusicPlayer.PlaySound(SearchData[i].MusicName, SearchData[i].Source, SearchData[i].TimeRange);
                    }
                }
            }
        }

        private void PauseMusic(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                string GetBtnName = ConvertHelper.ObjToStr((sender as Button).Content);
                if (GetBtnName == "暂停播放")
                {
                    MusicPlayer.PauseMusic(true);
                    (sender as Button).Content = "继续播放";
                }
                else
                {
                    MusicPlayer.PauseMusic(false);
                    (sender as Button).Content = "暂停播放";
                }
            }
        }

        private void EndMusic(object sender, RoutedEventArgs e)
        {
            MusicPlayer.EndMusic();
        }

        private void AddLocalMusics(object sender, RoutedEventArgs e)
        {
            new AddConstMusic().Show();
        }
    }
}
