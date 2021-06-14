using System;
using System.Windows.Input;

namespace WPFWeatherApp.ViewModel.Commands
{
    public class SearchCommand : ICommand
    {
        public WeatherVM WeatherVm { get; set; }

        public SearchCommand(WeatherVM vm)
        {
            WeatherVm = vm;
        }

        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrWhiteSpace(parameter.ToString());
        }

        public void Execute(object parameter)
        {
            WeatherVm.MakeQuery();
        }

        public event EventHandler CanExecuteChanged;
    }
}
