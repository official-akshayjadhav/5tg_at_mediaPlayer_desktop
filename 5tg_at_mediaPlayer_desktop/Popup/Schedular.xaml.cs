﻿using _5tg_at_mediaPlayer_desktop.connection;
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
using System.Windows.Shapes;

namespace _5tg_at_mediaPlayer_desktop.Popup
{
    /// <summary>
    /// Interaction logic for Schedular.xaml
    /// </summary>
    public partial class Schedular : Window
    {
        public Schedular()
        {
            InitializeComponent();
        }

        private void DateTme_Click(object sender, RoutedEventArgs e)
        {
            String sch = DateTimePicker1.Value.ToString();   //("yyyy-MM-dd HH:mm:ss");
            DateTime dt = DateTimePicker1.Value.Value;
            { }
            string format = "yyyy-MM-dd HH:mm:ss";
            string str = dt.ToString(format);
            { }
            //string query = "update playlists set Schedule = '" + sch + "' where PID = " + Global_Log.pID + " and name = '" + Global_Log.playlistName + "'";
            string query = "update playlists set Schedule = '" + str + "' where PID = " + Global_Log.pID + " and name = '" + Global_Log.playlistName + "'";

            int count = Global_Log.connectionClass.insertData(query);
            if (count == 0)
            {
                MessageBox.Show("Please ReSchedule");
            }
            else
            {
                MessageBox.Show("Schedule Successfully Updated");
                this.Close();
            }
        }
    }
}
