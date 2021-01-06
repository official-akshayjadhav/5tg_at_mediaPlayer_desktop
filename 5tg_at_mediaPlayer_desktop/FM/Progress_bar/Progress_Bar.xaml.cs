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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _5tg_at_mediaPlayer_desktop.FM.Progress_bar
{
    /// <summary>
    /// Interaction logic for Progress_Bar.xaml
    /// </summary>
    /// 

    public partial class Progress_Bar : UserControl
    {

        public static readonly DependencyProperty IndicatorBrushProperty = DependencyProperty.Register("IndicatorBrush", typeof(Brush), typeof(Progress_Bar));
        public Brush IndicatorBrush
        {
            get { return (Brush)this.GetValue(IndicatorBrushProperty); }
            set { this.SetValue(IndicatorBrushProperty, value); }
        }
        public static readonly DependencyProperty BackgroundBrushProperty = DependencyProperty.Register("BackgroundBrush", typeof(Brush), typeof(Progress_Bar));
        public Brush BackgroundBrush
        {
            get { return (Brush)this.GetValue(BackgroundBrushProperty); }
            set { this.SetValue(BackgroundBrushProperty, value); }
        }
        public static readonly DependencyProperty ProgressBorderBrushProperty = DependencyProperty.Register("ProgressBorderBrush", typeof(Brush), typeof(Progress_Bar));
        public Brush ProgressBorderBrush
        {
            get { return (Brush)this.GetValue(ProgressBorderBrushProperty); }
            set { this.SetValue(ProgressBorderBrushProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(Progress_Bar));
        public int Value
        {
            get { return (int)this.GetValue(ValueProperty); }
            set { this.SetValue(ValueProperty, value); }
        }
        public Progress_Bar()
        {
            InitializeComponent();
        }
    }
    [ValueConversion(typeof(int), typeof(double))]
    public class ValueToAngleConverter : IValueConverter
    {
        public object Convert(object value, Type t, object parameter, System.Globalization.CultureInfo culture)
        {
            return (double)(((int)value * 0.01) * 180);
        }

        public object ConvertBack(object value, Type t, object parameter, System.Globalization.CultureInfo culture)
        {
            return (double)((double)value / 180) * 100;
        }
    }
}
