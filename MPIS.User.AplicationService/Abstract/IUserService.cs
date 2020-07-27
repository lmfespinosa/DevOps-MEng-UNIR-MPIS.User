#region "Libraries"

using MPIS.User.AplicationService.DTOs.User;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

#endregion

namespace MPIS.User.ApplicationService.Abstract
{
    public interface IUserService
    {
        Task<Guid> CreateUserAsync(CreateUserRequest request);
        Task<bool> UpdateUserAsync(UpdateUserRequest request);
        Task<bool> DeleteUserByIdAsync(DeleteUserByIdRequest request);
        Task<List<UserResponse>> GetAllAvailableUsersAsync();
        Task<UserResponse> GetUserByIdAsync(GetUserByIdRequest request);
        Task<bool> IsValidUserByPasswordAsync(GetUserByPassEmailRequest request);
    }
}
