using Microsoft.Extensions.Logging;
using RegymBot.Data.Repositories;
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
        private IStepService _stepService;

        public HandleTrainingSchedule(ILogger<HandleTrainingSchedule> logger,
            ITelegramBotClient botClient,
            StaticMessageRepository staticMessageRepository,
            IStepService stepService)
                : base(logger, botClient)
        {
            _staticMessageRepository = staticMessageRepository;
            _stepService = stepService;
        }

        public async Task BotOnTrainingSchedule(Message message)
        {
            _logger.LogInformation("Received message in training list from: {FromUser}", message.From.Id);
            var lastStep = _stepService.GetLastStep(message.Chat.Id);
            string text;


        }
    }
}
