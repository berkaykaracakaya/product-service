using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Product.Commands;
using Product.Domain.Product;

namespace Product.Api.Controllers
{
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductAggregate), 201)]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateCommand request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}