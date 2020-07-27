#region "Libraries"

using System;

#endregion

namespace MPIS.Package.EventPublisher.AzureEventGrid.Abstraction.Entities
{
    public abstract class EventBase
    {
        public Guid Guid { get; set; }
        public DateTimeOffset DateTimeOffset { get; private set; }
        public long TimeElapsedMilliseconds { get; set; }



        protected EventBase(Guid guid, DateTimeOffset dateTimeOffset, long timeElapsedMilliseconds)
        {
            Guid = guid;
            DateTimeOffset = dateTimeOffset;
            TimeElapsedMilliseconds = timeElapsedMilliseconds;
        }

        public virtual bool IsValid()
        {
            return Guid != Guid.Empty
                && DateTimeOffset != null
                && TimeElapsedMilliseconds >= 0;
        }
    }
}
