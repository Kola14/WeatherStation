using System;
using System.Globalization;
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

            if (int.TryParse(indexText, out int index))
            {
                var listener = new SerialPortListener(availablePorts[index], 9600);

                listener.MessageReceived += ListenerMessageReceived;
                listener.Error += ErrorMessageReceived;

                listener.Start();

                Console.ReadLine();

                listener.Stop();
            }
        }

        private static void ListenerMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            if (SensorData.TryParse(e.Message, out var result))
            {
                var writer = new SqliteWriter();
                writer.Insert(result);

                Console.WriteLine(e.Message);
            }
        }

        private static void ErrorMessageReceived(object sender, ErrorEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}