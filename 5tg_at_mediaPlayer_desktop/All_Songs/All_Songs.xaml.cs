using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace _5tg_at_mediaPlayer_desktop.All_Songs
{
    /// <summary>
    /// Interaction logic for All_Songs.xaml
    /// </summary>
    public class songs{
        public String Image { get; set; }
        public String AirTime { get; set; }
        public String CartID { get; set; }
        public String Item { get; set; }
        public String Duration { get; set; }
        public String Chain { get; set; }
        public String Dot { get; set; }
    }
    public partial class All_Songs : UserControl
    {

        public All_Songs()
        {
            InitializeComponent();
            List<songs> users = new List<songs>();
            users.Add(new songs() {
                Image = "",
                AirTime = "AirTime",
                CartID = "CartID",
                Item = "Item",
                Duration = "Duration",
                Chain = "check",
                Dot = "..."
            });
            users.Add(new songs()
            {
                Image = "",
                AirTime = "AirTime",
                CartID = "CartID",
                Item = "Item",
                Duration = "Duration",
                Chain = "check",
                Dot = "..."
            });
            users.Add(new songs()
            {
                Image = "",
                AirTime = "AirTime",
                CartID = "CartID",
                Item = "Item",
                Duration = "Duration",
                Chain = "check",
                Dot = "..."
            });
            users.Add(new songs()
            {
                Image = "",
                AirTime = "AirTime",
                CartID = "CartID",
                Item = "Item",
                Duration = "Duration",
                Chain = "check",
                Dot = "..."
            });
            users.Add(new songs()
            {
                Image = "",
                AirTime = "AirTime",
                CartID = "CartID",
                Item = "Item",
                Duration = "Duration",
                Chain = "check",
                Dot = "..."
            });
            //this.DataContext = this;
            //datagrid.ItemsSource = users;
        }
    }
}
