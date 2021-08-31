using System;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Product.Application.Interfaces;
using Product.Domain.Category;
using Product.Domain.Common;
using Product.Domain.Product;
using Product.Infrastructure.Common.ValueObjects;

namespace Product.Infrastructure.Persistence
{
    public class ProductDbContext : DbContext, IProductDbContext
    {
        public ProductDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public DbSet<ProductAggregate> Products { get; set; }
        public DbSet<CategoryAggregate> Categories { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            await SendOutboxEvents(cancellationToken);
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
            ApplyAggregateProperties(modelBuilder);
        }

        private async Task SendOutboxEvents(CancellationToken cancellationToken)
        {
            var baseDbContext = this;
            var aggregateRootArray = baseDbContext.ChangeTracker.Entries<AggregateRoot>()
                .Select((Func<EntityEntry<AggregateRoot>, AggregateRoot>) (e => e.Entity))
                .Where((Func<AggregateRoot, bool>) (e => e.Events.Any())).ToArray();
            for (var index = 0; index < aggregateRootArray.Length; ++index)
            {
                var aggregate = aggregateRootArray[index];
                if (aggregate.Id == 0L)
                {
                    var aggregateRoot = aggregate;
                    aggregateRoot.Id = await baseDbContext.GetNextSequenceValueForType(aggregate.GetType());
                    aggregateRoot = null;
                }

                var list = aggregate.Events.ToList();
                aggregate.Events.Clear();
                foreach (var @event in list)
                {
                    @event.Payload.SetMetadata(aggregate.Id, aggregate.LastModifiedDate, aggregate.Version);
                    var entityEntry =
                        await baseDbContext.OutboxMessages.AddAsync(new OutboxMessage(@event), cancellationToken);
                }

                aggregate = null;
            }

            aggregateRootArray = null;
        }

        private async Task<long> GetNextSequenceValueForType(Type type)
        {
            var baseDbContext = this;
            var sequenceName = baseDbContext.GetSequenceNameFromType(type);
            var connection = baseDbContext.Database.GetDbConnection();
            await connection.OpenAsync();
            var cmd = connection.CreateCommand();
            object obj1 = null;
            var num1 = 0;
            object obj2;
            long num2 = 0;
            try
            {
                cmd.CommandText = "SELECT nextval('" + sequenceName + "')";
                obj2 = await cmd.ExecuteScalarAsync();
                await connection.CloseAsync();
                num2 = (long) obj2;
                num1 = 1;
            }
            catch (Exception ex)
            {
                obj1 = ex;
            }

            if (cmd != null)
                await cmd.DisposeAsync();
            var obj = obj1;
            if (obj != null)
            {
                if (!(obj is Exception source2))
                    throw (Exception) obj;
                ExceptionDispatchInfo.Capture(source2).Throw();
            }

            if (num1 == 1)
                return num2;
            obj1 = null;
            sequenceName = null;
            connection = null;
            cmd = null;
            obj2 = null;
            long num = 0;
            return num;
        }

        private void ApplyAggregateProperties(ModelBuilder modelBuilder)
        {
            foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes()
                .Where((Func<IMutableEntityType, bool>) (t => typeof(AggregateRoot).IsAssignableFrom(t.ClrType))))
            {
                var entityTypeBuilder = modelBuilder.Entity(mutableEntityType.ClrType);
                var sequenceNameFromType = GetSequenceNameFromType(mutableEntityType.ClrType);
                modelBuilder.HasSequence<long>(sequenceNameFromType).StartsAt(1L).IncrementsBy(1);
                entityTypeBuilder.Ignore("Events");
                entityTypeBuilder.Ignore("IsModified");
                entityTypeBuilder.Property("Id").HasColumnName("id").ValueGeneratedNever();
                entityTypeBuilder.Property("CreatedDate").HasColumnName("created_date");
                entityTypeBuilder.Property("LastModifiedDate").HasColumnName("last_modified_date");
                entityTypeBuilder.Property("Version").HasColumnName("xmin").HasColumnType("xid")
                    .ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
                entityTypeBuilder.HasKey("Id");
            }
        }

        private string GetSequenceNameFromType(Type type)
        {
            return (string.Join('_', Regex.Split(type.Name, "(?<!^)(?=[A-Z])")).Replace("Aggregate", "").ToLower() +
                    "_id_seq").Replace("__", "_");
        }
    }
}