using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Application.Interfaces;
using Product.Domain.Product;

namespace Product.Application.Product.Queries
{
    public class ProductByValuesQuery : IRequest<List<ProductAggregate>>
    {
        public ProductByValuesQuery(string keyword)
        {
            Keyword = keyword;
        }

        public string Keyword { get; set; }

        public class Hander : IRequestHandler<ProductByValuesQuery, List<ProductAggregate>>
        {
            private readonly IProductDbContext _context;

            public Hander(IProductDbContext context)
            {
                _context = context;
            }

            public async Task<List<ProductAggregate>> Handle(ProductByValuesQuery request,
                CancellationToken cancellationToken)
            {
                var products = _context.Products.AsNoTracking().Where(x =>
                    x.Title.Contains(request.Keyword) || x.Description.Contains(request.Keyword));

                return await products.ToListAsync(cancellationToken);
            }
        }
    }
}