#region "Libraries"

using Newtonsoft.Json;
using System;

#endregion

namespace MPIS.User.AplicationService.DTOs.User
{
    public class UserResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("surname")]
        public string Surname { get; set; }
        [JsonProperty("office")]
        public string Office { get; set; }
        [JsonProperty("Address")]
        public string Address { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
