using System;
using RegymBot.Data.Enums;

namespace RegymBot.Data.Models
{
    public class ClientModel
    {
        public Guid ClientGuid { get; set; }
        public string Name { get; set; }
        public string Enrol { get; set; }
        public RegymClub SelectedClub { get; set; }
        public bool Proceed { get; set; }
        public string Phone { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
