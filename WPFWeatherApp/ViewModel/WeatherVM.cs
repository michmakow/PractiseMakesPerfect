using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WPFWeatherApp.Annotations;
using WPFWeatherApp.Model;
using WPFWeatherApp.ViewModel.Commands;
using WPFWeatherApp.ViewModel.Helpers;

namespace WPFWeatherApp.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {
        private string _query;

        public string Query
        {
            get => _query;
            set
            {
                _query = value;
                OnPropertyChanged(nameof(Query));
            }
        }

        private CurrentConditions _currentConditions;

        public CurrentConditions CurrentConditions
        {
            get => _currentConditions;
            set
            {
                _currentConditions = value;
                OnPropertyChanged(nameof(CurrentConditions));
            }
        }

        private City _selectedCity;

        public City SelectedCity
        {
            get => _selectedCity;
            set
            {
                _selectedCity = value; 
                OnPropertyChanged(nameof(SelectedCity));
            }
        }

        public SearchCommand SearchCommand { get; set; }

        public WeatherVM()
        {
            //Good for test purpose, we see default in designer but no after running app
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                SelectedCity = new City()
                {
                    LocalizedName = "New York"
                };

                CurrentConditions = new CurrentConditions
                {
                    WeatherText = "Partly cloudy",
                    Temperature = new Temperature
                    {
                        Metric = new Units
                        {
                            Value = 21
                        }
                    }
                };
            }

            SearchCommand = new SearchCommand(this);
        }

        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(Query);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
