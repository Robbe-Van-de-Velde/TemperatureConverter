using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Cells;
using System.Windows.Input;

namespace ViewModel
{
    public class ConverterViewModel
    {

        public ConverterViewModel()
        {
            this.TemperatureInKelvin = new Cell<double>();

            this.Fahrenheit = new TemperatureScaleViewModel(new FahrenheitTemperatureScale(), this);
            this.Kelvin = new TemperatureScaleViewModel(new KelvinTemperatureScale(), this);
            this.Celsius = new TemperatureScaleViewModel(new CelsiusTemperatureScale(), this);
        }

        public Cell<double> TemperatureInKelvin { get; }

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

    public class TemperatureScaleViewModel
    {
        private readonly ConverterViewModel parent;
        private readonly ITemperatureScale temperatureScale;

        public TemperatureScaleViewModel(ITemperatureScale temperatureScale, ConverterViewModel parent)
        {
            this.parent = parent;
            this.temperatureScale = temperatureScale;

            this.Temperature = this.parent.TemperatureInKelvin.Derive(kelvin => this.temperatureScale.ConvertFromKelvin(kelvin), other => this.temperatureScale.ConvertToKelvin(other));

            var minimum = temperatureScale.ConvertFromKelvin(0);
            var maximum = temperatureScale.ConvertFromKelvin(1000);
            
            this.Add = new AddCommand(this.Temperature, +1, minimum, maximum);
            this.Subtract = new AddCommand(this.Temperature, -1, minimum, maximum);
        }

        public string Name => temperatureScale.Name;

        public Cell<double> Temperature { get; }

        public ICommand Add { get; }
        public ICommand Subtract { get; }

        public class AddCommand : ICommand
        {
            private readonly Cell<double> cell;
            private readonly int delta;
            private readonly double minimum;
            private readonly double maximum;

            public AddCommand(Cell<double> cell, int delta, double minimum, double maximum)
            {
                this.cell = cell;
                this.delta = delta;
                this.minimum = minimum;
                this.maximum = maximum;

                this.cell.PropertyChanged += (sender, args) => CanExecuteChanged(this, new EventArgs());
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return minimum <= cell.Value + delta && cell.Value + delta <= maximum;
            }

            public void Execute(object parameter)
            {
                cell.Value = Math.Round(cell.Value + delta);
            }
        }
    }
}
