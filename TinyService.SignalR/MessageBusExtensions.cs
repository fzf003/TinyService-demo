using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.MessageBus.Impl;
using Microsoft.AspNet.SignalR.Client;

namespace TinyService.MessageBus
{
    public static class MessageBusExtensions
    {
        public static IObservable<Exception> ErrorAsObservable(this IMessageBus bus)
        {
            return Observable.FromEvent<Exception>(handler => bus.Error += handler, handler => bus.Error -= handler);
        }

        public static IObservable<Unit> StopAsObservable(this IMessageBus bus)
        {
            return Observable.FromEvent(handler => bus.Stoped += handler, handler => bus.Stoped -= handler);
        }

        public static void Subscribe<T1>(this IMessageBus messagebus, string eventname, Action<T1> action)
        {
            var handler = action;
            if (handler != null)
                messagebus.proxy.On<T1>(eventname, handler);
        }

        public static void Subscribe<T1, T2>(this IMessageBus messagebus, string eventname, Action<T1, T2> action)
        {
            var handler = action;
            if (handler != null)
                messagebus.proxy.On<T1, T2>(eventname, handler);

        }

        public static void Subscribe<T1, T2, T3>(this IMessageBus messagebus, string eventname, Action<T1, T2, T3> action)
        {
            var handler = action;
            if (handler != null)
                messagebus.proxy.On<T1, T2, T3>(eventname, handler);
        }

        public static void Subscribe<T1, T2, T3, T4>(this IMessageBus messagebus, string eventname, Action<T1, T2, T3, T4> action)
        {
            var handler = action;
            if (handler != null)
                messagebus.proxy.On<T1, T2, T3, T4>(eventname, handler);
        }

        public static void Subscribe<T1, T2, T3, T4, T5>(this IMessageBus messagebus, string eventname, Action<T1, T2, T3, T4, T5> action)
        {
            var handler = action;
            if (handler != null)
                messagebus.proxy.On<T1, T2, T3, T4, T5>(eventname, handler);
        }

        public static void Subscribe<T1, T2, T3, T4, T5, T6>(this IMessageBus messagebus, string eventname, Action<T1, T2, T3, T4, T5, T6> action)
        {
            var handler = action;
            if (handler != null)
                messagebus.proxy.On<T1, T2, T3, T4, T5, T6>(eventname, handler);
        }

        public static void Subscribe<T1, T2, T3, T4, T5, T6, T7>(this IMessageBus messagebus, string eventname, Action<T1, T2, T3, T4, T5, T6, T7> action)
        {
            var handler = action;
            if (handler != null)
                messagebus.proxy.On<T1, T2, T3, T4, T5, T6, T7>(eventname, handler);
        }

    }
}

