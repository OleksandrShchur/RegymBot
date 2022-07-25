using Microsoft.Extensions.Logging;
using RegymBot.Helpers;
using System;
using System.Collections.Generic;

namespace RegymBot.Services.Impl
{
    public class StepService : IStepService
    {
        private readonly ILogger<IStepService> _logger;
        private List<BotStep> Steps = new List<BotStep> { BotStep.MainMenu };

        public StepService(ILogger<IStepService> logger) 
        {
            _logger = logger;
        }

        public BotStep GetLastStep()
        {
            try
            {
                var len = Steps.Count;

                return Steps[len - 1];
            }
            catch (Exception e)
            {
                _logger.LogError($"An error in get last step, message: {e.Message}, stacktrace: {e.StackTrace}");
            }

            return BotStep.MainMenu;
        }

        public void NewStep(BotStep step)
        {
            try
            {
                Steps.Add(step);
            }
            catch (Exception e)
            {
                _logger.LogError($"An error in write new step, message: {e.Message}, stacktrace: {e.StackTrace}");
            }
        }

        public void ReturnBackStep()
        {
            try
            {
                var len = Steps.Count;

                if (len > 1)
                {
                    Steps.RemoveAt(len - 1);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"An error in return back step, message: {e.Message}, stacktrace: {e.StackTrace}");
            }
        }
    }
}
