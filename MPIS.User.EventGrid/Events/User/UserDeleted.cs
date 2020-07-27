#region "Libraries"

using MPIS.Package.EventPublisher.AzureEventGrid.Abstraction.Entities;
using System;

#endregion

namespace MPIS.User.EventGrid.Events.User
{
    public class UserDeleted : EventBase
    {
        public UserDeleted(Guid guid, DateTimeOffset dateTimeOffset, long timeElapsedMilliseconds) : base(guid, dateTimeOffset, timeElapsedMilliseconds)
        {
        }

        public UserDeleted() : this(Guid.NewGuid(), DateTimeOffset.UtcNow, 0)
        {
        }

        public Guid Id { get; set; }
    }
}
