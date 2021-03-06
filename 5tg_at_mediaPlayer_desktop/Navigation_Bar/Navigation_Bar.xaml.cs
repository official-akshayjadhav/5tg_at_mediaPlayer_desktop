﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _5tg_at_mediaPlayer_desktop.Navigation_Bar
{
    /// <summary>
    /// Interaction logic for Navigation_Bar.xaml
    /// </summary>
    public partial class Navigation_Bar : UserControl
    {
        public Navigation_Bar()
        {
            InitializeComponent();
        }
        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility

            if (Tg_Btn.IsChecked == true)
            {
                tt_playlist.Visibility = Visibility.Collapsed;
                tt_allsongs.Visibility = Visibility.Collapsed;
                tt_audiorouting.Visibility = Visibility.Collapsed;
                tt_streaming.Visibility = Visibility.Collapsed;
                tt_features.Visibility = Visibility.Collapsed;
                tt_commands.Visibility = Visibility.Collapsed;
                tt_loghistory.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_playlist.Visibility = Visibility.Visible;
                tt_allsongs.Visibility = Visibility.Visible;
                tt_audiorouting.Visibility = Visibility.Visible;
                tt_streaming.Visibility = Visibility.Visible;
                tt_features.Visibility = Visibility.Visible;
                tt_commands.Visibility = Visibility.Visible;
                tt_loghistory.Visibility = Visibility.Visible;
            }
        }
        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }
        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            //img_bg.Opacity = 1;
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            //img_bg.Opacity = 0.3;
        }

    }
}
