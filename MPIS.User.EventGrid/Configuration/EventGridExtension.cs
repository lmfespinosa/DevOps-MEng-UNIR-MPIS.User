#region "Libraries"

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MPIS.Package.EventPublisher.AzureEventGrid;
using MPIS.Package.EventPublisher.AzureEventGrid.Abstraction;
using MPIS.Package.EventPublisher.AzureEventGrid.Configurations.Abstractions;
using MPIS.Package.EventPublisher.AzureEventGrid.Configurations.Implementation;
using MPIS.Package.EventPublisher.AzureEventGrid.Contracts;
using MPIS.User.EventGrid.Abstract;
using MPIS.User.EventGrid.Publishers;

#endregion

namespace MPIS.User.EventGrid.Configuration
{
    public static class EventGridExtension
    {
        public static void AddEventGridService(this IFunctionsHostBuilder builder)
        {
            // Configuration
            builder.Services.AddSingleton<IAzureEventGridClientConfiguration, AzureEventGridClientConfiguration>(
                serviceProvider => new AzureEventGridClientConfiguration(serviceProvider.GetService<IConfigurationRoot>()));

            builder.Services.AddSingleton<IEventGridTopicEventPublisherConfiguration, EventGridTopicEventPublisherConfiguration>(
                serviceProvider => new EventGridTopicEventPublisherConfiguration(serviceProvider.GetService<IConfigurationRoot>()));

            // Client
            builder.Services.AddSingleton<IAzureEventGridClient, AzureEventGridClient>();

            // Publishers
            builder.Services.AddScoped<IEventPublisher, EventGridTopicEventPublisher>();
            builder.Services.AddScoped<IUserPublisher, UserPublisher>();
        }
    }
}
