#region "Libraries"

using Microsoft.Extensions.Configuration;
using MPIS.Package.EventPublisher.AzureEventGrid.Configurations.Abstractions;

#endregion

namespace MPIS.Package.EventPublisher.AzureEventGrid.Configurations.Implementation
{
    public class AzureEventGridClientConfiguration : IAzureEventGridClientConfiguration
    {
        private const string DefaultConfigPrefix = "AzureEventGridClient";
        private static string _configPrefix = DefaultConfigPrefix;

        private readonly IConfigurationRoot _configurationRoot;

        /// <summary>
        /// EventGridTopicEventPublisherConfiguration constructor
        /// </summary>
        /// <param name="configurationRoot">Repository to retrieve configuration</param>
        /// <param name="configurationPrefix">Expected configuration prefix, will use 'EventGridClient' by default</param>
        public AzureEventGridClientConfiguration(IConfigurationRoot configurationRoot, string configurationPrefix = DefaultConfigPrefix)
        {
            _configurationRoot = configurationRoot;
            _configPrefix = configurationPrefix;
        }

        public string TopicKey => _configurationRoot.GetValue<string>($"{_configPrefix}.TopicKey");
    }
}

