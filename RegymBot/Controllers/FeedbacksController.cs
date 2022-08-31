using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegymBot.Data.Repositories;
using System.Threading.Tasks;

namespace RegymBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FeedbacksController : ControllerBase
    {
        private readonly FeedbackRepository _feedbackRepository;

        public FeedbacksController(FeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var feedbacks = await _feedbackRepository.LoadAllFeedbacksAsync();

            return Ok(feedbacks);
        }
    }
}
