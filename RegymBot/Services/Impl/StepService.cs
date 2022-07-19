using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegymBot.Services.Impl
{
    public class StepService : IStepService
    {
        private readonly ILogger<IStepService> _logger;
        private List<string> Steps { get; set; }

        public StepService(ILogger<IStepService> logger) 
        {
            _logger = logger;
        } 

        public string GetLastStep()
        {
            try { 
                var len = Steps.Count;

                if (len > 0)
                {
                    return Steps[len - 1];
                }
            }
            catch(Exception e)
            {
                _logger.LogError($"An error in get last step, message: {e.Message}, stacktrace: {e.StackTrace}");
            }

            return "";
        }

        public void ReturnBackStep()
        {
            try
            {
                var len = Steps.Count;

                if (len > 0)
                {
                    Steps.RemoveAt(len - 1);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"An error in return back step, message: {e.Message}, stacktrace: {e.StackTrace}");
            }
        }

        public void WriteNewStep(string step)
        {
            try
            {
                if (step != "" || step != null)
                {
                    Steps.Add(step);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"An error in write new step, message: {e.Message}, stacktrace: {e.StackTrace}");
            }
        }
    }
}
