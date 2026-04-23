using MusicEffectControl.MusicManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for CheckHotKey.xaml
    /// </summary>
    public partial class CheckHotKey : Window
    {
        public CheckHotKey()
        {
            InitializeComponent();
        }


        public string CurrentKey = "";

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (CurrentMusic != null)
            {
                HotKeyLab.Content = e.Key;
                CurrentKey = e.Key.ToString();
            }
        }

        public MusicItem CurrentMusic = null;
        public void SetChecker(int Rowid)
        {
            this.Show();
            CurrentMusic = MusicsAdo.GetFullInFoByRowid(Rowid);
            this.Title ="配置:" + CurrentMusic.MusicName + " 键";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentKey.Trim().Length > 0)
            {
                if (CurrentMusic != null)
                {
                    CurrentMusic.HotKey = CurrentKey;
                    int Rowid = 0;
                    if (!MusicsAdo.CanBinDingHotKey(CurrentMusic.Type, CurrentKey, ref Rowid))
                    {
                        var GetOldMusic = MusicsAdo.GetFullInFoByRowid(Rowid);
                        GetOldMusic.HotKey = "";
                        MusicsAdo.ModifyMusic(GetOldMusic);
                    }
                    MusicsAdo.ModifyMusic(CurrentMusic);
                    DeFine.WorkingWin.ReSetTable(CurrentMusic.Type);
                    this.Close();
                }
            }
        }
    }
}
