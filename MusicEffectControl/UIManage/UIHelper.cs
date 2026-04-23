using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MusicEffectControl.UIManage
{
    public class UIHelper
    {
        public static Action<object, RoutedEventArgs> AnyMusicBlockClick = null;
        public static Action<object, MouseButtonEventArgs> AnyMusicBlockShowMenu = null;

        public static void MusicBlockClick(object Sender, RoutedEventArgs e)
        {
            if (AnyMusicBlockClick != null)
            {
                AnyMusicBlockClick.Invoke(Sender, e);
            }
        }

        public static void ShowMenu(object Sender, MouseButtonEventArgs e)
        {
            if (AnyMusicBlockShowMenu != null)
            {
                AnyMusicBlockShowMenu.Invoke(Sender,e);
            }
        }

        public static Button AddMusicBlock(string MusicName, int Rowid,string HotKey)
        {
            Button NewBtn = new Button();
            if (HotKey.Trim().Length == 0)
            {
                NewBtn.Content = MusicName;
            }
            else
            {
                NewBtn.Content = "(" + HotKey  + ")" + MusicName;
            }
            NewBtn.FontSize = 10.5;
            NewBtn.Tag = Rowid;
            NewBtn.Margin = new Thickness(5,2,5,2);
            NewBtn.Padding = new Thickness(8,5,8,5);
            NewBtn.Cursor = Cursors.Hand;
            NewBtn.Click += MusicBlockClick;
            NewBtn.MouseRightButtonDown += ShowMenu;
            return NewBtn;
        }


        public static void AddMusicFromControl(Grid ParentGrid, List<Button> Btns)
        {
            StackPanel GetMain = null;
            for (int i = 0; i < ParentGrid.Children.Count; i++)
            {
                if (ParentGrid.Children[i] is ScrollViewer)
                {
                    GetMain = (ParentGrid.Children[i] as ScrollViewer).Content as StackPanel;
                    break;
                }
            }
            if (GetMain != null)
            {
                GetMain.Dispatcher.Invoke(new Action(() =>
                {
                    GetMain.Children.Clear();
                }));

                Queue<Button> BtnQueues = new Queue<Button>();

                foreach (var GetBtnItem in Btns)
                {
                    BtnQueues.Enqueue(GetBtnItem);
                }

                List<Grid> Grids = new List<Grid>();

                while (BtnQueues.Count > 0)
                {
                    Grid OneLine = new Grid();

                    for (int i = 0; i < DeFine.DefMusicViewColumnCount; i++)
                    {
                        if (BtnQueues.Count > 0)
                        {
                            ColumnDefinition Col1st = new ColumnDefinition();
                            Col1st.Width = new GridLength(1, GridUnitType.Star);
                            Col1st.Name = "M" + i.ToString();
                            OneLine.ColumnDefinitions.Add(Col1st);

                            var GetBtn = BtnQueues.Dequeue();
                            OneLine.Children.Add(GetBtn);
                            Grid.SetColumn(GetBtn,i);
                        }
                        else
                        {
                            ColumnDefinition Col1st = new ColumnDefinition();
                            Col1st.Width = new GridLength(1, GridUnitType.Star);
                            Col1st.Name = "M" + i.ToString();
                            OneLine.ColumnDefinitions.Add(Col1st);
                        }
                    }

                    Grids.Add(OneLine);
                }

             

                foreach (var GetGrid in Grids)
                {
                    GetMain.Dispatcher.Invoke(new Action(() =>
                    {
                        GetMain.Children.Add(GetGrid);
                    }));
                }
            }
        }
    }
}
