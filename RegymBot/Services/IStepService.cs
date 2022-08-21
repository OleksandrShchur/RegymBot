using RegymBot.Data.Enums;

namespace RegymBot.Services
{
    public interface IStepService
    {
        void ReturnBackStep(long userId);
        BotPage GetLastStep(long userId);
        void NewStep(BotPage step, long userId);
        BotPage ToStartPage(long userId);
        void SetOptions(long userId, object options);
        object GetOptions(long userId);
    }
}
