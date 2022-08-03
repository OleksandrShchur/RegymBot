using RegymBot.Data.Enums;
using System.Collections.Generic;

namespace RegymBot.Helpers.StateContext
{
    public class UserStepsModel
    {
        public long UserId { get; set; }
        public List<BotPage> History { get; set; }
    }
}
