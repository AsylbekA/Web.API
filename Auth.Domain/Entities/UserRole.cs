using Auth.Domain.Entities.BaseEntities;

namespace Auth.Domain.Entities
{
    public class UserRole : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
