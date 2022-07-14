using RegymBot.Data.Enums;
using System;

namespace RegymBot.Data.Entities
{
    public class StaticMessageEntity
    {
        public Guid StaticMessageGuid { get; set; }
        public BotPage Page { get; set; }
        public string Message { get; set; }
    }
}
