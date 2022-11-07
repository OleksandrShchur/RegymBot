using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RegymBot.Data;
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
        private readonly AppDbContext _dbContext;
        private IStepService _stepService;

        public HandleTrainingSchedule(
            ILogger<HandleTrainingSchedule> logger,
            ITelegramBotClient botClient,
            StaticMessageRepository staticMessageRepository,
            IStepService stepService,
            ClientRepository clientRepository,
            AppDbContext dbContext
        ): base(logger, botClient)
        {
            _staticMessageRepository = staticMessageRepository;
            _stepService = stepService;
            _clientRepository = clientRepository;
            _dbContext = dbContext;
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
                    await _clientRepository.UpdateEnrollAsync(client);

                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.GetUserPhone);

                    await _botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                        text: text);

                    _stepService.NewStep(BotPage.GetUserPhone, message.Chat.Id);

                    break;

                case BotPage.GetUserPhone:
                    var clientGuidPhone = new Guid(_stepService.GetOptions(message.Chat.Id).ToString());
                    var clientWithPhone = await _clientRepository.GetByGuidAsync(clientGuidPhone);

                    clientWithPhone.Phone = message.Text; 
                    clientWithPhone.Finished = true;
                    clientWithPhone.DateCreated = DateTime.Now.ToUniversalTime();
                    
                    var selectedClub = _stepService.SelectedClub(message.Chat.Id);             
                    clientWithPhone.SelectedClub = selectedClub;
                    
                    await _clientRepository.UpdateEnrollAsync(clientWithPhone);

                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.FinishEnrolInGroup);
                    await _botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                        text: text,
                        replyMarkup: EnrolInGroupButtons.Keyboard);

                    _stepService.NewStep(BotPage.FinishEnrolInGroup, message.Chat.Id);    
                    var adminContacts = await _dbContext.AdminsInfo.AsNoTracking().FirstOrDefaultAsync(i => i.AdminsInfoId == 1);

                    long selectedAdminId = selectedClub switch
                    {
                        RegymClub.Apollo => adminContacts.AdminApolloTelegramId,
                        RegymClub.Vavylon => adminContacts.AdminVavylonTelegramId,
                        RegymClub.PSHKN => adminContacts.AdminPSHKNTelegramId,
                        _ => 0
                    };

                    if (selectedAdminId != 0) 
                    {
                        await _botClient.SendTextMessageAsync(
                            chatId: selectedAdminId,
                            text: clientWithPhone.ToString());
                    }

                    break;
            }

        }
    }
}
