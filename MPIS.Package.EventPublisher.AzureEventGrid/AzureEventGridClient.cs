#region "Libraries"

using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using MPIS.Package.EventPublisher.AzureEventGrid.Configurations.Abstractions;
using MPIS.Package.EventPublisher.AzureEventGrid.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace MPIS.Package.EventPublisher.AzureEventGrid
{
    public class AzureEventGridClient : IAzureEventGridClient
    {
        private readonly IEventGridClient _eventGridClient;
        private readonly IAzureEventGridClientConfiguration _configuration;

        public AzureEventGridClient(IAzureEventGridClientConfiguration configuration)
        {
            _configuration = configuration;

            var topicCredentials = new TopicCredentials(_configuration.TopicKey);
            _eventGridClient = new EventGridClient(topicCredentials);
        }

        public Task PublishEventsAsync(string topicHostname, IList<EventGridEvent> events, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _eventGridClient.PublishEventsAsync(topicHostname, events, cancellationToken);
        }
    }
}
