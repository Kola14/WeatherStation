using System;

namespace WeatherStation.Listener
{
    public interface ISerialPortListener
    {
        event EventHandler<ErrorOccurredEventArgs> ErrorOccurred;
        event EventHandler<MessageReceivedEventArgs> MessageReceived;

        void Start();
        void Stop();
    }
}