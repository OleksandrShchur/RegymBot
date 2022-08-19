using System;

namespace RegymBot.Data.Entities
{
    public class PageEntity
    {
        public Guid PageGuid { get; set; }
        public int PageId { get; set; }
        public string Name { get; set; }
        public StaticMessageEntity Message { get; set; }
    }
}