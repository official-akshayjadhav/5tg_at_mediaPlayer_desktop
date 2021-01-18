using _5tg_at_mediaPlayer_desktop.connection;
using _5tg_at_mediaPlayer_desktop.Popup;
using CsvHelper;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _5tg_at_mediaPlayer_desktop.LogHistory
{
    /// <summary>
    /// Interaction logic for LogHistory.xaml
    /// </summary>
    public partial class LogHistory : UserControl
    {
        public LogHistory()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadHomePage();
        }

        private void import_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string strFilePath = Global_Log.ActiveDir + "\\LogFile.csv";

            List<Log> writeCsvData = new List<Log>();
            using (TextFieldParser csvReader = new TextFieldParser(strFilePath))
            {
                csvReader.CommentTokens = new string[] { "#" };
                csvReader.SetDelimiters(new string[] { "," });
                csvReader.HasFieldsEnclosedInQuotes = true;

                // Skip the row with the column names
                csvReader.ReadLine();

                while (!csvReader.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvReader.ReadFields();

                    writeCsvData.Add(new Log()
                    {
                        LogID = Convert.ToInt32(fields[0]),
                        CartId = Convert.ToInt32(fields[1]),
                        AirTime = fields[2],
                        Title = fields[3],
                        PlaylistName = fields[4],
                        Duration = TimeSpan.Parse(fields[5])
                    });
                }
            }

            if (writeCsvData.Count != 0)
            {
                foreach (Log log in writeCsvData)
                {
                    int logID = log.LogID;
                    logID = 10;
                    string logAirTime = log.AirTime;
                    { }
                    string updateLogTime = "update logs set LogTime = '" + logAirTime + "' where LID = " + logID;
                    int returnLogID = Global_Log.connectionClass.insertData(updateLogTime);
                    { }

                    string query_GetPID = "select PID from playlists where name = '" + log.PlaylistName + "'";
                    DataTable dt = Global_Log.connectionClass.retriveData(query_GetPID, "playlists");
                    DataRow dr = dt.Rows[0];
                    int log_PID = Convert.ToInt32(dr[0]);
                    { }
                    //string query_GetAID = "select * from Audio where UID = '" + log.CartId + "'";
                    string query_GetAID = "select * from Audio where UID = " + 000;
                    dt = Global_Log.connectionClass.retriveData(query_GetAID, "Audio");
                    dr = dt.Rows[0];
                    int log_AID = Convert.ToInt32(dr[0]);
                    { }

                    if (returnLogID == 0) //New Entry
                    {
                        //string query_SetLog = "insert into Logs(LID, LogTime, AID, PID)values(" + log.LogID + ",'" + log.AirTime + "'," + log_PID + "," + log_AID + ")";
                        string query_SetLog = "insert into Logs( LogTime, AID, PID)values('" + log.AirTime + "'," + log_AID + "," + log_PID + ")";
                        int cnt = Global_Log.connectionClass.insertData(query_SetLog);
                        { }
                    }
                    else //Update
                    {
                        string query = "update Audio set Title = '" + log.Title + "'  where UID = " + log_AID;
                        int cnt = Global_Log.connectionClass.insertData(query);

                        query = "update playlists set name = '" + log.PlaylistName + "' where pid = " + log_PID;
                        cnt = Global_Log.connectionClass.insertData(query);
                    }
                }
            }
        }

        private void export_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string query = "select lg.LID, lg.LogTime, a.UID, a.Title, a.duration, ps.name from Logs lg inner join Audio a on lg.AID = a.ID " +
                            "inner join playlists ps on ps.PID = lg.PID";

            DataTable dt = Global_Log.connectionClass.retriveData(query, "log");
            List<Log> writeCsvData = new List<Log>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                try
                {
                    writeCsvData.Add(new Log()
                    {
                        LogID = Convert.ToInt32(dr[0]),
                        AirTime = dr[1].ToString(),
                        CartId = Convert.ToInt32(dr[2]),
                        Title = dr[3].ToString(),
                        Duration = (TimeSpan)(dr[4]),
                        PlaylistName = dr[5].ToString()
                    });
                }
                catch (Exception ex)
                { }
            }

            string strFilePath = Global_Log.ActiveDir + "\\LogFile.csv";
            using (var writer = new StreamWriter(strFilePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(writeCsvData);
            }

            MessageBox.Show("All Songs Export");
        }

        private void ViewAll_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            loadHomePage();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Log
        }

        private void loadHomePage()
        {
            string query = "select lg.LID, lg.LogTime, a.UID, a.Title, a.duration, ps.name from Logs lg inner join Audio a on lg.AID = a.ID " +
                            "inner join playlists ps on ps.PID = lg.PID";

            List<Log> logList = new List<Log>();
            DataTable dt = getAllLogHistory(query);
            int count = dt.Rows.Count;

            for (int i = 0; i < count; i++)
            {
                DataRow dr = dt.Rows[i];
                try
                {
                    logList.Add(new Log()
                    {
                        LogID = Convert.ToInt32(dr[0]),
                        AirTime = dr[1].ToString(),
                        CartId = Convert.ToInt32(dr[2]),
                        Title = dr[3].ToString(),
                        Duration = (TimeSpan)dr[4],
                        PlaylistName = dr[5].ToString()
                    });
                }
                catch (Exception ex)
                { }
            }
            if (datagrid != null)
            {
                datagrid.ItemsSource = logList;
            }
        }

        public DataTable getAllLogHistory(string query)
        {
            return Global_Log.connectionClass.retriveData(query, "Logs");
        }

        private void Command_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Command command = new Command();
            command.ShowDialog();
        }
    }
}
