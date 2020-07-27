#region "Libraries"

using MPIS.User.AplicationService.DTOs.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace MPIS.User.Repository.Abstract
{
    public interface IUserRepository
    {
        Task<Guid> CreateUserAsync(CreateUserRequest request);
        Task<bool> UpdateUserAsync(UpdateUserRequest request);
        Task<bool> DeleteUserByIdAsync(DeleteUserByIdRequest request);
        Task<List<UserResponse>> GetAllAvailableUsersAsync();
        Task<UserResponse> GetUserByIdAsync(GetUserByIdRequest request);
        Task<bool> IsValidUserByPasswordAsync(GetUserByPassEmailRequest request);
    }
}
