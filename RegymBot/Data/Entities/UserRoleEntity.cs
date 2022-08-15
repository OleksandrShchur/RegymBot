using System;

namespace RegymBot.Data.Entities
{
    public class UserRoleEntity
    {
        public Guid UserGuid { get; set; }
        public UserEntity User { get; set; }
        public Guid RoleGuid { get; set; }
        public RoleEntity Role { get; set; }
    }
}
