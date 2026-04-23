using MusicEffectControl.ConvertManage;
using MusicEffectControl.MusicManage;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace MusicEffectControl
{
    /// <summary>
    /// Interaction logic for AddConstMusic.xaml
    /// </summary>
    public partial class AddConstMusic : Window
    {
        public AddConstMusic()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConstTags.Items.Clear();
            ConstTags.Items.Add("主持音效");
            ConstTags.Items.Add("喊麦音效");
            ConstTags.Items.Add("搞笑音效");
            ConstTags.Items.Add("游戏音效");
            ConstTags.Items.Add("生活音效");

            if (DeFine.WorkingWin.CurrentTag != "DIY音效")
            {
                ConstTags.SelectedValue = DeFine.WorkingWin.CurrentTag;
            }
            else
            {
                ConstTags.SelectedValue = "主持音效";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(FilePath.Text))
            {
                MusicHelper.AddMusicByPath(FilePath.Text, ConvertHelper.ObjToStr(ConstTags.SelectedValue));
                DeFine.WorkingWin.ReSetTable(ConvertHelper.ObjToStr(ConstTags.SelectedValue));
                this.Close();
            }
        }
    }
}
