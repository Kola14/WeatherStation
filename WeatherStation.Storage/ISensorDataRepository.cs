namespace WeatherStation.Storage
{
    public interface ISensorDataRepository
    {
        void Initilize();

        void Add(SensorData data);
    }
}