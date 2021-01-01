using _5tg_at_mediaPlayer_desktop.connection;
using ATL;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using MediaInfoLib;

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
            /*String path = "C:\\Users\\shubh\\Desktop\\part time\\1st.mp3";
            String ToDisplay;
            MediaInfo MI = new MediaInfo();

            ToDisplay = MI.Option("Info_Version", "0.7.0.0;MediaInfoDLL_Example_CS;0.7.0.0");
            if (ToDisplay.Length == 0)
            {
                richTextBox1.Text = "MediaInfo.Dll: this version of the DLL is not compatible";
                return;
            }

            //Information about MediaInfo
            ToDisplay += "\r\n\r\nInfo_Parameters\r\n";
            ToDisplay += MI.Option("Info_Parameters");

            ToDisplay += "\r\n\r\nInfo_Capacities\r\n";
            ToDisplay += MI.Option("Info_Capacities");

            ToDisplay += "\r\n\r\nInfo_Codecs\r\n";
            ToDisplay += MI.Option("Info_Codecs");

            //An example of how to use the library
            ToDisplay += "\r\n\r\nOpen\r\n";
            MI.Open(path);

            ToDisplay += "\r\n\r\nInform with Complete=false\r\n";
            MI.Option("Complete");
            ToDisplay += MI.Inform();

            ToDisplay += "\r\n\r\nInform with Complete=true\r\n";
            MI.Option("Complete", "1");
            ToDisplay += MI.Inform();

            ToDisplay += "\r\n\r\nCustom Inform\r\n";
            MI.Option("Inform", "General;File size is %FileSize% bytes");
            ToDisplay += MI.Inform();

            ToDisplay += "\r\n\r\nGet with Stream=General and Parameter='FileSize'\r\n";
            ToDisplay += MI.Get(0, 0, "FileSize");

            ToDisplay += "\r\n\r\nGet with Stream=General and Parameter=46\r\n";
            ToDisplay += MI.Get(0, 0, 46);

            ToDisplay += "\r\n\r\nCount_Get with StreamKind=Stream_Audio\r\n";
            ToDisplay += MI.Count_Get(StreamKind.Audio);

            ToDisplay += "\r\n\r\nGet with Stream=General and Parameter='AudioCount'\r\n";
            ToDisplay += MI.Get(StreamKind.General, 0, "AudioCount");

            ToDisplay += "\r\n\r\nGet with Stream=Audio and Parameter='StreamCount'\r\n";
            ToDisplay += MI.Get(StreamKind.Audio, 0, "StreamCount");

            ToDisplay += "\r\n\r\nClose\r\n";
            MI.Close();
            richTextBox1.Text = ToDisplay;
            Console.WriteLine(ToDisplay);
            */
        }

        Track track = null;
        List<Audio> audioList = null;



        public void ShowdataForMediaLibInfo(String path) {

            String ToDisplay;
            MediaInfo MI = new MediaInfo();

            ToDisplay = MI.Option("Info_Version", "0.7.0.0;MediaInfoDLL_Example_CS;0.7.0.0");
            if (ToDisplay.Length == 0)
            {
                richTextBox1.Text = "MediaInfo.Dll: this version of the DLL is not compatible";
                return;
            }

            //Information about MediaInfo
            ToDisplay += "\r\n\r\nInfo_Parameters\r\n";
            ToDisplay += MI.Option("Info_Parameters");

            ToDisplay += "\r\n\r\nInfo_Capacities\r\n";
            ToDisplay += MI.Option("Info_Capacities");

            ToDisplay += "\r\n\r\nInfo_Codecs\r\n";
            ToDisplay += MI.Option("Info_Codecs");

            //An example of how to use the library
            ToDisplay += "\r\n\r\nOpen\r\n";
            MI.Open(path);

            ToDisplay += "\r\n\r\nInform with Complete=false\r\n";
            MI.Option("Complete");
            ToDisplay += MI.Inform();

            ToDisplay += "\r\n\r\nInform with Complete=true\r\n";
            MI.Option("Complete", "1");
            ToDisplay += MI.Inform();

            ToDisplay += "\r\n\r\nCustom Inform\r\n";
            MI.Option("Inform", "General;File size is %FileSize% bytes");
            ToDisplay += MI.Inform();

            ToDisplay += "\r\n\r\nGet with Stream=General and Parameter='FileSize'\r\n";
            ToDisplay += MI.Get(0, 0, "FileSize");

            ToDisplay += "\r\n\r\nGet with Stream=General and Parameter=46\r\n";
            ToDisplay += MI.Get(0, 0, 46);

            ToDisplay += "\r\n\r\nCount_Get with StreamKind=Stream_Audio\r\n";
            ToDisplay += MI.Count_Get(StreamKind.Audio);

            ToDisplay += "\r\n\r\nGet with Stream=General and Parameter='AudioCount'\r\n";
            ToDisplay += MI.Get(StreamKind.General, 0, "AudioCount");

            ToDisplay += "\r\n\r\nGet with Stream=Audio and Parameter='StreamCount'\r\n";
            ToDisplay += MI.Get(StreamKind.Audio, 0, "StreamCount");

            ToDisplay += "\r\n\r\nClose\r\n";
            MI.Close();
            richTextBox1.Text = ToDisplay;
            Console.WriteLine(ToDisplay);


        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                audioList = new List<Audio>();
                track = new Track(openFileDialog.FileName);
                ShowdataForMediaLibInfo(track.ToString());
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
                Start_Time.Text = TimeSpan.FromSeconds(0).ToString();
                End_Time.Text = TimeSpan.FromSeconds(track.Duration).ToString();
                
               
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
