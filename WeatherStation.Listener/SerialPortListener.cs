using System;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace WeatherStation.Listener
{
    public class SerialPortListener : ISerialPortListener
    {
        private readonly string portName;
        private readonly int baudRate;
        private readonly SerialPort serialPort;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public event EventHandler<ErrorOccurredEventArgs> ErrorOccurred;

        public SerialPortListener(string portName, int baudRate)
        {
            this.portName = portName.ToLower();
            this.baudRate = baudRate;

            serialPort = new SerialPort(portName, baudRate);
        }

        public void Start()
        {
            serialPort.Open();
            var thread = new Thread(Listen);
            thread.Start();
        }

        private void Listen()
        {
            try
            {
                while (serialPort.IsOpen)
                {
                    string message = serialPort.ReadLine();
                    MessageReceived?.Invoke(this, new MessageReceivedEventArgs(message));
                }
            }
            catch(IOException ex)
            {
                ErrorOccurred?.Invoke(this, new ErrorOccurredEventArgs(ex.Message));
            }
        }

        public void Stop()
        {
            serialPort.Close();
        }
    }
}