using System;
using System.Collections.Generic;
using System.Data;

namespace DataWrapper.Samples.DataLayer
{
  public partial class ProductSPDataSetManager : ProductSPManager
  {
    #region GetAll Method
    public override List<Product> GetAll()
    {
      // Submit SQL to get all rows
      return GetRecordsUsingDataSet<Product>("SalesLT.Product_GetAll", CommandType.StoredProcedure);
    }
    #endregion

    #region GetAllWithOutputParameter Method
    public override List<Product> GetAllWithOutputParameter()
    {
      List<Product> ret;

      // Create SQL to call a stored procedure
      SQL = "SalesLT.Product_GetAllWithOutput";

      // Add Ouput Parameter
      var parameters = new List<IDbDataParameter> {
        // Add parameters for CommandObject
        CreateParameter("Test", "", false, DbType.String, 10, ParameterDirection.Output)
      };

      // Submit SQL to get all rows
      ret = GetRecordsUsingDataSet<Product>(SQL, CommandType.StoredProcedure, parameters.ToArray());

      // Get @Test Output Parameter
      string test = GetParameterValue<string>("Test", "");
      Console.WriteLine(test);

      return ret;
    }
    #endregion

    #region Get Method
    public override Product Get(int productId)
    {
      // Create SQL to call a stored procedure
      SQL = "SalesLT.Product_Get";

      // Create parameters for counting
      var parameters = new List<IDbDataParameter> {
        // Add parameters for CommandObject
        CreateParameter("ProductID", (object)productId, false)
      };

      // Submit SQL to get a single row
      return GetRecordUsingDataSet<Product>(SQL, CommandType.StoredProcedure, parameters.ToArray());
    }
    #endregion

    #region Search Method
    public override List<Product> Search(ProductSearch search)
    {
      // Create SQL to call a stored procedure
      SQL = "SalesLT.Product_Search";

      // Create parameters for searching
      var parameters = new List<IDbDataParameter> {
        // Add parameters for CommandObject
        CreateParameter("Name", (object)search.Name ?? DBNull.Value, true),
        CreateParameter("ProductNumber", (object)search.ProductNumber ?? DBNull.Value, true),
        CreateParameter("BeginningCost", (object)search.BeginningCost ?? DBNull.Value, true),
        CreateParameter("EndingCost", (object)search.EndingCost ?? DBNull.Value, true)
      };

      // Submit SQL to search for rows
      return GetRecordsUsingDataSet<Product>(SQL, CommandType.StoredProcedure, parameters.ToArray());
    }
    #endregion
  }
}
