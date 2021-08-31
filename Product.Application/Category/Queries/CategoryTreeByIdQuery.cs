using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Application.Category.Enums;
using Product.Application.Interfaces;
using Product.Domain.Category;

namespace Product.Application.Category.Queries
{
    public class CategoryTreeByIdQuery : IRequest<List<CategoryAggregate>>
    {
        public CategoryTreeByIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

        public class Handler : IRequestHandler<CategoryTreeByIdQuery, List<CategoryAggregate>>
        {
            private readonly IProductDbContext _context;

            public Handler(IProductDbContext context)
            {
                _context = context;
            }

            public async Task<List<CategoryAggregate>> Handle(CategoryTreeByIdQuery request,
                CancellationToken cancellationToken)
            {
                if (request.Id == 0) throw new Exception(CategoryApplicationException.CategoryNotFound);
                var categories = _context.Categories.Where(x =>
                    x.Id == request.Id || x.ParentId == request.Id || x.ParentId > 0 && x.Id == x.ParentId);
                return await categories.ToListAsync(cancellationToken);
            }
        }
    }
}