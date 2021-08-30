using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Category;
using Product.Domain.Product;

namespace Product.Application.Interfaces
{
    public interface IProductDbContext
    {
        DbSet<ProductAggregate> Products { get; set; }
        DbSet<CategoryAggregate> Categories { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}