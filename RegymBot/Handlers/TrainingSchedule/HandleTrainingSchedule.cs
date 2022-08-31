using Microsoft.Extensions.Logging;
using RegymBot.Data.Entities;
using RegymBot.Data.Enums;
using RegymBot.Data.Repositories;
using RegymBot.Helpers.Buttons;
using RegymBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RegymBot.Handlers.TrainingSchedule
{
    public class HandleTrainingSchedule : BaseHandle<HandleTrainingSchedule>
    {
        private readonly StaticMessageRepository _staticMessageRepository;
        private readonly ClientRepository _clientRepository;
        private IStepService _stepService;

        public HandleTrainingSchedule(ILogger<HandleTrainingSchedule> logger,
            ITelegramBotClient botClient,
            StaticMessageRepository staticMessageRepository,
            IStepService stepService,
            ClientRepository clientRepository)
                : base(logger, botClient)
        {
            _staticMessageRepository = staticMessageRepository;
            _stepService = stepService;
            _clientRepository = clientRepository;
        }

        public async Task BotOnTrainingSchedule(Message message)
        {
            _logger.LogInformation("Received message in training list from: {FromUser}", message.From.Id);
            var lastStep = _stepService.GetLastStep(message.Chat.Id);
            string text;

            switch(lastStep)
            {
                case BotPage.TrainingSchedule:
                    var newClient = new ClientEntity();
                    newClient.Enrol = message.Text;
                    newClient.DateCreated = DateTime.Now;
                    var addedClient = await _clientRepository.AddNewClientAsync(newClient);

                    _stepService.SetOptions(message.Chat.Id, addedClient.ClientGuid);
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.GetUserName);

                    await _botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                        text: text);

                    _stepService.NewStep(BotPage.GetUserName, message.Chat.Id);

                    break;

                case BotPage.GetUserName:
                    var clientGuid = new Guid(_stepService.GetOptions(message.Chat.Id).ToString());
                    var client = await _clientRepository.GetByGuidAsync(clientGuid);

                    client.Name = message.Text;
                    client.DateCreated = DateTime.Now;
                    await _clientRepository.UpdateClientInfoAsync(client);

                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.GetUserPhone);

                    await _botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                        text: text);

                    _stepService.NewStep(BotPage.GetUserPhone, message.Chat.Id);

                    break;

                case BotPage.GetUserPhone:
                    var clientGuidPhone = new Guid(_stepService.GetOptions(message.Chat.Id).ToString());
                    var clientWithPhone = await _clientRepository.GetByGuidAsync(clientGuidPhone);

                    clientWithPhone.Phone = message.Text;
                    clientWithPhone.DateCreated = DateTime.Now;
                    await _clientRepository.UpdateClientInfoAsync(clientWithPhone);

                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.FinishEnrolInGroup);

                    await _botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                        text: text,
                        replyMarkup: EnrolInGroupButtons.Keyboard);

                    break;
            }

        }
    }
}
