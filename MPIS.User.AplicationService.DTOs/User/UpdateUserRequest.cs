#region "Libraries"

using System;

#endregion

namespace MPIS.User.AplicationService.DTOs.User
{
    public class UpdateUserRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Office { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
