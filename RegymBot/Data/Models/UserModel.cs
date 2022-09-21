using RegymBot.Data.Enums;
using System;
using System.Collections.Generic;

namespace RegymBot.Data.Models
{
    public class UserModel
    {
        public Guid UserGuid { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public IEnumerable<RegymClub> Clubs { get; set; }
    }
}
