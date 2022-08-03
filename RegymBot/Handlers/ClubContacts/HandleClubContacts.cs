using Microsoft.Extensions.Logging;
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
        private IStepService _stepService;

        public HandleClubContacts(ILogger<HandleClubContacts> logger,
            ITelegramBotClient botClient,
            StaticMessageRepository staticMessageRepository,
            IStepService stepService)
                : base(logger, botClient)
        {
            _staticMessageRepository = staticMessageRepository;
            _stepService = stepService;
        }

        public async Task BotOnClubContacts(Message message)
        {
            _logger.LogInformation("Received callback query in club list from: {CallQueryFromId}", message.From.Id);
            var lastStep = _stepService.GetLastStep(message.Chat.Id);
            string text;

            switch (lastStep)
            {
                case BotPage.Club_Apollo:
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.Club_Apollo);

                    await _botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                    text: text,
                                                    replyMarkup: ClubContactButtons.Keyboard);

                    break;

                case BotPage.Club_Vavylon:
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.Club_Vavylon);

                    await _botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                    text: text,
                                                    replyMarkup: ClubContactButtons.Keyboard);

                    break;

                case BotPage.Club_Pshkn:
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.Club_Pshkn);

                    await _botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                    text: text,
                                                    replyMarkup: ClubContactButtons.Keyboard);

                    break;
            }
        }
    }
}
