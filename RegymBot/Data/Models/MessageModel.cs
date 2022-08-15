using System;

namespace RegymBot.Data.Models
{
    public class MessageModel
    {
        public Guid MessageGuid { get; set; }
        public string PageName { get; set; }
        public string Message { get; set; }
    }
}
