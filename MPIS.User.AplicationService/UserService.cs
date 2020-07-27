#region "Libraries"

using AutoMapper;
using MPIS.User.AplicationService.DTOs.User;
using MPIS.User.ApplicationService.Abstract;
using MPIS.User.EventGrid.Abstract;
using MPIS.User.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace MPIS.User.ApplicationService
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserPublisher _userPublisher;

        public UserService(IUserRepository userRepository, IMapper mapper, IUserPublisher userPublisher) {
            _userRepository = userRepository;
            _userPublisher = userPublisher;
            _mapper = mapper;
        }
        public async Task<Guid> CreateUserAsync(CreateUserRequest request)
        {
            var id = await _userRepository.CreateUserAsync(request);

            //await _userPublisher.Publish(request, id);

            return id;
        }

        public async Task<bool> DeleteUserByIdAsync(DeleteUserByIdRequest request)
        {
            var result = await _userRepository.DeleteUserByIdAsync(request);
            //await _userPublisher.Publish(request);
            return result;
        }

        public async Task<List<UserResponse>> GetAllAvailableUsersAsync()
        {
             return await _userRepository.GetAllAvailableUsersAsync();
        }

        public async Task<UserResponse> GetUserByIdAsync(GetUserByIdRequest request)
        {
            return await _userRepository.GetUserByIdAsync(request);
        }

        public async Task<bool> IsValidUserByPasswordAsync(GetUserByPassEmailRequest request)
        {
            return await _userRepository.IsValidUserByPasswordAsync(request);
        }

        public async Task<bool> UpdateUserAsync(UpdateUserRequest request)
        {
            var result = await _userRepository.UpdateUserAsync(request);

            //await _userPublisher.Publish(request);
            return result;
        }
    }
}
