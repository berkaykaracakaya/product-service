using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Api.Models.Request;
using Product.Application.Product.Commands;
using Product.Application.Product.Queries;
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

        [HttpDelete("{productId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteProduct([FromQuery] long productId)
        {
            await _mediator.Send(new ProductDeleteCommand(productId));
            return NoContent();
        }

        [HttpPatch("{productId}")]
        [ProducesResponseType(typeof(ProductAggregate), 200)]
        public async Task<IActionResult> UpdateProduct([FromRoute] long productId,
            [FromBody] UpdateProductRequest request)
        {
            return Ok(await _mediator.Send(request.ToCommand(productId)));
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ProductAggregate>), 200)]
        public async Task<IActionResult> GetProducts([FromQuery] string keywords)
        {
            return Ok(await _mediator.Send(new ProductByValuesQuery(keywords)));
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(typeof(ProductAggregate), 200)]
        public async Task<IActionResult> GetProduct([FromRoute] long productId)
        {
            return Ok(await _mediator.Send(new ProductByIdQuery(productId)));
        }
    }
}