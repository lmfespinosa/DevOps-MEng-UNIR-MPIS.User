#region "Libraries"

using Microsoft.Extensions.Configuration;
using MPIS.Package.EventPublisher.AzureEventGrid.Configurations.Abstractions;

#endregion

namespace MPIS.Package.EventPublisher.AzureEventGrid.Configurations.Implementation
{
    public class EventGridTopicEventPublisherConfiguration : IEventGridTopicEventPublisherConfiguration
    {
        private const string DefaultConfigPrefix = "EventGridTopicEventPublisher";
        private static string _configPrefix = DefaultConfigPrefix;

        private readonly IConfigurationRoot _configurationRoot;

        /// <summary>
        /// EventGridTopicEventPublisherConfiguration constructor
        /// </summary>
        /// <param name="configurationRoot">Repository to retrieve configuration</param>
        /// <param name="configurationPrefix">Expected configuration prefix, will use 'EventGridClient' by default</param>
        public EventGridTopicEventPublisherConfiguration(IConfigurationRoot configurationRoot, string configurationPrefix = DefaultConfigPrefix)
        {
            _configurationRoot = configurationRoot;
            _configPrefix = configurationPrefix;
        }

        public string TopicEndPoint => _configurationRoot.GetValue<string>($"{_configPrefix}.TopicEndPoint");

        public string SubjectBasePath => _configurationRoot.GetValue<string>($"{_configPrefix}.SubjectBasePath", "BasePath");

        public bool CheckEventValidity => _configurationRoot.GetValue<bool>($"{_configPrefix}.CheckEventValidity", true);

        public bool ThrowExceptions => _configurationRoot.GetValue<bool>($"{_configPrefix}.ThrowExceptions", true);
    }
}
