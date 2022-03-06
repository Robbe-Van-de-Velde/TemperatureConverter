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
using System.Globalization;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double kelvin = slider.Value;
            var celsius = kelvin - 273.15;
            var farenheit = (celsius * 1.8) + 32;
            farenheitBox.Text = $"{farenheit}";
        }
    }

    public class TemperatureConverter : IValueConverter
    {
        public ITemperatureScale TemperatureScale { get; set; }
   
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TemperatureScale.ConvertFromKelvin((double)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TemperatureScale.ConvertFromKelvin((double)value);
        }
    }
    /*public class CelsiusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var kelvin = (double)value;
            var celsius = kelvin - 273.15;

            return celsius.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var celsius = double.Parse((string)value);
            var kelvin = celsius + 273.15;

            return kelvin;
        }
    }

    public class FahrenheitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var kelvin = (double)value;
            var farenheit = Math.Round(((kelvin - 273.15) * 1.8) + 32, 2);

            return farenheit.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var fahrenheit = double.Parse((string)value);
            var kelvin = Math.Round(((fahrenheit - 32) / 1.8 ) + 273.15, 2);

            return kelvin;
        }
    }*/
}
