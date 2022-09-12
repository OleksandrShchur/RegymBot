using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RegymBot.Data;
using RegymBot.Data.Entities;
using RegymBot.Data.Enums;
using RegymBot.Helpers.Buttons;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RegymBot.Handlers.StartCommand
{
    public class HandleStartCommand : BaseHandle<HandleStartCommand>
    {
        private readonly AppDbContext _dbContext;

        public HandleStartCommand(
            ILogger<HandleStartCommand> logger,
            ITelegramBotClient botClient,
            AppDbContext dbContext
        )
                : base(logger, botClient)
        {
            _dbContext = dbContext;
        }

        public async Task handleStart(Update update)
        {
            if (update.Type != UpdateType.Message || update.Message.Type != MessageType.Text)
                return;

            var message = update.Message;

            if (!message.Text.Equals("/start"))
                return;

            var tgUser = await _dbContext.TGUsers.AsNoTracking().FirstOrDefaultAsync(i => i.UserId == message.Chat.Id);

            if (tgUser != null)
                return;

            tgUser = new TGUserEntity
            {
                DateCreated = DateTime.Now,
                FullName = $"{message.Chat.FirstName} {message.Chat.LastName}",
                TelegramLogin = message.Chat.Username,
                UserId = message.Chat.Id,
            };

            _dbContext.TGUsers.Add(tgUser);
            await _dbContext.SaveChangesAsync();
        }
    }
}
