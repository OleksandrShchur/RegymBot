using System;

namespace RegymBot.Data.Entities
{
    public class FeedbackEntity
    {
        public Guid FeedbackGuid { get; set; }
        public string Feedback { get; set; }
        public string TelegramLogin { get; set; }
        public string FullName { get; set; }
        public long UserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
