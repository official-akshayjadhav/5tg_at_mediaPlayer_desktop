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
using System.Windows.Threading;

namespace _5tg_at_mediaPlayer_desktop.Bottom_Media_Control
{
    /// <summary>
    /// Interaction logic for Bottom_Media_Control.xaml
    /// </summary>
    public partial class Bottom_Media_Control : UserControl
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private bool userIsDraggingSlider = false;
       
        public Bottom_Media_Control()
        {
            InitializeComponent();
            Uri music = new Uri("C:\\Users\\shubh\\Desktop\\part time\\1st.mp3");
            mediaPlayer.Open(music);
            song.Text = music.ToString();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        //play the media

        /*private void PlayMedia_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Play();
            InitializePropertyValues();
        }
        */
        /*private void PlayMedia_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //mediaPlayer.Pause();
        }
        */
       
        
        /*private void PauseMedia_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Pause();
        }

        private void StopMedia_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Stop();
        }
        */
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

        private void SliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
        }

        private void SliProgress_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            mediaPlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void SliProgress_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }
        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mediaPlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if ((mediaPlayer.Source != null) && (mediaPlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                sliProgress.Minimum = 0;
                sliProgress.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = mediaPlayer.Position.TotalSeconds;
            }
        }

        /*private void PlayMedia_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Play();
            InitializePropertyValues();
        }*/


        private void PlayPreviousMedia_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }
        private void PlayMedia_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Play();
            InitializePropertyValues();
            
        }

        private void PauseMedia_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Pause();
        }

        private void StopMedia_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Stop();
        }

        private void PlayNextMedia_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void PlayRepeatMedia_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Position = TimeSpan.Zero;
            mediaPlayer.Play();
        }
    }
}
