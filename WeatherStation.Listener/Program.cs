using System;
using System.IO.Ports;
using WeatherStation.Storage;

namespace WeatherStation.Listener
{
    static class Program
    {
        //todo: Выделить в отдельный класс? Bootstrap?
        public static void Main(string[] args)
        {
            //todo: Слишком много всего
            Console.WriteLine("Available ports: ");

            var availablePorts = SerialPort.GetPortNames();

            for (int i = 0; i < availablePorts.Length; i++)
            {
                Console.WriteLine($"{i}. {availablePorts[i]}");
            }

            string indexText = Console.ReadLine();

            if (!int.TryParse(indexText, out int index))
            {
                return;
            }

            var repository = new SensorDataRepository("sensors.db");
            repository.Initilize();

            var listener = new SerialPortListener(availablePorts[index], 9600);

            var bootrapper = new Bootstrapper(listener, repository);
            bootrapper.Start();

            Console.ReadLine();

            bootrapper.Stop();
        }
    }
}