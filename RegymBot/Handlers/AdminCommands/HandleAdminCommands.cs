using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RegymBot.Data;
using RegymBot.Data.Enums;
using RegymBot.Helpers.Buttons;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RegymBot.Handlers.AdminCommands
{
    public class HandleAdminCommands : BaseHandle<HandleAdminCommands>
    {
        private readonly AppDbContext _dbContext;

        public HandleAdminCommands(
            ILogger<HandleAdminCommands> logger,
            ITelegramBotClient botClient,
            AppDbContext dbContext
        )
                : base(logger, botClient)
        {
            _dbContext = dbContext;
        }

        public async Task handleClubToken(Update update)
        {
            if (update.Type != UpdateType.Message || update.Message.Type != MessageType.Text)
                return;

            var message = update.Message;

            if (!message.Text.Contains("/start "))
                return;

            string clubToken = message.Text.Replace("/start ", "");
            if (string.IsNullOrEmpty(clubToken) ||
                (!clubToken.Equals("apollo_admin") && !clubToken.Equals("pshkn_admin") && !clubToken.Equals("vavylon_admin")))
                return;

            var adminContacts = await _dbContext.AdminsInfo.AsNoTracking().FirstOrDefaultAsync(i => i.AdminsInfoId == 1);
            RegymClub selectedClub = RegymClub.None;

            switch (clubToken)
            {
                case "apollo_admin":
                    adminContacts.AdminApolloTelegramId = message.Chat.Id;
                    selectedClub = RegymClub.Apollo;
                    break;
                case "vavylon_admin":
                    adminContacts.AdminVavylonTelegramId = message.Chat.Id;
                    selectedClub = RegymClub.Vavylon;
                    break;
                case "pshkn_admin":
                    adminContacts.AdminPSHKNTelegramId = message.Chat.Id;
                    selectedClub = RegymClub.PSHKN;
                    break;
            }

            _dbContext.AdminsInfo.Update(adminContacts);
            await _dbContext.SaveChangesAsync();

            await _botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: $"Ви підписались на сповіщення клубу {selectedClub.ToString()}"
            );
        }
    }
}
