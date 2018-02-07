using System;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.RabbitMq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using RawRabbit;

namespace Actio.Common.Services
{
  public class ServiceHost : IServiceHost
  {

    private readonly IWebHost _webHost;

    public ServiceHost(IWebHost webHost)
    {
      this._webHost = webHost;

    }
    public void Run() => _webHost.Run();

    public static HostBuilder Create<TStartup>(string[] args) where TStartup : class
    {
      Console.Title = typeof(TStartup).Namespace;
      var config = new ConfigurationBuilder()
          .AddEnvironmentVariables()
          .AddCommandLine(args)
          .Build();

      var webHostBuilder = WebHost.CreateDefaultBuilder(args)
          .UseConfiguration(config)
          .UseStartup<TStartup>();

      return new HostBuilder(webHostBuilder.Build());
    }

    public abstract class BuilerBase
    {
      public abstract ServiceHost Build();
    }

    public class HostBuilder : BuilerBase
    {
      private readonly IWebHost _webHost;
      private IBusClient _bus;

      public HostBuilder(IWebHost webHost)
      {
        this._webHost = webHost;
      }

      public BusBuilder UserRabbitMq()
      {
          _bus = (IBusClient)_webHost.Services.GetService(typeof(IBusClient));

          return new BusBuilder(_webHost, _bus);
      }

      public override ServiceHost Build()
      {
        return new ServiceHost(_webHost);
      }
    }

    public class BusBuilder: BuilerBase
    {
      private readonly IWebHost _webHost;
      private IBusClient _bus;

      public BusBuilder(IWebHost webHost, IBusClient bus)
      {
        this._webHost = webHost;
        this._bus = bus;
      }

      public BusBuilder SubscribeToCommand<TCommand>() where TCommand: ICommand
      {
        var handler = (ICommandHandler<TCommand>)_webHost.Services.GetService(typeof(ICommandHandler<TCommand>));
        
        _bus.WithCommandHandler(handler);
        return this;
      }

      public BusBuilder SubscribeToEvent<TEvent>() where TEvent: IEvent
      {
        var handler = (IEventHandler<TEvent>)_webHost.Services.GetService(typeof(IEventHandler<TEvent>));
        
        _bus.WithEventHandler(handler);
        return this;
      }

      public override ServiceHost Build()
      {
          throw new NotImplementedException();
      }
    }
  }
}