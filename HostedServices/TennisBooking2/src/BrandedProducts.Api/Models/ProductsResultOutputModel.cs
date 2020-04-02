using System.Collections.Generic;

namespace BrandedProducts.Api.Models
{
    public class ProductsResultOutputModel
    {
        public int TotalProducts => Products.Count;

        public IReadOnlyCollection<ProductOutputModel> Products { get; set; }
    }
}