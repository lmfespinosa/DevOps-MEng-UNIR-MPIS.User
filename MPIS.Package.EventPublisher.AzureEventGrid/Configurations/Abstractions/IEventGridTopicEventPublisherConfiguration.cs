using System;
using System.Collections.Generic;
using System.Text;

namespace MPIS.Package.EventPublisher.AzureEventGrid.Configurations.Abstractions
{
    public interface IEventGridTopicEventPublisherConfiguration
    {
        string TopicEndPoint { get; }

        /// <summary>
        /// // This field allow filtering the events in the subscribers, for example: ContentManager/ItemPublished
        /// </summary>
        string SubjectBasePath { get; }

        /// <summary>
        /// If true will call IsValid method to check the validity of the message, 
        /// not allowing to send the message if is not valid.
        /// </summary>
        bool CheckEventValidity { get; }

        /// <summary>
        /// If true will throw exceptions when required
        /// </summary>
        bool ThrowExceptions { get; }
    }
}
