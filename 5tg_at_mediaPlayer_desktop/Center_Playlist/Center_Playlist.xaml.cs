﻿using _5tg_at_mediaPlayer_desktop.connection;
using _5tg_at_mediaPlayer_desktop.Playlist;
using _5tg_at_mediaPlayer_desktop.Popup;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace _5tg_at_mediaPlayer_desktop.Center_Playlist
{

    /// <summary>
    /// Interaction logic for Center_Playlist.xaml
    /// </summary>
    public partial class Center_Playlist : UserControl
    {

        public Center_Playlist()
        {
            InitializeComponent();
            loadPlaylist();
        }

        public void loadPlaylist()
        {
            if (Global_Log.playBack == null)
            {
                Global_Log.playBack = new PlayBack();
            }

            List<Playlists> list_Playlists = new List<Playlists>();
            try
            {
                DataTable dt = Global_Log.connectionClass.retriveData("select PID,name,CAST(createdDate as date), TotalSong from playlists", "playlists");

                int count = dt.Rows.Count;
                Playlists playlists = new Playlists();
                for (int i = 0; i < count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    try
                    {
                        list_Playlists.Add(new Playlists()
                        {
                            PID = Convert.ToInt32(dr.ItemArray[0]),
                            Name = dr.ItemArray[1].ToString(),
                            Date = dr.ItemArray[2].ToString(),
                            TotalSong = Convert.ToInt32(dr.ItemArray[3])
                        });
                    }
                    catch (Exception ex)
                    { }
                }
                playlistnames.ItemsSource = list_Playlists;
            }
            catch (Exception ex)
            { }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var image = e.AddedItems[0] as ComboBoxItem;
            string currentOperation = image.Content.ToString();

            var ttp = sender as ComboBox;
            Playlists playlists = null;
            if (image != null && ttp.Tag is Playlists)
            {
                playlists = (Playlists)ttp.Tag;
            }

            if (playlists != null)
            {
                Create_Playlist create_Playlist = new Create_Playlist();
                Global_Log.pID = playlists.PID;
                Global_Log.playlistName = playlists.Name;

                if (currentOperation == "Update")
                {
                    create_Playlist.SetProperty(playlists.Name.ToString());
                }
                else if (currentOperation == "Dalete")
                {
                    create_Playlist.deletePlaylist(playlists.PID);
                }
            }
            loadPlaylist();
        }

        //public object ConfigurationManager { get; private set; }

        private void View_all_playlist_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Playlistnames_LoadingRowDetails(object sender, DataGridRowDetailsEventArgs e)
        {
            /*DataTable dt = new DataTable();
            dt.Columns.Add("Sr. No.", typeof(String));
            dt.Columns.Add("ID", typeof(String));
            dt.Columns.Add("Name", typeof(String));
            dt.Columns.Add("Count",typeof(String));
            dt.Columns.Add("Controls",typeof(String));
            dt.Rows.Add("1","PLS1","PLNAME","15","-");
            dt.Rows.Add("2", "PLS1", "PLNAME", "15", "-");
            //Dictionary<string, string, string, string, string> dictMapping = new Dictionary<>();
            playlistnames.ItemsSource = dt.DefaultView;
            foreach (DataGridViewColumn col in playlistnames.Columns)
            {
            }*/

        }

        
        private void Show_Click(object sender, RoutedEventArgs e) {
            //MyPopup.IsOpen = true;
            /*Track_Metadata addMusic = new Track_Metadata();
            addMusic.ShowDialog();
            */
            Create_Playlist createplaylist = new Create_Playlist();
            createplaylist.ShowDialog();
            //LoadAllSong();
        }
        private void Hide_Click(object sender, RoutedEventArgs e) {
            MyPopup.IsOpen = false;
        }
    }
}