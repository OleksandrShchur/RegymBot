using System.ComponentModel.DataAnnotations;
using System;

namespace RegymBot.Data.Entities
{
    public class TGUserEntity
    {
        [KeyAttribute]
        public Guid TGUserEntityGuid { get; set; }
        public string TelegramLogin { get; set; }
        public string FullName { get; set; }
        public long UserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
