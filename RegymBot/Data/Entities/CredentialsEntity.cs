using System;

namespace RegymBot.Data.Entities
{
    public class CredentialsEntitiy
    {
        public Guid CredentialsGuid { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}