using System;

namespace RegymBot.Data.Entities
{
    public class StaticMessageEntity
    {
        public Guid StaticMessageGuid { get; set; }
        public int PageId { get; set; }
        public PageEntity Page { get; set; }
        public string Message { get; set; }
    }
}
