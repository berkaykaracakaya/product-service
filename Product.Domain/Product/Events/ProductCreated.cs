using System.Collections.Generic;
using Product.Domain.Common.ValueObjects;
using Product.Domain.Product.ValueObjects;

namespace Product.Domain.Product.Events
{
    public class ProductCreated : DomainEventPayload
    {
        public ProductCreated(ProductAggregate product)
        {
            Title = product.Title;
            Description = product.Description;
            Quantity = product.Quantity;
            Categories = product.Categories;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public List<ProductCategory> Categories { get; set; }
    }
}