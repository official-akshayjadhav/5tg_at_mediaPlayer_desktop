using System;
using System.Windows;
using System.Windows.Controls;
using _5tg_at_mediaPlayer_desktop.connection;


namespace _5tg_at_mediaPlayer_desktop.Fade_in_out
{
    /// <summary>
    /// Interaction logic for Fade_in_out.xaml
    /// </summary>
    public partial class Fade_in_out : UserControl
    {
        static bool temp = false;
        public static Double max_time_of_song = 0;

        public Fade_in_out()
        {
            InitializeComponent();
        }

        public Fade_in_out(int TimesInSec)
        {
            InitializeComponent();

            max_time_of_song = TimesInSec;
            max_slider.Minimum = 0;

            max_slider.Minimum = 0;
            max_slider.Maximum = max_time_of_song;

            max_slider.Value = max_time_of_song;
        }

        private void start_s_Click(object sender, RoutedEventArgs e)
        {
            double incrementValue = min_slider.Value - 1;
            start_text.Text = incrementValue.ToString();
            min_slider.Value = incrementValue;
        }

        private void start_e_Click(object sender, RoutedEventArgs e)
        {
            min_slider.Maximum = max_time_of_song;
            {
                double incrementValue = min_slider.Value + 1;
                start_text.Text = incrementValue.ToString();
                min_slider.Value = incrementValue;
            }
        }

        private void end_s_Click(object sender, RoutedEventArgs e)
        {
            {
                double incrementValue = max_slider.Value - 1;
                end_text.Text = incrementValue.ToString();
                max_slider.Value = incrementValue;
            }
            if (max_slider.Value <= min_slider.Value)
            {
                double decrementValue = min_slider.Value + 1;
                start_text.Text = decrementValue.ToString();
                min_slider.Value = decrementValue;
            }
        }

        private void end_e_Click(object sender, RoutedEventArgs e)
        {
            min_slider.Maximum = max_time_of_song;
            {
                double incrementValue = max_slider.Value + 1;
                end_text.Text = incrementValue.ToString();
                max_slider.Value = incrementValue;
            }
        }

        private void max_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (temp == false)
            {
                temp = true;
                max_slider.Value = max_time_of_song;
            }

            max_slider.Maximum = max_time_of_song;
            Global_Log.endFadeInSec = Convert.ToInt32(max_slider.Value);
        }
        private void min_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            min_slider.Maximum = max_time_of_song;

            if (max_slider.Value < min_slider.Value)
            {
                start_text.Text = max_slider.Value.ToString();
                min_slider.Value = max_slider.Value;
            }

            Global_Log.startFadeInSec = Convert.ToInt32(min_slider.Value);
        }

        private void PlayMedia_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }





        //private void start_s_Click(object sender, RoutedEventArgs e)
        //{
        //    double start_s1 = double.Parse(start_text.Text);
        //    if (start_s1 > 0)
        //    {
        //        start_s1 -= 1;
        //        start_text.Text = start_s1.ToString();
        //        min_slider.Value = start_s1;
        //    }
        //    else
        //    {
        //        start_text.Text = "0";
        //        min_slider.Value = 0;
        //    }
        //}

        //private void start_e_Click(object sender, RoutedEventArgs e)
        //{
        //    double start_e1 = double.Parse(start_text.Text);
        //    //if (int end = int.Parse(end_text.Text)) { }
        //    double end_s1 = double.Parse(end_text.Text);
        //    if (start_e1 < end_s1)
        //    {
        //        start_e1 += 1;
        //        start_text.Text = start_e1.ToString();
        //        min_slider.Value = start_e1;
        //    }
        //}

        //private void end_s_Click(object sender, RoutedEventArgs e)
        //{
        //    double end_s1 = double.Parse(end_text.Text);
        //    double start_e1 = double.Parse(start_text.Text);
        //    if (end_s1 > start_e1)
        //    {
        //        end_s1 -= 1;
        //        end_text.Text = end_s1.ToString();
        //        max_slider.Value = end_s1;
        //    }
        //}

        //private void end_e_Click(object sender, RoutedEventArgs e)
        //{
        //    double end_e1 = double.Parse(end_text.Text);
        //    if (end_e1 < max_time_of_song)
        //    {
        //        end_e1 += 1;
        //        end_text.Text = end_e1.ToString();
        //        max_slider.Value = end_e1;
        //    }
        //}

        //private void start_text_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (end_text != null)
        //        {
        //            double start_txt = double.Parse(start_text.Text);
        //            double end_txt = double.Parse(end_text.Text);
        //            if (start_txt > 0)
        //            {
        //                if (start_txt < end_txt)
        //                {
        //                    min_slider.Value = start_txt;
        //                }
        //                else
        //                {
        //                    start_text.Text = end_txt.ToString();
        //                    min_slider.Value = end_txt;
        //                }
        //            }
        //            else
        //            {

        //                start_text.Text = "0";
        //                min_slider.Value = 0;
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        start_text.Text = "0";
        //    }
        //}

        //private void end_text_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    try
        //    {
        //        double end_txt = double.Parse(end_text.Text);
        //        double start_txt = double.Parse(start_text.Text);
        //        if (end_txt < max_time_of_song)
        //        {
        //            if (end_txt > start_txt)
        //            {
        //                max_slider.Value = end_txt;
        //            }
        //            else
        //            {
        //                end_text.Text = start_txt.ToString();
        //                max_slider.Value = start_txt;
        //            }
        //        }
        //        else
        //        {
        //            end_text.Text = max_time_of_song.ToString();
        //            max_slider.Value = max_time_of_song;
        //        }
        //    }
        //    catch
        //    {
        //        end_text.Text = max_time_of_song.ToString();
        //    }
        //}


        /////Slider CS file
        /////
        //double min1, max1;
        //private void min_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    min1 = min_slider.Value;
        //    if (min1 < max1)
        //    {
        //        start_text.Text = min1.ToString();
        //    }
        //    else
        //    {
        //        start_text.Text = max_slider.Value.ToString();
        //        min_slider.Value = (double)max_slider.Value;
        //    }
        //}

        //private void PlayMedia_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{

        //}

        //private void max_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    max1 = max_slider.Value;
        //    if (max1 > min1)
        //    {
        //        end_text.Text = max1.ToString();
        //    }
        //    else
        //    {
        //        end_text.Text = min_slider.Value.ToString();
        //        max_slider.Value = min_slider.Value;
        //    }
        //}

    }
}
