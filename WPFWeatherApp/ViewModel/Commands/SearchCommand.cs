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
            var query = parameter as string;

            return !string.IsNullOrWhiteSpace(query);
        }

        public void Execute(object parameter)
        {
            WeatherVm.MakeQuery();
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
