using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Cells;

namespace ViewModel
{
    public class ConverterViewModel
    {

        public ConverterViewModel()
        {
            this.TemperatureInKelvin = new Cell<double>();

            this.Kelvin = new TemperatureScaleViewModel(new KelvinTemperatureScale(), this);
            this.Celsius = new TemperatureScaleViewModel(new CelsiusTemperatureScale(), this);
            this.Fahrenheit = new TemperatureScaleViewModel(new FahrenheitTemperatureScale(), this);
        }

        public Cell<double> TemperatureInKelvin { get; set;}

        public TemperatureScaleViewModel Kelvin { get; }

        public TemperatureScaleViewModel Celsius { get; }

        public TemperatureScaleViewModel Fahrenheit { get; }

        public IEnumerable<TemperatureScaleViewModel> Scales
        {
            get
            {
                yield return Celsius;
                yield return Fahrenheit;
                yield return Kelvin;
            }
        }
    }

    public class TemperatureScaleViewModel : INotifyPropertyChanged
    {
        private readonly ConverterViewModel parent;
        private readonly ITemperatureScale temperatureScale;

        public TemperatureScaleViewModel(ITemperatureScale temperatureScale, ConverterViewModel parent)
        {
            this.parent = parent;
            this.temperatureScale = temperatureScale;

            this.parent.TemperatureInKelvin.PropertyChanged += (sender, args) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Temperature)));
        }

        public string Name => temperatureScale.Name;

        public double Temperature
        {
            get
            {
                return temperatureScale.ConvertFromKelvin(parent.TemperatureInKelvin.Value);
            }
            set
            {
                parent.TemperatureInKelvin.Value = temperatureScale.ConvertToKelvin(value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
