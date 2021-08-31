using System;
using System.Collections.Generic;
using System.Linq;
using Product.Domain.Common;
using Product.Domain.Product.Enums;
using Product.Domain.Product.Events;
using Product.Domain.Product.ValueObjects;

namespace Product.Domain.Product
{
    public class ProductAggregate : AggregateRoot
    {
        public ProductAggregate()
        {
        }

        public ProductAggregate(string title, string description, double quantity, List<ProductCategory> categories)
        {
            Title = title;
            Description = description;
            Categories = categories;
            Quantity = quantity;
            EnsureProductAlive();
            SetAsCreated();
            AddEvent(ProductEvent.Created, new ProductCreated(this));
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public List<ProductCategory> Categories { get; set; }

        public void Update(string title, string description, double quantity, List<ProductCategory> categories)
        {
            Title = title;
            Description = description;
            Quantity = quantity;
            Categories = categories;
            EnsureProductAlive();
            SetAsModified();
            AddEvent(ProductEvent.Updated, new ProductUpdated(this));
        }

        public void Delete()
        {
            SetAsModified();
            AddEvent(ProductEvent.Deleted, new ProductDeleted(this));
        }

        private void EnsureProductAlive()
        {
            if (Categories == null && Categories.Count == 0)
                throw new Exception(ProductExceptions.ProductHaveAMinimumOneCategory);

            var acceptorCategories =
                Categories.Where(x => x.MaxStockQuantity >= Quantity && x.MinStockQuantity <= Quantity);
            if (acceptorCategories.Count() != Categories.Count)
                throw new Exception(ProductExceptions.ProductCategoryNotQuantityValid);
        }
    }
}