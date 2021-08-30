using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.HealthCheck;

namespace Product.Api.Controllers
{
    [Route("healthcheck")]
    public class HealthCheckController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HealthCheckController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _mediator.Send(new HealthCheckQuery());
            return NoContent();
        }


        [HttpGet("external")]
        public IActionResult External()
        {
            return NoContent();
        }
    }
}