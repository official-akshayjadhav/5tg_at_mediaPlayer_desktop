using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using _5tg_at_mediaPlayer_desktop.FM;
using _5tg_at_mediaPlayer_desktop.Popup;
using _5tg_at_mediaPlayer_desktop.connection;

namespace _5tg_at_mediaPlayer_desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            playlist.Visibility = Visibility.Visible;
            // allSongs.Visibility = Visibility.Visible;

        }

        public string SelectedPlayList { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Center_Playlist_Loaded();
            //LoadAllSong();
            //Center_Playlist.Center_Playlist playlist = new Center_Playlist.Center_Playlist();
            //playlist.LoadAllSong();
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility
            if (Tg_Btn.IsChecked == true)
            {
                tt_allplaylist.Visibility = Visibility.Collapsed;
                tt_playlist.Visibility = Visibility.Collapsed;
                tt_allsongs.Visibility = Visibility.Collapsed;
                tt_audiorouting.Visibility = Visibility.Collapsed;
                tt_streaming.Visibility = Visibility.Collapsed;
                tt_features.Visibility = Visibility.Collapsed;
                tt_commands.Visibility = Visibility.Collapsed;
                tt_loghistory.Visibility = Visibility.Collapsed;
                tt_addmusic.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_allplaylist.Visibility = Visibility.Visible;
                tt_playlist.Visibility = Visibility.Visible;
                tt_allsongs.Visibility = Visibility.Visible;
                tt_audiorouting.Visibility = Visibility.Visible;
                tt_streaming.Visibility = Visibility.Visible;
                tt_features.Visibility = Visibility.Visible;
                tt_commands.Visibility = Visibility.Visible;
                tt_loghistory.Visibility = Visibility.Visible;
                tt_addmusic.Visibility = Visibility.Visible;
            }

        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            //img_bg.Opacity = 1;
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            //img_bg.Opacity = 0.3;
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void Playlistnames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OpenPlaylist(object sender, MouseButtonEventArgs e)
        {
            allSongs.Visibility = Visibility.Collapsed;
            loghistory.Visibility = Visibility.Collapsed;
            allplaylist.Visibility = Visibility.Collapsed;
            playlist.Visibility = Visibility.Visible;
        }

        private void OpenAllPlaylist(object sender, MouseButtonEventArgs e)
        {

            allplaylist.Visibility = Visibility.Visible;
            allSongs.Visibility = Visibility.Collapsed;
            loghistory.Visibility = Visibility.Collapsed;
            playlist.Visibility = Visibility.Collapsed;
        }

        private void OpenSongs(object sender, MouseButtonEventArgs e)
        {
            allplaylist.Visibility = Visibility.Collapsed;
            playlist.Visibility = Visibility.Collapsed;
            loghistory.Visibility = Visibility.Collapsed;
            allSongs.Visibility = Visibility.Visible;
        }

        private void openloghistory(object sender, MouseButtonEventArgs e)
        {
            allplaylist.Visibility = Visibility.Collapsed;
            playlist.Visibility = Visibility.Collapsed;
            allSongs.Visibility = Visibility.Collapsed;
            loghistory.Visibility = Visibility.Visible;
        }

        private void OpenHome(object sender, MouseButtonEventArgs e)
        {
            //FM_Custom obj = new FM_Custom();
            //obj.Show();
            try
            {
                if (Global_Log.fM_Custom == null)
                {
                    Global_Log.fM_Custom = new FM.FM_Custom();
                }

                Global_Log.fM_Custom.Show();
            }
            catch (Exception ex)
            {
                Global_Log.fM_Custom = new FM.FM_Custom();
                Global_Log.fM_Custom.Show();
            }
            this.Close();
        }

        private void addmusic_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Track_Metadata addMusic = new Track_Metadata();
            addMusic.ShowDialog();
        }
    }
}
