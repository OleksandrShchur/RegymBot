using System.ComponentModel.DataAnnotations;
using RegymBot.Data.Enums;
using System;
using System.Collections.Generic;

namespace RegymBot.Data.Entities
{
    public class AdminsRegistrationLinks
    {
        [KeyAttribute]
        public int AdminsRegistrationLinksId { get; set; }
        public string Apollo { get; set; }
        public string Vavylon { get; set; }
        public string Pshkn { get; set; }
    }
}
