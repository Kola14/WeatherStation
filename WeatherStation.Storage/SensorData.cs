using System;
using System.Globalization;

namespace WeatherStation.Storage
{
    public class SensorData
    {
        /// <summary>
        /// Температура
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// Влажность воздуха
        /// </summary>
        public double Humidity { get; set; }

        /// <summary>
        /// Дата получения
        /// </summary>
        public DateTime ReceiveDate { get; set; }

        public static bool TryParse(string text, out SensorData result)
        {
            string[] rawData = text.Split('\t');

            result = new SensorData { ReceiveDate = DateTime.Now };

            if (rawData.Length < 2 ||
                !TryParseFloat(rawData[0], out float humidity) || 
                !TryParseFloat(rawData[1], out float temperature))
            {
                return false;
            }

            result.Humidity = humidity;
            result.Temperature = temperature;

            return true;
        }

        private static bool TryParseFloat(string text, out float value)
        {
            bool result = float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out float tempValue);
            value = tempValue;
            return result;
        }
    }
}
