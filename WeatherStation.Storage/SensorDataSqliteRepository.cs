using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace WeatherStation.Storage
{
    public class SensorDataSqliteRepository : ISensorDataRepository
    {
        private readonly string filePath;
        private readonly string connectionString;

        public SensorDataSqliteRepository(string filePath)
        {
            this.filePath = Environment.ExpandEnvironmentVariables(filePath);

            connectionString = $"Data Source={this.filePath};";
        }

        public void Initilize()
        {
            if (File.Exists(filePath))
            {
                return;
            }

            SQLiteConnection.CreateFile(filePath);

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = @"CREATE TABLE SensorData (
                                   Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                   ReceiveDate DATETIME,
                                   Temperature FLOAT, 
                                   Humidity FLOAT
                               );";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Add(SensorData data)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO SensorData (ReceiveDate, Temperature, Humidity) VALUES(@receiveDate, @temperature, @humidity)";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@receiveDate", data.ReceiveDate);
                    command.Parameters.AddWithValue("@temperature", data.Temperature);
                    command.Parameters.AddWithValue("@humidity", data.Humidity);

                    command.ExecuteNonQuery();
                }
            }
        }

        public SensorData[] All()
        {
            var result = new List<SensorData>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT ReceiveDate, Temperature, Humidity FROM SensorData";

                using (var command = new SQLiteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var sensorData = new SensorData
                        {
                            ReceiveDate = (DateTime) reader["ReceiveDate"],
                            Temperature = (double) reader["Temperature"],
                            Humidity = (double) reader["Humidity"]
                        };

                        result.Add(sensorData);
                    }
                }
            }

            return result.ToArray();
        }
    }
}