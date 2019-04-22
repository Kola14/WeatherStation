using System.Data.SQLite;
using System.IO;

namespace WeatherStation.Storage
{
    //todo: Rename to Repository?
    public class SqliteWriter
    {
        public SqliteWriter()
        {
            if (!File.Exists("Database.sqlite"))
            {
                SQLiteConnection.CreateFile("Database.sqlite");

                Setup();
            }
        }

        public void Setup()
        {
            using (var connection = new SQLiteConnection("Data Source=Database.sqlite;"))
            {
                connection.Open();

                string query = @"CREATE TABLE SensorData (
                                   Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                   ReceiveDate DATETIME,
                                   Temperature FLOAT, 
                                   Humidity FLOAT
                               );";

                using(var command = new SQLiteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Insert(SensorData data)
        {
            using (var connection = new SQLiteConnection("Data Source=Database.sqlite;"))
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
    }
}