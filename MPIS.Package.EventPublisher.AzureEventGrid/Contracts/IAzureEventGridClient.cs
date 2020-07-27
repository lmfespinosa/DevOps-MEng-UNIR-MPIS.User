#region "Libraries"

using Microsoft.Azure.EventGrid.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace MPIS.Package.EventPublisher.AzureEventGrid.Contracts
{
    public interface IAzureEventGridClient
    {
        Task PublishEventsAsync(string topicHostname, IList<EventGridEvent> events, CancellationToken cancellationToken = default(CancellationToken));
    }
}
