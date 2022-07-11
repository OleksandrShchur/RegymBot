using System;

namespace RegymBot.Data.Entities
{
    public class FeedbackEntity
    {
        public Guid FeedbackGuid { get; set; }
        public string Feedback { get; set; }
        public long UserId { get; set; }
    }
}
