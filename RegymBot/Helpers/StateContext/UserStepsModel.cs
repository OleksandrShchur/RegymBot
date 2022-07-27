using System.Collections.Generic;

namespace RegymBot.Helpers.StateContext
{
    public class UserStepsModel
    {
        public long UserId { get; set; }
        public List<BotStep> History { get; set; }
    }
}
