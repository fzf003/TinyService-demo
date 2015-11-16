//using Microsoft.AspNet.SignalR;
//using Microsoft.AspNet.SignalR.Client;
//using Microsoft.AspNet.SignalR.Messaging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TinyService.MessageBus.Impl
//{
//    public class SignalRMessageBus : ScaleoutMessageBus
//    {
//        private readonly Connection _connection;
//        private Task startTask;

//        public SignalRMessageBus(MessageBusConfiguration configuration, IDependencyResolver dependencyResolver)
//            : base(dependencyResolver, configuration)
//        {
//            _connection = new Connection(configuration.RemoteUrl);

//            startTask = _connection.Start();
//         }

      
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                _connection.Stop();
//            }
//        }
//    }
//}
