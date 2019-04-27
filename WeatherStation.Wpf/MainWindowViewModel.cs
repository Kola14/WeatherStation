using LiveCharts;
using LiveCharts.Wpf;
using System.Linq;
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

            var data = repository.All();

            this.repository = repository;

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Humidity",
                    Values = new ChartValues<double>(data.Select(x => x.Humidity))
                },
                new LineSeries
                {
                    Title = "Temperature",
                    Values = new ChartValues<double>(data.Select(x => x.Temperature))
                }
            };

            ToggleDataUpdateCommand = new RelayCommand(ToggleDataUpdate);
        }

        private void ToggleDataUpdate()
        {
        }
    }
}