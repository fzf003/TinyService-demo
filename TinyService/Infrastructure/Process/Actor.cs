using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using TinyService.Infrastructure.RegisterCenter;

namespace TinyService.Infrastructure.Process
{
    public abstract class Actor
    {
        private readonly ActionBlock<Message.Message> _action;

        int actorindex = 0;
        public Actor()
        {
            _action = new ActionBlock<Message.Message>(message =>
            {
                dynamic self = this;
                dynamic mess = message;
                self.Handle(mess);
              
            });

        }


        protected void Publish<TActor>(Message.Message message) where TActor : Actor
        {
            var list = ActorProcessRegistry.Instance.GetEventHandler(typeof(TActor));

            list[(Interlocked.Increment(ref actorindex) % list.Count)].Send(message);
        }

        public Task<bool> Send(Message.Message message)
        {
           return _action.SendAsync(message);
        }

        public Task Completion
        {
            get
            {
                _action.Complete();
                return _action.Completion;
            }
        }



    }
}
