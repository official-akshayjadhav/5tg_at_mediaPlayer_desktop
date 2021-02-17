using _5tg_at_mediaPlayer_desktop.connection;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using MediaInfo;


namespace _5tg_at_mediaPlayer_desktop.Popup
{
    /// <summary>
    /// Interaction logic for Track_Metadata.xaml
    /// </summary>
    public partial class Track_Metadata : Window
    {

        public Track_Metadata()
        {
            InitializeComponent();
            HeaderText.Text = "Add Music";
        }

        //Track track = null;
        List<Audio> audioList = null;


        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                audioList = new List<Audio>();
                MediaInfoWrapper track = new MediaInfoWrapper(openFileDialog.FileName);

                string title = track.Tags.Title;
                string fileNames = openFileDialog.FileName;
                int fileSize = (int)track.Size;
                string fType = track.ToString();
                string filePath = track.Tags.SubTrack;
                string times = track.Duration.ToString();

                TimeSpan tt = TimeSpan.FromMilliseconds(track.Duration);

                Global_Log.startTimeInSec = 0;
                Global_Log.endTimeInSec = Convert.ToInt32(tt.TotalSeconds);

                ConvertFileDetails convertFileDetails = new ConvertFileDetails();
                convertFileDetails.InputFilePath = System.IO.Path.GetFullPath(openFileDialog.FileNames[0]);
                { }
                convertFileDetails.OutputFilePath = title + ".shd";
                { }
                string outputFileName = FfmpegHandler.convertFile2(convertFileDetails, 0);
                { }

                //convertFileDetails.InputFilePath = System.IO.Path.GetFullPath(openFileDialog.FileNames[0]);


                //byte[] fileDataBytes = File.ReadAllBytes(System.IO.Path.GetFullPath(openFileDialog.FileNames[0]));
                //String fileDataBase64 = Convert.ToBase64String(fileDataBytes);

                String fileDataBase64 = outputFileName;
                Global_Log.newSongTrack = fileDataBase64;

                Trim.Trim obj_Trim = new Trim.Trim(Global_Log.endTimeInSec);
                Fade_in_out.Fade_in_out obj_Fade_in_out = new Fade_in_out.Fade_in_out(Global_Log.endTimeInSec);


                fileName.Text = fileNames;
                Song_Name.Text = title;
                File_Type.Text = " audio/mpeg ";
                File_Size.Text = fileSize.ToString();
                Duration.Text = times;
                Start_Time.Text = TimeSpan.FromSeconds(0).ToString();
                End_Time.Text = TimeSpan.FromMilliseconds(track.Duration).ToString();

                TimeSpan ttt = TimeSpan.FromSeconds(Global_Log.endTimeInSec - 10);

                audioList.Add(new Audio()
                {
                    UID = "000",
                    Title = track.Tags.Title,
                    FileName = track.Tags.Title,
                    Filesize = 0,
                    Filetype = "audio",
                    Filepath = filePath,
                    Duration = TimeSpan.FromMilliseconds(track.Duration),
                    Track = fileDataBase64,
                    Trim_Start = TimeSpan.FromSeconds(Global_Log.startTimeInSec),
                    Trim_End = TimeSpan.FromSeconds(Global_Log.endTimeInSec),
                    Intro = TimeSpan.FromSeconds(Global_Log.startFadeInSec),
                    EOM = TimeSpan.FromSeconds(Global_Log.endFadeInSec)
                });
            }
        }

        private void AddSong_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Global_Log.bottom_Media_Control == null)
                {
                    Global_Log.bottom_Media_Control = new Bottom_Media_Control.Bottom_Media_Control();
                }

                Global_Log.bottom_Media_Control.stopSong();

                Audio audio = null;
                if (HeaderText.Text == "Add Music")
                {
                    audio = audioList[0];

                    audio.Trim_Start = TimeSpan.FromSeconds(Global_Log.startTimeInSec);
                    audio.Trim_End = TimeSpan.FromSeconds(Global_Log.endTimeInSec);
                    audio.Intro = TimeSpan.FromSeconds(Global_Log.startFadeInSec);
                    audio.EOM = TimeSpan.FromSeconds(Global_Log.endFadeInSec);
                    { }
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
            Start_Time.Text = Global_Log.audio.Trim_Start.ToString();
            End_Time.Text = Global_Log.audio.Trim_End.ToString();

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
            Start_Time.IsEnabled = boolValue;
            End_Time.IsEnabled = boolValue;
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
