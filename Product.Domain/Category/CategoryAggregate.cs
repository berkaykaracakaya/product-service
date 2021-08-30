using Product.Domain.Category.Enums;
using Product.Domain.Category.Events;
using Product.Domain.Common;

namespace Product.Domain.Category
{
    public class CategoryAggregate : AggregateRoot
    {
        public CategoryAggregate()
        {
            
        }

        public CategoryAggregate(long parentId, string title, double minStockQuantity, double maxStockQuantity, bool status)
        {
            ParentId = parentId;
            Title = title;
            MinStockQuantity = minStockQuantity;
            MaxStockQuantity = maxStockQuantity;
            Status = status;
            SetAsCreated();
            AddEvent(CategoryEvent.Created,new CategoryCreated(this));
        }
        
        public long ParentId { get; set; }
        public string Title { get; set; }
        public double MinStockQuantity { get; set; }
        public double MaxStockQuantity { get; set; }
        public bool Status { get; set; }
    }
}