using RegymBot.Helpers;

namespace RegymBot.Services
{
    public interface IStepService
    {
        void ReturnBackStep();
        BotStep GetLastStep();
        void NewStep(BotStep step);
    }
}
