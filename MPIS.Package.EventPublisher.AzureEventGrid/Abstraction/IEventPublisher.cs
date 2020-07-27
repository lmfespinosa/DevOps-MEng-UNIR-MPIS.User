#region "Libraries"

using MPIS.Package.EventPublisher.AzureEventGrid.Abstraction.Entities;
using System.Threading.Tasks;

#endregion

namespace MPIS.Package.EventPublisher.AzureEventGrid.Abstraction
{
    public interface IEventPublisher
    {
        Task Publish<TEvent>(TEvent eventBase) where TEvent : EventBase, new();
    }
}
