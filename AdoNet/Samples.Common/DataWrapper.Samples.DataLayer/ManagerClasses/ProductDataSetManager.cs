using System;
using System.Collections.Generic;
using System.Data;

namespace DataWrapper.Samples.DataLayer
{
  public partial class ProductDataSetManager : ProductManager
  {
    #region GetAll Method
    public new List<Product> GetAll()
    {
      // Submit SQL to get all rows
      return GetRecordsUsingDataSet<Product>("SELECT * FROM SalesLT.Product");
    }
    #endregion

    #region Get Method
    public override Product Get(int productId)
    {
      // Create SQL to get a single row
      SQL = "SELECT * FROM SalesLT.Product WHERE ProductID = @ProductID";

      // Create parameters for counting
      var parameters = new List<IDbDataParameter> {
        // Add parameters for CommandObject
        CreateParameter("ProductID", (object)productId, false)
      };

      // Submit SQL to get a single row
      return GetRecordUsingDataSet<Product>(SQL, parameters.ToArray());
    }
    #endregion

    #region Search Method
    public override List<Product> Search(ProductSearch search)
    {
      // Create SQL to search for rows
      SQL = "SELECT [ProductID],[Name],[ProductNumber],[Color],[StandardCost],[ListPrice],[Size],[Weight],[ProductCategoryID],[ProductModelID],[SellStartDate],[SellEndDate],[DiscontinuedDate],[ModifiedDate] FROM SalesLT.Product";
      SQL += " WHERE (@Name          IS NULL OR NAME          LIKE @Name + '%')";
      SQL += " AND   (@ProductNumber IS NULL OR ProductNumber LIKE @ProductNumber + '%')";
      SQL += " AND   (@BeginningCost IS NULL OR StandardCost  >=   @BeginningCost)";
      SQL += " AND   (@EndingCost    IS NULL OR StandardCost  <=   @EndingCost)";

      // Create parameters for searching
      var parameters = new List<IDbDataParameter> {
        // Add parameters for CommandObject
        CreateParameter("Name", (object)search.Name ?? DBNull.Value, true),
        CreateParameter("ProductNumber", (object)search.ProductNumber ?? DBNull.Value, true),
        CreateParameter("BeginningCost", (object)search.BeginningCost ?? DBNull.Value, true),
        CreateParameter("EndingCost", (object)search.EndingCost ?? DBNull.Value, true)
      };

      // Submit SQL to search for rows
      return GetRecordsUsingDataSet<Product>(SQL, parameters.ToArray());
    }
    #endregion    

    #region MultipleResultSets Method
    public ProductAndCategory MultipleResultSets()
    {
      ProductAndCategory ret = new ProductAndCategory();

      // Reset all properties
      Reset();

      // Create SQL to select from multiple tables
      SQL = "SELECT * FROM SalesLT.Product;";
      SQL += "SELECT * FROM SalesLT.ProductCategory;";

      // Execute Query
      DataSet ds = GetDataSet();

      // Build products list
      ret.Products = ToList<Product>(ds.Tables[0]);

      // Build categories list
      ret.Categories = ToList<ProductCategory>(ds.Tables[1]);

      // Set RowsAffected
      RowsAffected = ret.Products.Count + ret.Categories.Count;

      return ret;
    }
    #endregion
  }
}
