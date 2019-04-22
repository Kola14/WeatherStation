using System;

namespace WeatherStation.Listener
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public string Message {get; }

        public MessageReceivedEventArgs(string message)
        {
            Message = message;
        }
    }
}
