using System.Reflection;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;
using RawRabbit;
using RawRabbit.Pipe;

namespace Actio.Common.RabbitMq
{
    public static class Extensions
    {
        public static Task WithCommandHandler<TCommand>(this IBusClient bus, ICommandHandler<TCommand> handler) where TCommand: ICommand => bus.SubscribeAsync<TCommand>(msg => handler.Handle(msg),
        ctx => ctx.UseSubscribeConfiguration(cfg => cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TCommand>()))));

        public static Task WithEventHandler<TEvent>(this IBusClient bus, IEventHandler<TEvent> handler) where TEvent: IEvent => bus.SubscribeAsync<TEvent>(msg => handler.Handle(msg),
        ctx => ctx.UseSubscribeConfiguration(cfg => cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TEvent>()))));

        private static string GetQueueName<T>() => $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";
    }
}