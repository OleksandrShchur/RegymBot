using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegymBot.Data.Entities
{
    public class UserClubEntity
    {
        [ForeignKey("User")]
        public Guid UserRef { get; set; }
        public UserEntity User { get; set; }

        [ForeignKey("Club")]
        public int ClubRef { get; set; }
        public ClubEntity Club { get; set; }
    }
}
