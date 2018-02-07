using System.Reflection;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Pipe;
using Microsoft.Extensions.Configuration;
using RawRabbit.Instantiation;

namespace Actio.Common.RabbitMq
{
    public static class Extensions
    {
        public static Task WithCommandHandler<TCommand>(this IBusClient bus, ICommandHandler<TCommand> handler) where TCommand: ICommand => bus.SubscribeAsync<TCommand>(msg => handler.Handle(msg),
        ctx => ctx.UseSubscribeConfiguration(cfg => cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TCommand>()))));

        public static Task WithEventHandler<TEvent>(this IBusClient bus, IEventHandler<TEvent> handler) where TEvent: IEvent => bus.SubscribeAsync<TEvent>(msg => handler.Handle(msg),
        ctx => ctx.UseSubscribeConfiguration(cfg => cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TEvent>()))));

        private static string GetQueueName<T>() => $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";

        public static void AddRabbitMq(this IServiceCollection servoice, IConfiguration configuration)
        {
            var options = new RabbitMqOptions();
            var section = configuration.GetSection("rabbitmq");
            section.Bind(options);

            var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions{
                ClientConfiguration = options
            });

            servoice.AddSingleton<IBusClient>(client);
        }
    }
}