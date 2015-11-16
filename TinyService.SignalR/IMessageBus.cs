using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.MessageBus
{
    public interface IMessageBus : IDisposable
    {
        System.Threading.Tasks.Task AddTopic(string topicName);

        event Action<Exception> Error;
        System.Threading.Tasks.Task ExitTopic(string topicName);
        IHubProxy proxy { get; }
        Task Publish<T>(params object[] message);
        Task Send<T>(params object[] message);
        System.Threading.Tasks.Task SendMessageToTopic<T>(T message, string topicName);
        System.Threading.Tasks.Task Start();
        void Stop();

        event Action Stoped;

      
    }
}
