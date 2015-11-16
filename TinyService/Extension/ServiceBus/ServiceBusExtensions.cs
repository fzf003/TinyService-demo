using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;

namespace TinyService.Extension.ServiceBus
{
    public static class ServiceBusExtensions
    {

        public static Task PublishAsync<T>(this IServiceBus bus, T message)
        {
            var sendaction = new Action(() => { bus.Publish<T>(message); });
            return Task.Factory.FromAsync(sendaction.BeginInvoke, sendaction.EndInvoke, null);
        }

        public static Task PublishAsync<T>(this IServiceBus bus,IEnumerable< T> messages)
        {
            var sendaction = new Action(() => {
                foreach (var message in messages)
                {
                    bus.Publish<T>(message);
                }
            
            });
            return Task.Factory.FromAsync(sendaction.BeginInvoke, sendaction.EndInvoke, null);
        }

        public static IDisposable ToSubscribe<T>(this IServiceBus bus, Action<T> action)
        {
            return bus.Subscribe(new Handler<T>(action));
        }

        public static Task SendAsync<T>(this IServiceBus bus, T message) where T : class
        {
            return Task.Run(() => { bus.Send<T>(message); });
        }

        public static IDisposable ToSubscribe<T>(this IObservable<T> self, Action<T> action)
        {
            return self.Subscribe(new Handler<T>(action));
        }

    }
}
