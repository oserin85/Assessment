using Assessment.EventServices.Api.Producers;
using Assessment.Models.Service.EventServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.EventServices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private EventProducer _eventProducer { get; }

        public EventController(EventProducer eventProducer)
        {
            _eventProducer = eventProducer;
        }

        [HttpPost, Route("/")]
        public async Task<IActionResult> AddEvents(Events events)
        {
            try
            {
                await _eventProducer.SendMessages(events?.EventsList?.ToList());
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Your Req. not Prossed!");
            }
        }
    }
}
