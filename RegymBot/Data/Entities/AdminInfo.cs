using System.ComponentModel.DataAnnotations;

namespace RegymBot.Data.Entities
{
    public class AdminsInfo
    {
        [KeyAttribute]
        public int AdminsInfoId { get; set; }
        public string AdminApolloLogin { get; set; }
        public string AdminVavylonLogin { get; set; }
        public string AdminPSHKNLogin { get; set; }
        public long AdminApolloTelegramId { get; set; }
        public long AdminVavylonTelegramId { get; set; }
        public long AdminPSHKNTelegramId { get; set; }
    }
}
