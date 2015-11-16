using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.MessageBus;
using TinyService.MessageBus.Contract;

namespace TinyService.Application.Hub
{
    [HubName("ClientHub")]
    public class ClientHub : Hub<IClientHub>, IServerHub
    {
        public ClientHub()
        {
        }
        public override Task OnConnected()
        {
            Console.WriteLine("User connected: " + Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Console.WriteLine("客户端 {0}已断开: " , Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        //public void Greetings(string name)
        //{

        //    ///除了
        //    //this.Clients.AllExcept().Open("");
        //    ///调用者
        //    //this.Clients.Caller.Open("");

        //    //this.Clients.OthersInGroup("").Open("");

        //  //  return string.IsNullOrEmpty(name) ? "Hello world!" : string.Format("Hello {0}!", name);
        //}

        //public void Execute(string name,int index)
        //{
        //    var message = new AppMessage(Guid.NewGuid().ToString("N"));
        //    message.Name = string.Format("CLose:{0}-------{1}", Guid.NewGuid().ToString("N"), index);
        //    this.Clients.All.Close(message);
        //}

        public void JoinTopic(string topicName)
        {
            Groups.Add(Context.ConnectionId, topicName).ContinueWith(task =>
            {
                 Console.WriteLine(Context.ConnectionId + " joined " + topicName);
            });
        }

        public void LeaveTopic(string topicName)
        {
            Groups.Remove(Context.ConnectionId, topicName).Wait();
            Console.WriteLine(Context.ConnectionId + " removed from " + topicName);
        }

        public void SendMessageToTopic(AppMessage message, string topicName)
        {
            var appmessage = new AppMessage();
            appmessage.Name = string.Format("来自{0}的消息:{1}", topicName, message);

            this.Clients.Group(topicName).Close(appmessage);
        }

        /// <summary>
        /// 单向onWay
        /// </summary>
        /// <param name="message"></param>
        public void Send(AppMessage message)
        {
            var appmessage = new AppMessage();
            appmessage.Name = string.Format("来自{0}的消息:{1}", "Send", message);
            this.Clients.Caller.Receive(appmessage);
        }

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="message"></param>
        public void Publish(AppMessage message)
        {
             this.Clients.All.Notify(message);
        }
    }
}
