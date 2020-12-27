using _5tg_at_mediaPlayer_desktop.connection;
using ATL;
using Microsoft.Win32;
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


namespace _5tg_at_mediaPlayer_desktop.Popup
{
    /// <summary>
    /// Interaction logic for Track_Metadata.xaml
    /// </summary>
   public partial class Track_Metadata : Window
    {
        
        public static readonly DependencyProperty MinProperty = DependencyProperty.Register("Min", typeof(double), typeof(MainWindow), new PropertyMetadata(0d));
        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register("Max", typeof(double), typeof(MainWindow), new PropertyMetadata(100d));
        public static readonly DependencyProperty StartProperty = DependencyProperty.Register("Start", typeof(double), typeof(MainWindow), new PropertyMetadata(20d));
        public static readonly DependencyProperty EndProperty = DependencyProperty.Register("End", typeof(double), typeof(MainWindow), new PropertyMetadata(85d));

        public double Max
        {
            get => (double)GetValue(MaxProperty);
            set => SetValue(MaxProperty, value);

        }

        public double Min
        {
            get => (double)GetValue(MinProperty);
            set => SetValue(MinProperty, value);
        }

        public double Start
        {
            get => (double)GetValue(StartProperty);
            set => SetValue(StartProperty, value);
        }

        public double End
        {
            get => (double)GetValue(EndProperty);
            set => SetValue(EndProperty, value);
        }



        public Track_Metadata()
        {
            InitializeComponent();
            HeaderText.Text = "Add Music";
        }

        Track track = null;
        List<Audio> audioList = null;
        
        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                audioList = new List<Audio>();
                track = new Track(openFileDialog.FileName);

                string title = track.Title;
                string fileNames = track.Path;
                int fileSize = 0000;
                string fType = " audio/mpeg ";
                string filePath = track.Path;
                string times = track.DurationMs.ToString();

                byte[] fileDataBytes = File.ReadAllBytes(System.IO.Path.GetFullPath(openFileDialog.FileNames[0]));
                String fileDataBase64 = Convert.ToBase64String(fileDataBytes);

                fileName.Text = fileNames;
                Song_Name.Text = title;
                File_Type.Text = " audio/mpeg ";
                File_Size.Text = fileSize.ToString();
                Duration.Text = times;
                //Start_Time.Text = TimeSpan.FromSeconds(0).ToString();
                //End_Time.Text = TimeSpan.FromSeconds(track.Duration).ToString();
                
               
                audioList.Add(new Audio()
                {
                    UID = "000",
                    Title = track.Title,
                    FileName = track.Title,
                    Filesize = 0,
                    Filetype = "audio",
                    Filepath = track.Path,
                    Duration = TimeSpan.FromSeconds(track.Duration),
                    Track = fileDataBase64,
                    Trim_Start = TimeSpan.FromSeconds(0),
                    Trim_End = TimeSpan.FromSeconds(track.Duration),
                });
            }
        }

        private void AddSong_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Audio audio = null;
                if (HeaderText.Text == "Add Music")
                {
                    audio = audioList[0];
                    Global_Log.playBack.insertSong(audio, true);
                    MessageBox.Show("Song Successfully Load");
                }
                else if (HeaderText.Text == "Update Music")
                {
                    audio = Global_Log.audio;                    
                    audio.FileName = fileName.Text;
                    audio.Title = Song_Name.Text;
                    //audio.Trim_Start = Start_Time.Text;
                    //audio.Trim_End = End_Time.Text;
                    Global_Log.playBack.insertSong(audio, false);                    
                }
            }
            catch (Exception ex)
            { }
            finally
            {
                this.Close();
            }
        }

        internal void updateSong()
        {
            setEnabling(true);
            Browse.IsEnabled = false;
            HeaderText.Text = "Update Music";
            AddSong.Content = "Update Audio";

            fileName.Text = Global_Log.audio.Title;
            Song_Name.Text = Global_Log.audio.Title;
            //Start_Time.Text = Global_Log.audio.Trim_Start.ToString();
            //End_Time.Text = Global_Log.audio.Trim_End.ToString();

            File_Type.Text = Global_Log.audio.Filetype.ToString();
            File_Size.Text = Global_Log.audio.Filesize.ToString();
            Duration.Text = Global_Log.audio.Duration.ToString();

            this.ShowDialog();
        }

        internal void insertSong()
        {
            setEnabling(false);
            Browse.IsEnabled = true;
            HeaderText.Text = "Add Music";
            this.ShowDialog();
        }

        public void setEnabling(bool boolValue)
        {
            fileName.IsEnabled = boolValue;
            Song_Name.IsEnabled = boolValue;
            //Start_Time.IsEnabled = boolValue;
            //End_Time.IsEnabled = boolValue;
        }

        /*private void RangeSlider_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void RangeSlider_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is FunwayControls.RangeSlider rs)
            {
                //start.Text = rs.Start.ToString();
                //end.Text = rs.End.ToString();
            }
            //Start_Time.Text = sender.;
        }
        private static void OnDragStartedEvent(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            if (sender is FunwayControls.RangeSlider rs)
            {
                //rs.OnDragStartedEvent(e);
            }
        }*/
    }
}
