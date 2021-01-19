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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //for (int i = 0; i <= 100; i++)
            //{
            //    circulare_ProgressBar.Value = i;
            //}

            //progress_value.Text = "10";
            //circulare_ProgressBar.Value = 25;
        }


        public void loadProgressBar(int i)
        {
            if (i <= 100)
            {
                int value = i++;
                progress_value.Text = value.ToString() + " %";
                circulare_ProgressBar.Value = value;
            }
        }




        private void Live_assist_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
