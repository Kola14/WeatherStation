using System;

namespace WeatherStation.Listener
{
    public class ErrorOccurredEventArgs: EventArgs
    {
        public string Message { get; }

        public ErrorOccurredEventArgs(string message)
        {
            Message = message;
        }
    }
}
