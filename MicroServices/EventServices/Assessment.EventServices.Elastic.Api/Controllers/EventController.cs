using Assessment.Models.Service.EventServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Assessment.EventServices.Elastic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private ILogger _logger { get; set; }

        public EventController(ILogger<EventController> logger)
        {
            _logger = logger;
        }

        [HttpPost, Route("/")]
        public async Task<IActionResult> AddEvents(Event events)
        {
            try
            {
                _logger.LogInformation(JsonConvert.SerializeObject(events));
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Your Req. not Prossed!");
}
        }
    }
}
