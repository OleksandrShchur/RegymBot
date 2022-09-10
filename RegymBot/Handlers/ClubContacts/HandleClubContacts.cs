﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RegymBot.Data;
using RegymBot.Data.Enums;
using RegymBot.Data.Repositories;
using RegymBot.Helpers.Buttons;
using RegymBot.Services;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RegymBot.Handlers.ClubContacts
{
    public class HandleClubContacts : BaseHandle<HandleClubContacts>
    {
        private readonly StaticMessageRepository _staticMessageRepository;
        private readonly AppDbContext _dbContext;
        private IStepService _stepService;

        public HandleClubContacts(ILogger<HandleClubContacts> logger,
            ITelegramBotClient botClient,
            StaticMessageRepository staticMessageRepository,
            AppDbContext dbContext,
            IStepService stepService
        )
                : base(logger, botClient)
        {
            _staticMessageRepository = staticMessageRepository;
            _dbContext = dbContext;
            _stepService = stepService;
        }

        public async Task BotOnClubContacts(Message message)
        {
            _logger.LogInformation("Received callback query in club list from: {CallQueryFromId}", message.From.Id);
            var lastStep = _stepService.GetLastStep(message.Chat.Id);
            string text;

            var adminContacts = await _dbContext.AdminsInfo.AsNoTracking().FirstOrDefaultAsync(i => i.AdminsInfoId == 1);

            switch (lastStep)
            {
                case BotPage.Club_Apollo:
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.Club_Apollo);

                    await _botClient.SendLocationAsync(chatId: message.Chat.Id,
                                                    latitude: 50.4501,
                                                    longitude: 30.5234);
                    await _botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                    text: text,
                                                    replyMarkup: ClubContactButtons.Keyboard(adminContacts is null ? string.Empty : adminContacts.AdminApolloLogin));

                    break;

                case BotPage.Club_Vavylon:
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.Club_Vavylon);

                    await _botClient.SendLocationAsync(chatId: message.Chat.Id,
                                                    latitude: 49.9935,
                                                    longitude: 36.2304);
                    await _botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                    text: text,
                                                    replyMarkup: ClubContactButtons.Keyboard(adminContacts is null ? string.Empty : adminContacts.AdminVavylonLogin));

                    break;

                case BotPage.Club_Pshkn:
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.Club_Pshkn);

                    await _botClient.SendLocationAsync(chatId: message.Chat.Id,
                                                    latitude: 49.8397,
                                                    longitude: 24.0297);
                    await _botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                    text: text,
                                                    replyMarkup: ClubContactButtons.Keyboard(adminContacts is null ? string.Empty : adminContacts.AdminPSHKNLogin));

                    break;
            }
        }
    }
}
