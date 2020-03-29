using DataWrapper.Samples.AppLayer;
using System.Data;

namespace DataWrapper.Samples.DataLayer
{
  public partial class ProductAndCategoryManager : AppDataManagerBase
  {
    #region MultipleResultSets Method
    public virtual ProductAndCategory MultipleResultSets()
    {
      ProductAndCategory ret = new ProductAndCategory();

      // Reset all properties
      Reset();

      // Create SQL to select from multiple tables
      SQL = "SELECT * FROM SalesLT.Product;";
      SQL += "SELECT * FROM SalesLT.ProductCategory;";

      // Execute Query
      using (IDataReader dr = GetDataReader()) {
        // Use reflection to load Product data
        ret.Products = ToList<Product>(dr);
        RowsAffected = ret.Products.Count;

        // Move to next result set
        dr.NextResult();

        // Use reflection to load ProductCategory data
        ret.Categories = ToList<ProductCategory>(dr);
        RowsAffected += ret.Categories.Count;
      }

      return ret;
    }
    #endregion
  }
}