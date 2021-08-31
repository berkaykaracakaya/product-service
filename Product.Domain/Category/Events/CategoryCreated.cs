using Product.Domain.Common.ValueObjects;

namespace Product.Domain.Category.Events
{
    public class CategoryCreated : DomainEventPayload
    {
        public CategoryCreated(CategoryAggregate category)
        {
            ParentId = category.ParentId;
            Title = category.Title;
            MinStockQuantity = category.MinStockQuantity;
            MaxStockQuantity = category.MaxStockQuantity;
            Status = category.Status;
        }

        public long ParentId { get; set; }
        public string Title { get; set; }
        public double MinStockQuantity { get; set; }
        public double MaxStockQuantity { get; set; }
        public bool Status { get; set; }
    }
}