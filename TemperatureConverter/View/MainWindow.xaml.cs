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

        private void ConvertToCelsius(object sender, RoutedEventArgs e)
        {
            var farenheit = double.Parse(farenheitBox.Text);
            var celsius = (farenheit - 32) / 1.8;
            celsiusBox.Text = $"{celsius}";
        }

        private void ConvertToFarenheit(object sender, RoutedEventArgs e)
        {
            var celsius = double.Parse(celsiusBox.Text);
            var farenheit = (celsius * 1.8) + 32;
            farenheitBox.Text = $"{farenheit}";
        }

        private void ConvertCelsius(object sender, RoutedEventArgs e)
        {
            var celsius = double.Parse(celsiusBox.Text);
            var farenheit = (celsius * 1.8) + 32;
            farenheitBox.Text = $"{farenheit}";
            var kelvin = celsius + 273.15;
            kelvinBox.Text = $"{kelvin}";
        }

        private void ConvertFarenheit(object sender, RoutedEventArgs e)
        {
            var farenheit = double.Parse(farenheitBox.Text);
            var celsius = (farenheit - 32) / 1.8;
            celsiusBox.Text = $"{celsius}";
            var kelvin = celsius + 273.15;
            kelvinBox.Text = $"{kelvin}";
        }

        private void ConvertKelvin(object sender, RoutedEventArgs e)
        {
            var kelvin = double.Parse(kelvinBox.Text);
            var celsius = kelvin - 273.15;
            celsiusBox.Text = $"{celsius}";
            var farenheit = (celsius * 1.8) + 32;
            farenheitBox.Text = $"{farenheit}";
        }
    }
}
