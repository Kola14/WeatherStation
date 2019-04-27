using System;
using System.IO.Ports;
using WeatherStation.Storage;

namespace WeatherStation.Listener
{
    static class Program
    {
        public static void Main(string[] args)
        {
            string portName = GetPortName();

            var repository = new SensorDataRepository("sensors.db");
            repository.Initilize();

            var listener = new SerialPortListener(portName, 9600);

            var bootrapper = new Bootstrapper(listener, repository);
            bootrapper.Start();

            Console.ReadLine();

            bootrapper.Stop();
        }

        private static string GetPortName()
        {
            Console.WriteLine("Available ports: ");

            var availablePorts = SerialPort.GetPortNames();

            for (int i = 0; i < availablePorts.Length; i++)
            {
                Console.WriteLine($"{i}. {availablePorts[i]}");
            }

            while (true)
            {
                string indexText = Console.ReadLine();

                if (int.TryParse(indexText, out int index) && index < availablePorts.Length)
                {
                    return availablePorts[index];
                }

                Console.WriteLine("Incorrect port number");
            }
        }
    }
}