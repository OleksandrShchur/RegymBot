using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RegymBot.Data.Entities
{
    public class ClubEntity
    {
        [Key]
        public int ClubId { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserClubEntity> UserClubs { get; set; }
    }
}
