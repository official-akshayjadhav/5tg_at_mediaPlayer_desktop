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

namespace _5tg_at_mediaPlayer_desktop
{
    /// <summary>
    /// Interaction logic for Track_Metadata.xaml
    /// </summary>
    public partial class Track_Metadata : Window
    {
        public Track_Metadata()
        {
            InitializeComponent();
            HeaderText.Text = "ADD Music";
        }

        Track track = null;
        List<Audio> audioList = null;
        public static Audio audio = null;

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
                if (HeaderText.Text == "Add Music")
                {
                    audio = audioList[0];
                    Global_Log.playBack.insertSong(audio, true);
                    MessageBox.Show("Song Successfully Load");
                }
                else if (HeaderText.Text == "Update Music")
                {
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
    }
}
