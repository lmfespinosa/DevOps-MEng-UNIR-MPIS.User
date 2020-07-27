using System;
using System.Collections.Generic;
using System.Text;

namespace MPIS.User.AplicationService.DTOs.User
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Office { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
