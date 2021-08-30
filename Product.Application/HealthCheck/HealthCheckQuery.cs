using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Application.Interfaces;
using Product.Domain.Product;
using Product.Domain.Product.ValueObjects;

namespace Product.Application.HealthCheck
{
    public class HealthCheckQuery : IRequest
    {
        public class Handler : IRequestHandler<HealthCheckQuery>
        {
            private readonly IProductDbContext _context;

            public Handler(IProductDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(HealthCheckQuery request, CancellationToken cancellationToken)
            {
                var check = await _context.Products.OrderBy(p => p.Id).FirstOrDefaultAsync(cancellationToken);
                return Unit.Value;
            }

            private async Task RunClientHealthCheck(Func<Task> healthCheckTask, int count)
            {
                for (var i = 0; i < count; i++) await healthCheckTask();
            }
        }
    }
}