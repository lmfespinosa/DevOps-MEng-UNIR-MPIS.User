#region "Libraries"

using MPIS.Package.EventPublisher.AzureEventGrid.Abstraction.Entities;
using System;

#endregion

namespace MPIS.User.EventGrid.Events.User
{
    public class UserCreated : EventBase
    {
        public UserCreated(Guid guid, DateTimeOffset dateTimeOffset, long timeElapsedMilliseconds) : base(guid, dateTimeOffset, timeElapsedMilliseconds)
        {
        }

        public UserCreated() : this(Guid.NewGuid(), DateTimeOffset.UtcNow, 0)
        {
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Office { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
