using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TinyService.Infrastructure;

namespace TinyService.Infrastructure.Process.Actor
{
    public class ActorApplication
    {
        private readonly IActorRegisterService _actorregisterservice;
        private readonly List<Uri> _endpointAddresses = new List<Uri>();
        private readonly ConcurrentDictionary<Address, MailBox> _localAddresses = new ConcurrentDictionary<Address, MailBox>();
        private readonly ConcurrentDictionary<MailBox, List<Actor>> _localactor = new ConcurrentDictionary<MailBox, List<Actor>>();
        static Lazy<ActorApplication> _Instanse = new Lazy<ActorApplication>(() => new ActorApplication());

        private ActorApplication()
        {
            this._actorregisterservice = new DefaultInMemoryActorRegisterService();
        }

        public static ActorApplication Instanse
        {
            get
            {
                return _Instanse.Value;
            }
        }

        public Task<bool> Dispatcher(ActorMessage message)
        {
            MailBox mailbox=null;
            if (_localAddresses.TryGetValue(message.To, out mailbox))
            {
                mailbox.Add(message);
                return Task.FromResult<bool>(true);
            }
            return Task.FromResult<bool>(false);
        }

        public void AddMailBox(string actorName, Func<Actor> func, int count = 1)
        {
            var address = new Address(actorName);

            if (!_localAddresses.ContainsKey(address))
            {
                var mailBox = new MailBox(new BlockingCollection<ActorMessage>());

                _localAddresses.TryAdd(address, mailBox);

                var actors = new List<Actor>();

                for (int i = 0; i < count; i++)
                {
                    var universalActorName = string.Format("actor://{0}/{1}", actorName, Guid.NewGuid().ToString("N"));

                    var actor = func();

                    actor.Uri = new Uri(universalActorName);
                    
                    actors.Add(actor);
                }

                _localactor.TryAdd(mailBox, actors);

                new TaskFactory(TaskCreationOptions.LongRunning, TaskContinuationOptions.None)
                   .StartNew(async () =>
                   {
                       var messages = mailBox.Messages;
                       foreach (var message in messages.GetConsumingEnumerable())
                       {
                            List<Actor> actorstore;
                           if (_localactor.TryGetValue(mailBox, out  actorstore))
                           {
                               var index = mailBox.Index;
                               mailBox.Index = Interlocked.Increment(ref index);
                               var selectactor = mailBox.Index  % actorstore.Count;
                               await actorstore[selectactor].SendAsync(message);
                           }
                       }
                   });

            }
        }

        public MailBox GetMailBox(string actorname)
        {
            MailBox mailbox;
            if (!_localAddresses.TryGetValue(new Address(actorname), out mailbox))
            {
                throw new ArgumentNullException("mailbox is null");
            }
            return mailbox;
        }


        public IDictionary<MailBox, List<Actor>> EndpointAddresses
        {
            get
            {
                return _localactor;
            }
        }
    }
}
