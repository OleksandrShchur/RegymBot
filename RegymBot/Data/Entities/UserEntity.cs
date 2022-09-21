﻿using RegymBot.Data.Enums;
using System;
using System.Collections.Generic;

namespace RegymBot.Data.Entities
{
    public class UserEntity
    {
        public Guid UserGuid { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Description { get; set; }
        public Category Category { get; set; }
        public IEnumerable<UserRoleEntity> UserRoles { get; set; }
        public IEnumerable<UserClubEntity> UserClubs { get; set; }
    }
}
