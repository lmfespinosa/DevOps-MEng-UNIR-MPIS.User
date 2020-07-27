#region "Libraries"

using MPIS.Package.Base.Abstract;
using System;

#endregion

namespace MPIS.Package.Base
{
    public class BaseDomainModel : IBaseDomainModel
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set ; }
        public DateTime? Updated { get ; set ; }
        public DateTime? Deleted { get ; set; }

        public bool IsActive() => !Deleted.HasValue || Deleted.Value >= DateTime.UtcNow;
 
    }
}
