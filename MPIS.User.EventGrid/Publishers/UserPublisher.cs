#region "Libraries"

using AutoMapper;
using MPIS.Package.EventPublisher.AzureEventGrid.Abstraction;
using MPIS.User.AplicationService.DTOs.User;
using MPIS.User.EventGrid.Abstract;
using MPIS.User.EventGrid.Events.User;
using System;
using System.Threading.Tasks;

#endregion

namespace MPIS.User.EventGrid.Publishers
{
    public class UserPublisher : IUserPublisher
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;

        public UserPublisher(IEventPublisher eventPublisher, IMapper mapper)
        {
            _eventPublisher = eventPublisher;
            _mapper = mapper;

        }

        public async Task Publish(CreateUserRequest user, Guid userId)
        {
            var @event = _mapper.Map<UserCreated>(user);
            @event.Id = userId;

            await _eventPublisher.Publish(@event);
        }

        public async Task Publish(DeleteUserByIdRequest user)
        {
            var @event = _mapper.Map<UserDeleted>(user);

            await _eventPublisher.Publish(@event);
        }

        public async Task Publish(UpdateUserRequest user)
        {
            var @event = _mapper.Map<UserUpdated>(user);

            await _eventPublisher.Publish(@event);
        }
    }
}
