namespace RegymBot.Services
{
    public interface IStepService
    {
        void WriteNewStep(string step);
        void ReturnBackStep();
        string GetLastStep();
    }
}
