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
            this.Increment = new IncrementCommand(this.Temperature);
        }

        public string Name => temperatureScale.Name;

        public Cell<double> Temperature { get; }

        public ICommand Increment { get; }

        public class IncrementCommand : ICommand
        {
            private readonly Cell<double> cell;

            public IncrementCommand(Cell<double> cell)
            {
                this.cell = cell;
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                cell.Value = Math.Round(cell.Value + 1);
            }
        }
    }
}
