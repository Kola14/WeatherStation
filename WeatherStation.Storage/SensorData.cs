using System;

namespace WeatherStation.Storage
{
    public class SensorData
    {
        /// <summary>
        /// Температура
        /// </summary>
        public float Temperature { get; set; }

        /// <summary>
        /// Влажность воздуха
        /// </summary>
        public float Humidity { get; set; }

        /// <summary>
        /// Дата получения
        /// </summary>
        public DateTime ReceiveDate { get; set; }
    }
}
