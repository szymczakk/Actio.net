using System.Threading.Tasks;

namespace Actio.Common.Events
{
    public interface IEventHandler<in T> where T: IEvent
    {
         Task Handle(T @event);
    }
}