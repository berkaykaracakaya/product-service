using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Api.Configuration;
using Product.Application.Interfaces;
using Product.Infrastructure.Persistence;

namespace Product.Api.Extensions
{
    public static class ServiceCollectionDatabaseExtensions
    {
        public static IServiceCollection AddDatabases(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IProductDbContext, ProductDbContext>(p =>
                p.UseNpgsql(
                    configuration.GetValue<string>(ConfigKeys.DatabaseConnection),
                    options => { options.MigrationsAssembly(typeof(ProductDbContext).Assembly.FullName); }));

            return services;
        }
    }
}