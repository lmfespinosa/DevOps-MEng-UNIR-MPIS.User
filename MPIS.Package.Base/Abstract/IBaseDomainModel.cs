#region "Libraries"

using System;

#endregion

namespace MPIS.Package.Base.Abstract
{
    public interface IBaseDomainModel
    {
        Guid Id { get; set; }
        DateTime Created { get; set; }
        DateTime? Updated { get; set; }
        DateTime? Deleted { get; set; }
        bool IsActive();
    }
}
