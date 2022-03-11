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
        private Cell<double> temperatureInKelvin;

        public Cell<double> TemperatureInKelvin
        {
            get
            {
                return temperatureInKelvin;
            }
            set
            {
                temperatureInKelvin = value;
            }
        }

        public ConverterViewModel()
        {
            this.Kelvin = new TemperatureScaleViewModel(new KelvinTemperatureScale(), this);
            this.Celsius = new TemperatureScaleViewModel(new CelsiusTemperatureScale(), this);
            this.Fahrenheit = new TemperatureScaleViewModel(new FahrenheitTemperatureScale(), this);
        }

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
