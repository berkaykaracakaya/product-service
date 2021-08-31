using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Product.Application.Interfaces;
using Product.Application.Product.Queries;
using Product.Domain.Product;
using Product.Domain.Product.ValueObjects;

namespace Product.Application.Product.Commands
{
    public class ProductUpdateCommand : IRequest<ProductAggregate>
    {
        public ProductUpdateCommand(long productId, string title, string description, double quantity,
            List<ProductCategory> categories)
        {
            ProductId = productId;
            Title = title;
            Description = description;
            Quantity = quantity;
            Categories = categories;
        }

        public long ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public List<ProductCategory> Categories { get; set; }

        public class Handler : IRequestHandler<ProductUpdateCommand, ProductAggregate>
        {
            private readonly IProductDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IMediator mediator, IProductDbContext context)
            {
                _mediator = mediator;
                _context = context;
            }

            public async Task<ProductAggregate> Handle(ProductUpdateCommand request,
                CancellationToken cancellationToken)
            {
                var product = await _mediator.Send(new ProductByIdQuery(request.ProductId));

                product.Update(request.Title, request.Description, request.Quantity, request.Categories);
                _context.Products.Update(product);
                await _context.SaveChangesAsync(cancellationToken);

                return product;
            }
        }
    }
}