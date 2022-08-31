using System;

namespace RegymBot.Data.Entities
{
    public class ClientEntity
    {
        public Guid ClientGuid { get; set; }
        public string Name { get; set; }
        public string Enrol { get; set; }
        public string Phone { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
