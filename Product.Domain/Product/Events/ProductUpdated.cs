using System.Collections.Generic;
using Product.Domain.Common.ValueObjects;
using Product.Domain.Product.ValueObjects;

namespace Product.Domain.Product.Events
{
    public class ProductUpdated : DomainEventPayload
    {
        public ProductUpdated(ProductAggregate product)
        {
            Id = product.Id;
            Title = product.Title;
            Description = product.Description;
            Quantity = product.Quantity;
            Categories = product.Categories;
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public List<ProductCategory> Categories { get; set; }
    }
}