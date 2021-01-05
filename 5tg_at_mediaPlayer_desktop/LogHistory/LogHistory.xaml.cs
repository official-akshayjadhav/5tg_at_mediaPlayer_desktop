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

            //    string strSeperator = ",";
            //StringBuilder sbOutput = new StringBuilder();

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    string s0 = writeCsvData[i].LogID.ToString();
            //    string s1 = writeCsvData[i].AirTime.ToString();
            //    string s2 = writeCsvData[i].UID.ToString();
            //    string s3 = writeCsvData[i].Title.ToString();
            //    string s4 = writeCsvData[i].Duration.ToString();
            //    string s5 = writeCsvData[i].PlaylistName.ToString();

            //    String s = s0 + s1 + "," + s2 + "," + s3 + "," + s4 + "," + s5 + Environment.NewLine;
            //    File.AppendAllText(strFilePath, s);
            //}
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
