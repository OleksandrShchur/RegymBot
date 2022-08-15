using System;
using System.Collections.Generic;

namespace RegymBot.Data.Entities
{
    public class RoleEntity
    {
        public Guid RoleGuid { get; set; }
        public string Role { get; set; }
        public IEnumerable<UserRoleEntity> UserRoles { get; set; }
    }
}
