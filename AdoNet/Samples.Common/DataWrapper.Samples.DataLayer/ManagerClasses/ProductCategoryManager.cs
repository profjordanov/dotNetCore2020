using DataWrapper.Samples.AppLayer;
using System.Collections.Generic;

namespace DataWrapper.Samples.DataLayer
{
  public partial class ProductCategoryManager : AppDataManagerBase
  {
    #region GetAll Method
    public List<ProductCategory> GetAll()
    {
      // Create SQL to select all rows
      SQL = "SELECT * FROM SalesLT.ProductCategory";

      // Submit SQL to get all rows
      return GetRecords<ProductCategory>(SQL);
    }
    #endregion
  }
}
