using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Application.Category.Enums;
using Product.Application.Interfaces;
using Product.Domain.Category;

namespace Product.Application.Category.Queries
{
    public class CategoryByIdQuery : IRequest<CategoryAggregate>
    {
        public CategoryByIdQuery(long categoryId)
        {
            CategoryId = categoryId;
        }

        public long CategoryId { get; set; }

        public class Handler : IRequestHandler<CategoryByIdQuery, CategoryAggregate>
        {
            private readonly IProductDbContext _context;

            public Handler(IProductDbContext context)
            {
                _context = context;
            }

            public async Task<CategoryAggregate> Handle(CategoryByIdQuery request, CancellationToken cancellationToken)
            {
                var category =
                    await _context.Categories.FirstOrDefaultAsync(x => x.Id == request.CategoryId, cancellationToken);
                if (category == null) throw new Exception(CategoryApplicationException.CategoryNotFound);

                return category;
            }
        }
    }
}