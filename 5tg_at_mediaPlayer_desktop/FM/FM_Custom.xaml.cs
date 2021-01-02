using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace _5tg_at_mediaPlayer_desktop.FM
{
    /// <summary>
    /// Interaction logic for FM_Custom.xaml
    /// </summary>
    public partial class FM_Custom : Window
    {
        public FM_Custom()
        {
            InitializeComponent();
            //Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata { DefaultValue = 6 });
        }

        private void Live_assist_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
