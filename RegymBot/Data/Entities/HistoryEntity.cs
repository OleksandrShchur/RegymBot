using System;

namespace RegymBot.Data.Entities
{
    public class HistoryEntity
    {
        public Guid HistoryGuid { get; set; }
        public string Item { get; set; }
        public DateTime Date { get; set; }
    }
}
