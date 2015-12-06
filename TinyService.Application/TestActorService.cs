using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TinyService.Infrastructure;

using TinyService.Infrastructure.Process.Actor;

namespace TinyService.Application
{
    public class TestActorService : Actor
    {

        public TestActorService(ActorApplication factory)
            : this(null, factory)
        {
        }

        public TestActorService(Uri uri, ActorApplication factory)
            : base(uri, factory)
        {
        }


        public async Task Handle(AddMessage message)
        {
            //await Task.Delay(1000);
            var result = await Task.FromResult("TestActorService:" + message.Index + "---" + message.Id + "|" + message.Timestamp + "|" + this.Uri.ToString() + "===" + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(result);
        }

        public async Task Handle(UpdateMessage message)
        {
            var result = await Task.FromResult("TestActorService000:" + message.Timestamp + "---" + message.Id + "|" + message.Timestamp + "|" + this.Uri.ToString() + "===" + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(result);
        }
    }

    public class AddMessage : ActorMessage
    {
        public AddMessage(string actorname)
        {
            this.To = new Address(actorname);
        }
        public int Index { get; set; }
    }

    public class UpdateMessage : ActorMessage
    {
        public UpdateMessage(string actorname)
        {
            this.To = new Address(actorname);
        }
    }
}
