using Microsoft.Extensions.Logging;
using RegymBot.Helpers.StateContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegymBot.Services.Impl
{
    public class StepService : IStepService
    {
        private readonly ILogger<StepService> _logger;
        private List<UserStepsModel> State = new List<UserStepsModel>();

        public StepService(ILogger<StepService> logger) 
        {
            _logger = logger;
        }

        public BotStep GetLastStep(long userId)
        {
            try
            {
                if (!UserExists(userId))
                {
                    NewUser(userId);
                }

                return State.Where(s => s.UserId == userId).FirstOrDefault().History.LastOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"An error in get last step, message: {e.Message}, stacktrace: {e.StackTrace}");
            }

            return BotStep.MainMenu;
        }

        public void NewStep(BotStep step, long userId)
        {
            try
            {
                State.Where(s => s.UserId == userId).FirstOrDefault().History.Add(step);
            }
            catch (Exception e)
            {
                _logger.LogError($"An error in write new step, message: {e.Message}, stacktrace: {e.StackTrace}");
            }
        }

        public void ReturnBackStep(long userId)
        {
            try
            {
                var len = State.Where(s => s.UserId == userId).FirstOrDefault().History.Count;

                if (len > 1)
                {
                    State.Where(s => s.UserId == userId).FirstOrDefault().History.RemoveAt(len - 1);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"An error in return back step, message: {e.Message}, stacktrace: {e.StackTrace}");
            }
        }

        private bool UserExists(long userId)
        {
            return State.Any(s => s.UserId == userId);
        }

        private void NewUser(long userId)
        {
            var newUser = new UserStepsModel
            {
                UserId = userId,
                History = new List<BotStep> { BotStep.MainMenu }
            };

            State.Add(newUser);
        }
    }
}
