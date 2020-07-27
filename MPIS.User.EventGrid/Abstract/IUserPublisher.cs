#region "Libraries"

using MPIS.User.AplicationService.DTOs.User;
using System;
using System.Threading.Tasks;

#endregion

namespace MPIS.User.EventGrid.Abstract
{
    public interface IUserPublisher
    {
        Task Publish(CreateUserRequest user, Guid userId);
        Task Publish(DeleteUserByIdRequest user);
        Task Publish(UpdateUserRequest user);
    }
}
