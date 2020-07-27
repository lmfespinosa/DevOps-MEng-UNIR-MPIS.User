#region "Libraries"

using MPIS.User.AplicationService.DTOs.User;
using MPIS.User.EventGrid.Abstract;
using System;
using System.Threading.Tasks;

#endregion

namespace MPIS.User.Function.Unit.Tests.MockServices
{
    public class MockUserPublisher : IUserPublisher
    {
        private static MockUserPublisher _instance;
        private MockUserPublisher() { }

        public static MockUserPublisher Create()
        {
            if (_instance == null)
            {
                _instance = new MockUserPublisher();
            }
            return _instance;
        }


        public async Task Publish(CreateUserRequest user, Guid userId)
        {
            await Task.Run(() => CreateUserRequest());
        }

        public async Task Publish(DeleteUserByIdRequest user)
        {
            await Task.Run(() => CreateUserRequest());
        }

        public async Task Publish(UpdateUserRequest user)
        {
            await Task.Run(() => CreateUserRequest());
        }


        private CreateUserRequest CreateUserRequest() => new CreateUserRequest()
        {
            /*DateCreated = DateTime.UtcNow,
            DateUpdated = DateTime.UtcNow,
            DateSent = DateTime.UtcNow,
            Body = "Mensage send",
            ErrorMessage = string.Empty,
            From = "+34123456789",
            Sid = Guid.NewGuid().ToString(),
            Status = "Ok",
            To =*/
        };
    }
}
