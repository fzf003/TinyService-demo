using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;
namespace TinyService.Command.Impl
{
     [Component(IsSingleton = true)]
    public class DefaultCommandMapper : ICommandMapper
    {
        ConcurrentDictionary<Type, Type> typeservicestore = new ConcurrentDictionary<Type, Type>();
        public ICommandHandler<TCommand> GetCommandAction<TCommand>() where TCommand : class, ICommand
        {
            var handlerType = typeservicestore.GetOrAdd(typeof(TCommand), (type) =>
            {
                var handlerInheritingFromType = typeof(ICommandHandler<>).MakeGenericType(typeof(TCommand));
                var commandHandlerType = GetCommandHandlerType(handlerInheritingFromType);
                return commandHandlerType;
            });

            return ObjectFactory.GetService<ICommandHandler<TCommand>>(handlerType);
        }

        private Type GetCommandHandlerType(Type handlerInheritingFromType)
        {
            var commandHandlerType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
               .SingleOrDefault(x => x.GetInterfaces().Any(y => y == handlerInheritingFromType));
            return commandHandlerType;
        }
    }
}
