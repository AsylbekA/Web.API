using Auth.Domain.Entities.BaseEntities;

namespace Auth.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime IIN { get; set; }
        public UserRole UserRoles { get; set; }
    }
}
