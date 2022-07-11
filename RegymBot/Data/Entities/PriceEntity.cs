using System;

namespace RegymBot.Data.Entities
{
    public class PriceEntity
    {
        public Guid PriceGuid { get; set; }
        public string PriceName { get; set; }
        public decimal Price { get; set; }
    }
}
