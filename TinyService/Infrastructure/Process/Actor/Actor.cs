using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using TinyService.Infrastructure;

namespace TinyService.Infrastructure.Process.Actor
{
    public abstract  class Actor
    {
        private readonly ActionBlock<ActorMessage> _actorhandler;
        public Uri Uri { get; set; }

        public ActorApplication factory { get; set; }
        public Actor(ActorApplication factory)
            : this(null,factory)
        {

        }

        public Actor(Uri uri, ActorApplication factory)
        {
            _actorhandler = new ActionBlock<ActorMessage>(async message =>
            {
                dynamic self = this;
                dynamic mess = message;
                self.Handle(mess);
            });

            this.Uri = uri;
            this.factory = factory;
        }

        public Task<bool> SendAsync(ActorMessage message)
        {
            return _actorhandler.SendAsync(message);
        }

        public Task Completion
        {
            get
            {
                _actorhandler.Complete();
                return _actorhandler.Completion;
            }
        }



    }
}
