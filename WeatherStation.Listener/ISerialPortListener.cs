using System;

namespace WeatherStation.Listener
{
    public interface ISerialPortListener
    {
        event EventHandler<ErrorEventArgs> Error;
        event EventHandler<MessageReceivedEventArgs> MessageReceived;

        void Start();
        void Stop();
    }
}