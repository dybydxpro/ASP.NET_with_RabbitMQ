using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestMQ.API.Models;
using TestMQ.API.Services.Contect;

namespace TestMQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        protected readonly ILogger<BookingController> _logger;
        protected readonly IMessageProducer _messageProducer;

        public BookingController(ILogger<BookingController> logger, IMessageProducer messageProducer)
        {
            _logger = logger;
            _messageProducer = messageProducer;
        }

        [HttpPost]
        public async Task<ActionResult<Booking>> Post(Booking booking)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            
            await _messageProducer.SendingMessage<Booking>(booking);
            return Ok(booking);
        }
    }
}
