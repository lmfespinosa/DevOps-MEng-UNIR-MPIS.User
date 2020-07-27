#region "Libraries"

using AutoMapper;
using MPIS.User.AplicationService.DTOs.User;
using MPIS.User.EventGrid.Events.User;

#endregion

namespace MPIS.User.AutoMapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<DomainModel.User, CreateUserRequest>();
            CreateMap<CreateUserRequest, DomainModel.User>();
            CreateMap<DomainModel.User, UpdateUserRequest>();
            CreateMap<UpdateUserRequest, DomainModel.User>();
            CreateMap<DomainModel.User, UserResponse>();
            CreateMap<UserResponse,DomainModel.User>();
            CreateMap<CreateUserRequest, UserCreated>();
            CreateMap<UpdateUserRequest, UserUpdated>();
            CreateMap<DeleteUserByIdRequest, UserDeleted>();

            CreateMap<CreateUserRequest, UserResponse>();
            CreateMap<UserResponse, CreateUserRequest>();
        }
    }
}
