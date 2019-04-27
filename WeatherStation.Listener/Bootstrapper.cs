﻿using System;
using WeatherStation.Storage;

namespace WeatherStation.Listener
{
    //todo: Rename
    public class Bootstrapper
    {
        private readonly ISerialPortListener listener;
        private readonly ISensorDataRepository repository;

        public Bootstrapper(ISerialPortListener serialListener, ISensorDataRepository repository)
        {
            this.listener = serialListener;
            this.repository = repository;

            listener.MessageReceived += ListenerMessageReceived;
            listener.Error += ErrorMessageReceived;
        }

        public void Start()
        {
            listener.Start();
        }

        public void Stop()
        {
            listener.Stop();
        }

        private void ListenerMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            if (SensorData.TryParse(e.Message, out var result))
            {
                repository.Add(result);

                Console.WriteLine(e.Message);
            }
        }

        private void ErrorMessageReceived(object sender, ErrorEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
