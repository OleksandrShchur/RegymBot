using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using RegymBot.Data.Enums;
using RegymBot.Data.Repositories;
using RegymBot.Handlers.ClubList;
using RegymBot.Helpers.Buttons;
using RegymBot.Services;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;

namespace RegymBot.Handlers.ClubContacts
{
    public class CallbackQueryClubContacts : BaseCallback<CallbackQueryClubContacts>
    {
        private readonly StaticMessageRepository _staticMessageRepository;
        private readonly HandleClubList _handleClubList;
        private readonly IWebHostEnvironment _appEnvironment;

        public CallbackQueryClubContacts(
            ITelegramBotClient botClient,
            ILogger<CallbackQueryClubContacts> logger,
            StaticMessageRepository staticMessageRepository,
            HandleClubList handleClubList,
            IStepService stepService,
            IWebHostEnvironment appEnvironment) : base(stepService, botClient, logger)
        {
            _staticMessageRepository = staticMessageRepository;
            _handleClubList = handleClubList;
            _appEnvironment = appEnvironment;
        }

        public async Task BotOnCallbackQueryReceived(Telegram.Bot.Types.CallbackQuery callbackQuery)
        {
            _logger.LogInformation("Received callback query in club contacts from: {CallQueryFromId}", callbackQuery.From.Id);
            string text;

            switch (callbackQuery.Data)
            {
                case "coach":
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.Category);
                    _stepService.NewStep(BotPage.Category, callbackQuery.From.Id);

                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    text: text,
                                                    replyMarkup: CategoryButtons.Keyboard);

                    break;

                case "training_schedule":
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.TrainingSchedule);
                    _stepService.NewStep(BotPage.TrainingSchedule, callbackQuery.From.Id);

                    using (var stream = File.Open(_appEnvironment.WebRootPath + "\\training_schedule.jpg", FileMode.Open)) 
                    {
                        await _botClient.SendPhotoAsync(chatId: callbackQuery.Message.Chat.Id,
                            photo: stream, caption: text, replyMarkup: ReturnBackButton.Keyboard);
                    }

                    break;

                case "back":
                    _stepService.ReturnBackStep(callbackQuery.From.Id);
                    await _handleClubList.BotOnClubList(callbackQuery.Message);

                    break;
            }
        }
    }
}
