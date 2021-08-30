using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Product.Application.Interfaces;
using Product.Domain.Product;

namespace Product.Application.Product.Commands
{
    public class ProductCreateCommand : IRequest<ProductAggregate>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public long CategoryId { get; set; }
        
        public class Handler : IRequestHandler<ProductCreateCommand,ProductAggregate>
        {
            private readonly IProductDbContext _context;

            public Handler(IProductDbContext context)
            {
                _context = context;
            }
            public Task<ProductAggregate> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}