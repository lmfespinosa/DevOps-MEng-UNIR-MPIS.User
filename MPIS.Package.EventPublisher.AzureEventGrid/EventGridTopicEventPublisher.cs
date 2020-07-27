#region "Libraries"

using Microsoft.Azure.EventGrid.Models;
using MPIS.Package.EventPublisher.AzureEventGrid.Abstraction;
using MPIS.Package.EventPublisher.AzureEventGrid.Abstraction.Entities;
using MPIS.Package.EventPublisher.AzureEventGrid.Configurations.Abstractions;
using MPIS.Package.EventPublisher.AzureEventGrid.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace MPIS.Package.EventPublisher.AzureEventGrid
{
    public class EventGridTopicEventPublisher : IEventPublisher
    {
        private readonly IEventGridTopicEventPublisherConfiguration _configuration;
        private readonly IAzureEventGridClient _client;

        public EventGridTopicEventPublisher(IEventGridTopicEventPublisherConfiguration configuration,
            IAzureEventGridClient client)
        {
            _configuration = configuration;
            _client = client;
        }

        public async Task Publish<TEvent>(TEvent @event) where TEvent : EventBase, new()
        {
            try
            {
                if (_configuration.CheckEventValidity && !@event.IsValid())
                {
                    throw new ArgumentOutOfRangeException(nameof(@event), "Event validity failed");
                }

                await PublishInternal<TEvent>(@event);
            }
            catch (Exception)
            {
                var errorMessage = $"Failed executing Publish<{typeof(EventBase).Name}>()";
                if (_configuration.ThrowExceptions) throw;
            }
        }

        private async Task PublishInternal<TEvent>(TEvent @event) where TEvent : EventBase, new()
        {
            string topicHostname = new Uri(_configuration.TopicEndPoint).Host;
            await _client.PublishEventsAsync(topicHostname, BuildEventGridList(@event, _configuration.SubjectBasePath)).ConfigureAwait(false);
        }

        private IList<EventGridEvent> BuildEventGridList<TEvent>(TEvent @event, string subjectBasePath) where TEvent : EventBase, new()
        {
            List<EventGridEvent> eventsList = new List<EventGridEvent>
            {
                new EventGridEvent()
                {
                    Id = @event.Guid.ToString(),
                    EventType = typeof(TEvent).Name,
                    Data = @event,
                    EventTime = @event.DateTimeOffset.UtcDateTime,

                    // This field allow filtering the events in the subscribers, for example: ContentManager/ItemPublished
                    Subject = BuildSubject<TEvent>(subjectBasePath),
                    DataVersion = "1.0" // Since we do not have data version at the base event class, for now we use always same version
                }
            };

            return eventsList;
        }

        private static string BuildSubject<TEvent>(string subjectBasePath) where TEvent : EventBase, new()
        {
            return subjectBasePath.EndsWith("/")
                ? $"{subjectBasePath}{typeof(TEvent).Name}"
                : $"{subjectBasePath}/{typeof(TEvent).Name}";
        }
    }
}
