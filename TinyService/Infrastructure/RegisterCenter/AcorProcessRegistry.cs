using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure.Process;

namespace TinyService.Infrastructure.RegisterCenter
{
   /* public class ActorProcessRegistry
    {
        private static volatile ActorProcessRegistry _Instance;

        private static object objlock = new object();

        private readonly ConcurrentDictionary<string, List<Actor>> _subjects = new ConcurrentDictionary<string, List<Actor>>();

        private ActorProcessRegistry() { }

        public static ActorProcessRegistry Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (objlock)
                    {
                        if (_Instance == null)
                            _Instance = new ActorProcessRegistry();
                    }
                }

                return _Instance;
            }
        }

        public void AddActor(Type type, Actor actorprocess, int num=1)
        {
            lock (objlock)
            {
                List<Actor> handlers = null;

                if (!this._subjects.TryGetValue(type.Name, out handlers))
                {
                    handlers = new List<Actor>();
                    for (int i = 0; i <num; i++)
                    {
                        handlers.Add(actorprocess);
                    }
                    this._subjects.TryAdd(type.Name, handlers);
                }
                else
                {
                    if (!handlers.Any(p => p.Equals(actorprocess)))
                    {
                        for (int i = 0; i < num; i++)
                        {
                            handlers.Add(actorprocess);
                        }
                    }
                }
            }



        }

        public Actor GetSignaleActor<T>()
        {
            return GetEventHandler(typeof(T)).FirstOrDefault();
        }

        public List<Actor> GetEventHandler(Type type)
        {
            List<Actor> handlers = null;
            this._subjects.TryGetValue(type.Name, out handlers);
            return handlers;
        }


        public ConcurrentDictionary<string, List<Actor>> Subjects
        {
            get
            {
                return this._subjects;
            }
        }
    }*/
}
