using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Product.Api.Controllers
{
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}