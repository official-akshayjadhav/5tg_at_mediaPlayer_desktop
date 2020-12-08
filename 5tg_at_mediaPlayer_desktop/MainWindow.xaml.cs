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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _5tg_at_mediaPlayer_desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        public MainWindow()
        {
            InitializeComponent();
            Uri music = new Uri("C:\\Users\\shubh\\Desktop\\part time\\1st.mp3");
            mediaPlayer.Open(music);
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

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 1;
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 0.3;
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        //play the media

        private void PlayMedia_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Play();
            InitializePropertyValues();
        }

        private void PauseMedia_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Pause();
        }

        private void StopMedia_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Stop();
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaPlayer.Volume = (double)volumeSlider.Value;
        }

        private void SpeedRatioSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaPlayer.SpeedRatio = (double)speedRatioSlider.Value;
        }

        private void TimelineSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int SliderValue = (int)timelineSlider.Value;
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            mediaPlayer.Position = ts;
        }


        void InitializePropertyValues()
        {
            mediaPlayer.Volume = (double)volumeSlider.Value;
            mediaPlayer.SpeedRatio = (double)speedRatioSlider.Value;
        }

        private void MyMediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            timelineSlider.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        private void MyMediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Stop();
        }
    }
}
