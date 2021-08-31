using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Product.Application.Category.Queries;
using Product.Application.Interfaces;
using Product.Application.Product.Enums;
using Product.Domain.Product;
using Product.Domain.Product.ValueObjects;

namespace Product.Application.Product.Commands
{
    public class ProductCreateCommand : IRequest<ProductAggregate>
    {
        [Required]
        [StringLength(200, ErrorMessage = ProductApplicationException.ProductTitleMaximum200Character)]
        public string Title { get; set; }

        public string Description { get; set; }
        public double Quantity { get; set; }
        public long CategoryId { get; set; }

        public class Handler : IRequestHandler<ProductCreateCommand, ProductAggregate>
        {
            private readonly IProductDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IProductDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<ProductAggregate> Handle(ProductCreateCommand request,
                CancellationToken cancellationToken)
            {
                var categories = await _mediator.Send(new CategoryTreeByIdQuery(request.CategoryId), cancellationToken);
                var product = new ProductAggregate(request.Title, request.Description, request.Quantity,
                    categories.Select(x =>
                        new ProductCategory(x.Id, x.ParentId, x.Title, x.MinStockQuantity, x.MaxStockQuantity, x.Status)
                    ).ToList());
                await _context.Products.AddAsync(product, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return product;
            }
        }
    }
}