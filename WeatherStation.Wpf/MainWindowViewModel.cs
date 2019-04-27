using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Input;
using System.Windows.Media;
using WeatherStation.Storage;

namespace WeatherStation.Wpf
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ISensorDataRepository repository;

        private Brush fillColor = Brushes.Red;

        public Brush FillColor
        {
            get => fillColor;
            set
            {
                fillColor = value;
                OnPropertyChanged(nameof(FillColor));
            }
        }

        public SeriesCollection SeriesCollection { get; }

        public ICommand ToggleDataUpdateCommand { get; }

        public MainWindowViewModel()
        {
            var repository = new SensorDataSqliteRepository("%homepath%/sensors.db");
            repository.Initilize();

            this.repository = repository;

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Humidity",
                    Values = new ChartValues<double> { 4, 6, 5, 2 ,7 }
                },
                new LineSeries
                {
                    Title = "Temperature",
                    Values = new ChartValues<double> { 6, 7, 3, 4 ,6 }
                }
            };

            ToggleDataUpdateCommand = new RelayCommand(ToggleDataUpdate);
        }

        private void ToggleDataUpdate()
        {
        }
    }
}