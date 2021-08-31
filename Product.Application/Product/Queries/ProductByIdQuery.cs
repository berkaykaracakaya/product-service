using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Application.Interfaces;
using Product.Application.Product.Enums;
using Product.Domain.Product;

namespace Product.Application.Product.Queries
{
    public class ProductByIdQuery : IRequest<ProductAggregate>
    {
        public ProductByIdQuery(long productId)
        {
            ProductId = productId;
        }

        public long ProductId { get; set; }

        public class Handler : IRequestHandler<ProductByIdQuery, ProductAggregate>
        {
            private readonly IProductDbContext _context;

            public Handler(IProductDbContext context)
            {
                _context = context;
            }

            public async Task<ProductAggregate> Handle(ProductByIdQuery request, CancellationToken cancellationToken)
            {
                var product =
                    await _context.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
                if (product == null) throw new Exception(ProductApplicationException.ProductNotFound);

                return product;
            }
        }
    }
}