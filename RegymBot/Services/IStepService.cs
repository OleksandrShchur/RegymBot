using RegymBot.Helpers.StateContext;

namespace RegymBot.Services
{
    public interface IStepService
    {
        void ReturnBackStep(long userId);
        BotStep GetLastStep(long userId);
        void NewStep(BotStep step, long userId);
    }
}
