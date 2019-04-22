using System;

namespace WeatherStation.Listener
{
    public class ErrorEventArgs: EventArgs
    {
        public string Message { get; }

        public ErrorEventArgs(string message)
        {
            Message = message;
        }
    }
}
