namespace RegymBot.Data.Entities
{
    public class PageEntity
    {
        public int PageId { get; set; }
        public string Name { get; set; }
        public StaticMessageEntity Message { get; set; }
    }
}
