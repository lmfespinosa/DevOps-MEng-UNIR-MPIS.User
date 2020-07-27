#region "Libraries"

using MPIS.Package.Base;

#endregion

namespace MPIS.User.DomainModel
{
    public class User : BaseDomainModel
    {
        public string Name  { get; set; }
        public string Surname { get; set; }
        public string Office { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
