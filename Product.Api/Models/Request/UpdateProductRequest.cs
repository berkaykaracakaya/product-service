using System.Collections.Generic;
using Product.Application.Product.Commands;
using Product.Domain.Product.ValueObjects;

namespace Product.Api.Models.Request
{
    public class UpdateProductRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public List<ProductCategory> Categories { get; set; }

        public ProductUpdateCommand ToCommand(long productId)
        {
            return new ProductUpdateCommand(productId, Title, Description, Quantity, Categories);
        }
    }
}