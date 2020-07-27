using System;
using System.Collections.Generic;
using System.Text;

namespace MPIS.User.AplicationService.DTOs.User
{
    public class GetUserByPassEmailRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
