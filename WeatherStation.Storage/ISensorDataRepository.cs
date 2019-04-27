namespace WeatherStation.Storage
{
    public interface ISensorDataRepository
    {
        void Add(SensorData data);

        SensorData[] All();
    }
}