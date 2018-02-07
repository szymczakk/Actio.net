using System;
using System.Threading.Tasks;
using Actio.Common.Events;

namespace Actio.Api.Handlers
{
  public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
  {
    public ActivityCreatedHandler()
    {
        
    }
    
    public async Task Handle(ActivityCreated @event)
    {
      await Task.CompletedTask;
      Console.WriteLine($"Activity created: {@event.Name}");
    }
  }
}