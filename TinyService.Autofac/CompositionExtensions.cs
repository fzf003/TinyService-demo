namespace TinyService.autofac
{
    using Autofac;
    using Autofac.Builder;
    using Autofac.Core;
    using Autofac.Features.Scanning;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using TinyService.Infrastructure.CommonComposition;

    public static class CompositionExtensions
    {

        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> RegisterComponents(this ContainerBuilder builder, params Assembly[] assemblies)
        {

            return RegisterComponents(builder, assemblies.SelectMany(x => x.GetTypes()));
        }


        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> RegisterComponents(this ContainerBuilder builder, params Type[] types)
        {
            return RegisterComponents(builder, (IEnumerable<Type>)types);
        }


        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> RegisterComponents(this ContainerBuilder builder, IEnumerable<Type> types)
        {
            var registration = builder
                .RegisterTypes(types.Where(t => t.GetCustomAttributes(true).OfType<ComponentAttribute>().Any()).ToArray())
                .AsSelf()
                .AsImplementedInterfaces();


            registration.As(t =>
            {
                var name = t.GetCustomAttributes(true).OfType<NamedAttribute>().Select(x => x.Name).FirstOrDefault();
                if (string.IsNullOrEmpty(name))
                    return Enumerable.Empty<Service>();

                return t.GetInterfaces()
                    .Where(i => i != typeof(IDisposable))
                    .Select(i => new KeyedService(name, i))
                    .Concat(new[] { new KeyedService(name, t) })
                    .ToArray();
            });

            registration.ActivatorData.ConfigurationActions.Add((t, rb) =>
            {

                if (rb.ActivatorData.ImplementationType.GetCustomAttributes(true).OfType<ComponentAttribute>().First().IsSingleton)
                    rb.SingleInstance();
            });

            return registration;
        }
    }
}