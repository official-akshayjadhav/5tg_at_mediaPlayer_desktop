using _5tg_at_mediaPlayer_desktop.connection;
using System;
using System.Windows;
using System.Windows.Controls;

namespace _5tg_at_mediaPlayer_desktop.Trim
{
    /// <summary>
    /// Interaction logic for Trim.xaml
    /// </summary>

    public partial class Trim : UserControl
    {
        //double max_time_of_song = 5;
        static double max_time_of_song = 0;


        public Trim()
        {
            InitializeComponent();
            { }
            string val= Global_Log.endTimeInSec.ToString();
            { }
            max_slider.Maximum = Global_Log.endTimeInSec;
            end_text.Text = Convert.ToString(Global_Log.endTimeInSec);
            
            end_text.Text = val;
            { }
        }

        public Trim(string time)
        {
            InitializeComponent();
            max_time_of_song = double.Parse(time);
            if (end_text != null)
            {
                //max_slider.Maximum = max_time_of_song;
                max_slider.Maximum = Global_Log.endTimeInSec;
                end_text.Text = max_time_of_song.ToString();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            start_text.Text = "0";
            end_text.Text = Global_Log.endTimeInSec.ToString();
            max_slider.Maximum = Global_Log.endTimeInSec;

        }
        

        private void start_s_Click(object sender, RoutedEventArgs e)
        {
            double start_s1 = double.Parse(start_text.Text);
            if (start_s1 > 0)
            {
                start_s1 -= 1;
                start_text.Text = start_s1.ToString();
                min_slider.Value = start_s1;
            }
            else
            {
                start_text.Text = "0";
                min_slider.Value = 0;
            }
        }

        private void start_e_Click(object sender, RoutedEventArgs e)
        {
            double start_e1 = double.Parse(start_text.Text);
            //if (int end = int.Parse(end_text.Text)) { }
            double end_s1 = double.Parse(end_text.Text);
            if (start_e1 < end_s1)
            {
                start_e1 += 1;
                start_text.Text = start_e1.ToString();
                min_slider.Value = start_e1;
            }
        }

        private void end_s_Click(object sender, RoutedEventArgs e)
        {
            double end_s1 = double.Parse(end_text.Text);
            double start_e1 = double.Parse(start_text.Text);
            if (end_s1 > start_e1)
            {
                end_s1 -= 1;
                end_text.Text = end_s1.ToString();
                max_slider.Value = end_s1;
            }
        }

        private void end_e_Click(object sender, RoutedEventArgs e)
        {
            double end_e1 = double.Parse(end_text.Text);
            if (end_e1 < max_time_of_song)
            {
                end_e1 += 1;
                end_text.Text = end_e1.ToString();
                max_slider.Value = end_e1;
            }
        }

        private void start_text_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (end_text != null)
                {
                    double start_txt = double.Parse(start_text.Text);
                    double end_txt = double.Parse(end_text.Text);
                    if (start_txt > 0)
                    {
                        if (start_txt < end_txt)
                        {
                            min_slider.Value = start_txt;
                        }
                        else
                        {
                            start_text.Text = end_txt.ToString();
                            min_slider.Value = end_txt;
                        }
                    }
                    else
                    {

                        start_text.Text = "0";
                        min_slider.Value = 0;
                    }
                }
                else
                {
                    //Global_Log
                }
            }
            catch
            {
                start_text.Text = "0";
            }
        }

        private void end_text_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //max_slider.Value = Global_Log.endTimeInSec;

                double end_txt = double.Parse(end_text.Text);
                double start_txt = double.Parse(start_text.Text);
                if (end_txt < max_time_of_song)
                {
                    if (end_txt > start_txt)
                    {
                        max_slider.Value = end_txt;
                    }
                    else
                    {
                        end_text.Text = start_txt.ToString();
                        max_slider.Value = start_txt;
                    }
                }
                else
                {
                    end_text.Text = max_time_of_song.ToString();
                    max_slider.Value = max_time_of_song;
                }
            }
            catch
            {
                end_text.Text = max_time_of_song.ToString();
            }
        }


        ///Slider CS file
        ///
        double min1, max1;
        private void min_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            min1 = min_slider.Value;
            if (min1 < max1)
            {
                start_text.Text = min1.ToString();

            }
            else
            {
                start_text.Text = max_slider.Value.ToString();
                min_slider.Value = (double)max_slider.Value;
            }
        }

        private void PlayMedia_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }


        private void max_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            max1 = Convert.ToInt32(max_slider.Value);
            if (max1 > min1)
            {
                end_text.Text = max1.ToString();
            }
            else
            {
                end_text.Text = min_slider.Value.ToString();
                max_slider.Value = min_slider.Value;
            }
        }

    }
}
