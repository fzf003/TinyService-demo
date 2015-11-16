using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.MessageBus.Impl
{
    public abstract class AbstractMessageBus : IMessageBus
    {
        private HubConnection _hubconnection;
        private IHubProxy _hubpoxy;
        public event Action Stoped;
        public event Action<Exception> Error;
        public AbstractMessageBus(MessageBusConfiguration config)
        {
            if(config==null)
            {
                throw new Exception("配置不能为空");
            }

            this._hubconnection = new HubConnection(config.RemoteUrl, config.QueryString);
            this._hubpoxy = this._hubconnection.CreateHubProxy(config.HubName);
            this.Stoped = () => { };
            this._hubconnection.Error += (ex) => { };
        }

        protected void OnError(Exception ex)
        {
            var handler = this.Error;
            if (handler != null)
            {
                handler(ex);
            }
        }

        public virtual Task Publish<T>(params object[] message)
        {
            return this._hubpoxy.Invoke("Publish", message);
        }

        public virtual Task Send<T>(params object[] message)
        {
            return this._hubpoxy.Invoke("Send", message);
        }

        public virtual Task AddTopic(string topicName)
        {
            return this._hubpoxy.Invoke("JoinTopic", topicName);
        }

        public virtual Task ExitTopic(string topicName)
        {
            return this._hubpoxy.Invoke("LeaveTopic", topicName);
        }

        public virtual Task SendMessageToTopic<T>(T message, string topicName)
        {
            return this._hubpoxy.Invoke("SendMessageToTopic", message, topicName);
        }


        public IHubProxy proxy
        {
            get
            {
                return this._hubpoxy;
            }
        }



        public Task Start()
        {
            return this._hubconnection.Start();
        }

        public void Stop()
        {
            this._hubconnection.Stop();
            OnStop();
        }

        protected void OnStop()
        {
            var handler = this.Stoped;
            if (handler != null)
            {
                handler();
            }
        }
         
        public void Dispose()
        {
            Stop();
        }


    }
}
