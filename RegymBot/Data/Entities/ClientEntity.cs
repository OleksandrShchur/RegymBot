using System;
using RegymBot.Data.Enums;

namespace RegymBot.Data.Entities
{
    public class ClientEntity
    {
        public Guid ClientGuid { get; set; }
        public string Name { get; set; }
        public string Enrol { get; set; }
        public RegymClub SelectedClub { get; set; }
        public bool Proceed { get; set; }
        public string Phone { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Finished { get; set; }

        public override string ToString()
        {
            return $"Клієнт: {this.Name}  \nКомментар: {this.Enrol}  \nКлуб: {this.SelectedClub}  \nТелефон: {this.Phone}  \nСтворено: {this.DateCreated}  \n";
        }
    }
}
