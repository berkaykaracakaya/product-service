namespace Product.Domain.Product.ValueObjects
{
    public class ProductCategory
    {
        public ProductCategory()
        {
        }

        public ProductCategory(long ıd, long parentId, string title, double minStockQuantity, double maxStockQuantity,
            bool status)
        {
            Id = ıd;
            ParentId = parentId;
            Title = title;
            MinStockQuantity = minStockQuantity;
            MaxStockQuantity = maxStockQuantity;
            Status = status;
        }

        public long Id { get; set; }
        public long ParentId { get; set; }
        public string Title { get; set; }
        public double MinStockQuantity { get; set; }
        public double MaxStockQuantity { get; set; }
        public bool Status { get; set; }
    }
}