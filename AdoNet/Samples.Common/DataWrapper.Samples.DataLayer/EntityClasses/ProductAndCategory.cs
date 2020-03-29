using System.Collections.Generic;

namespace DataWrapper.Samples.DataLayer
{
  public class ProductAndCategory
  {
    public ProductAndCategory()
    {
      Products = new List<Product>();
      Categories = new List<ProductCategory>();
    }

    public List<Product> Products { get; set; }
    public List<ProductCategory> Categories { get; set; }
  }
}
