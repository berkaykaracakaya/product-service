using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Product.Application.Interfaces;
using Product.Application.Product.Queries;

namespace Product.Application.Product.Commands
{
    public class ProductDeleteCommand : IRequest
    {
        public ProductDeleteCommand(long productId)
        {
            ProductId = productId;
        }

        public long ProductId { get; set; }

        public class Handler : IRequestHandler<ProductDeleteCommand>
        {
            private readonly IProductDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IProductDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
            {
                var product = await _mediator.Send(new ProductByIdQuery(request.ProductId), cancellationToken);
                product.Delete();
                _context.Products.Remove(product);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}