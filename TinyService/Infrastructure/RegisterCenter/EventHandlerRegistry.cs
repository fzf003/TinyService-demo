using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Infrastructure.RegisterCenter
{
    public class EventHandlerRegistry
    {
        private static volatile EventHandlerRegistry _Instance;

        private static object objlock = new object();

        private readonly ConcurrentDictionary<Type, List<object>> _subjects = new ConcurrentDictionary<Type, List<object>>();

        private EventHandlerRegistry() { }

        public static EventHandlerRegistry Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (objlock)
                    {
                        if (_Instance == null)
                            _Instance = new EventHandlerRegistry();
                    }
                }

                return _Instance;
            }
        }

        public void AddEventHandler(Type type, object eventhandler)
        {
            lock (objlock)
            {
                List<object> handlers = null;

                if (!this._subjects.TryGetValue(type, out handlers))
                {
                    handlers = new List<object>();
                    handlers.Add(eventhandler);
                    this._subjects.TryAdd(type, handlers);
                }
                else
                {
                    if (!handlers.Any(p => p.Equals(eventhandler)))
                    {
                        handlers.Add(eventhandler);
                    }
                }
            }



        }

        public List<object> GetEventHandler(Type type)
        {
            List<object> handlers = null;
            this._subjects.TryGetValue(type, out handlers);
            return handlers;
        }


        public ConcurrentDictionary<Type, List<object>> Subjects
        {
            get
            {
                return this._subjects;
            }
        }
    }
}
