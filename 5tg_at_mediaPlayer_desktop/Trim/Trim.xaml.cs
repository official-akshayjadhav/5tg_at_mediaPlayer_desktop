using System.Windows;
using System.Windows.Controls;

namespace _5tg_at_mediaPlayer_desktop.Trim
{
    /// <summary>
    /// Interaction logic for Trim.xaml
    /// </summary>

    public partial class Trim : UserControl
    {
        public Trim()
        {
            InitializeComponent();
        }

        private void start_s_Click(object sender, RoutedEventArgs e)
        {
            int start_s1 = int.Parse(start_text.Text);
            if (start_s1 == 0)
                start_text.Text = start_s1.ToString();
            else { 
                start_s1 -= 1;
                start_text.Text = start_s1.ToString();
            }
        }

        private void start_e_Click(object sender, RoutedEventArgs e)
        {
            int start_e1 = int.Parse(start_text.Text);
            //if (int end = int.Parse(end_text.Text)) { }
            start_e1 += 1;
            start_text.Text = start_e1.ToString();  
        }

        private void end_s_Click(object sender, RoutedEventArgs e)
        {
            int end_s1 = int.Parse(end_text.Text);
            if (end_s1 == 0)
                end_text.Text = end_s1.ToString();
            else
            {
                end_s1 -= 1;
                end_text.Text = end_s1.ToString();
            }
        }

        private void end_e_Click(object sender, RoutedEventArgs e)
        {
            int end_e1 = int.Parse(end_text.Text);
            end_e1 += 1;
            end_text.Text = end_e1.ToString();
        }
    }
}
