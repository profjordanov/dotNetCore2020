using Common.Library.Exceptions;
using DataWrapper.Samples.AppLayer;
using System;
using System.Collections.Generic;
using System.Data;

namespace DataWrapper.Samples.DataLayer
{
  public partial class ProductSPManager : AppDataManagerBase
  {
    #region GetAll Method
    public virtual List<Product> GetAll()
    {
      // Submit SQL to get all rows
      return GetRecords<Product>("SalesLT.Product_GetAll", CommandType.StoredProcedure);
    }
    #endregion

    #region GetAllWithOutputParameter Method
    public virtual List<Product> GetAllWithOutputParameter()
    {
      List<Product> ret;

      // Create SQL to call a stored procedure
      SQL = "SalesLT.Product_GetAllWithOutput";

      // Add Ouput Parameter
      var parameters = new List<IDbDataParameter> {
        // Add parameters for CommandObject
        CreateParameter("@Test", "", false, DbType.String, 10, ParameterDirection.Output)
      };

      // Submit SQL to get all rows
      ret = GetRecords<Product>(SQL, CommandType.StoredProcedure, parameters.ToArray());

      // Get @Test Output Parameter
      string test = GetParameterValue<string>("@Test", "");
      Console.WriteLine(test);

      return ret;
    }
    #endregion

    #region Get Method
    public virtual Product Get(int productId)
    {
      // Create SQL to call a stored procedure
      SQL = "SalesLT.Product_Get";

      // Create parameters for counting
      var parameters = new List<IDbDataParameter> {
        // Add parameters for CommandObject
        CreateParameter("ProductID", (object)productId, false)
      };

      // Submit SQL to get a single row
      return GetRecord<Product>(SQL, CommandType.StoredProcedure, parameters.ToArray());
    }
    #endregion

    #region Count Methods
    public virtual int Count()
    {
      // Submit SQL to count all rows
      return CountRecords("SalesLT.Product_Count", CommandType.StoredProcedure);
    }

    public virtual int Count(ProductSearch search)
    {
      // Create SQL to call a stored procedure
      SQL = "SalesLT.Product_Count";

      // Create parameters for counting
      var parameters = new List<IDbDataParameter> {
        // Add parameters for CommandObject
        CreateParameter("Name", (object)search.Name ?? DBNull.Value, true),
        CreateParameter("ProductNumber", (object)search.ProductNumber ?? DBNull.Value, true),
        CreateParameter("BeginningCost", (object)search.BeginningCost ?? DBNull.Value, true),
        CreateParameter("EndingCost", (object)search.EndingCost ?? DBNull.Value, true)
      };

      // Submit SQL to count rows
      return CountRecords(SQL, CommandType.StoredProcedure, parameters.ToArray());
    }
    #endregion

    #region Search Method
    public virtual List<Product> Search(ProductSearch search)
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
      return GetRecords<Product>(SQL, CommandType.StoredProcedure, parameters.ToArray());
    }
    #endregion

    #region Validate Method
    public override bool Validate<T>(T entityToValidate)
    {
      // Check all data annotations first
      bool ret = base.Validate(entityToValidate);

      // Cast to concrete entity class
      Product entity = entityToValidate as Product;

      // TODO: Add other business rules here
      if (entity.Name.Length < 2) {
        AddValidationMessage("Name", "Name must be greater than 2 characters in length.");
      }

      if (ValidationMessages.Count > 0) {
        throw new ValidationException(ValidationMessages);
      }

      return ret;
    }
    #endregion

    #region Insert Method
    public virtual int Insert(Product entity)
    {
      // Reset all properties for calling a stored procedure
      Reset(CommandType.StoredProcedure);

      // Attempt to validate the data, a ValidationException is thrown if validation rules fail
      Validate<Product>(entity);

      // Create SQL to call a stored procedure
      SQL = "SalesLT.Product_Insert";

      // Create standard insert parameters
      BuildInsertUpdateParameters(entity);

      // Create parameter to get IDENTITY value generated
      AddParameter("@ProductID", -1, true, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);

      // Add any standard parameters
      AddStandardParameters();

      // Execute Query
      RowsAffected = ExecuteNonQuery(false, "@ProductID");

      // Get the primary key generated from the SQL Server IDENTITY
      entity.ProductID = GetParameterValue<int>("@ProductID", -1);

      // Get standard output parameters
      GetStandardOutputParameters();

      return RowsAffected;
    }
    #endregion

    #region Update Method
    public virtual int Update(Product entity)
    {
      // Reset all properties for calling a stored procedure
      Reset(CommandType.StoredProcedure);

      // Attempt to validate the data, a ValidationException is thrown if validation rules fail
      Validate<Product>(entity);

      // Create SQL to call a stored procedure
      SQL = "SalesLT.Product_Update";

      // Create standard update parameters
      BuildInsertUpdateParameters(entity);

      // Primary Key parameter
      AddParameter("@ProductId", (object)entity.ProductID, false);

      // Add any standard parameters
      AddStandardParameters();

      // Execute Query
      RowsAffected = ExecuteNonQuery();

      // Get standard output parameters
      GetStandardOutputParameters();

      return RowsAffected;
    }
    #endregion

    #region Delete Method
    public virtual int Delete(Product entity)
    {
      // Reset all properties for calling a stored procedure
      Reset(CommandType.StoredProcedure);

      // Create SQL to call a stored procedure
      SQL = "SalesLT.Product_Delete";

      // Create parameters
      AddParameter("@ProductId", (object)entity.ProductID, false);

      // Add any standard parameters
      AddStandardParameters();

      // Execute Query
      RowsAffected = ExecuteNonQuery();

      // Get standard output parameters
      GetStandardOutputParameters();

      return RowsAffected;
    }
    #endregion

    #region BuildInsertUpdateParameters Method
    protected virtual void BuildInsertUpdateParameters(Product entity)
    {
      // Add parameters to CommandObject
      AddParameter("Name", (object)entity.Name, false);
      AddParameter("ProductNumber", (object)entity.ProductNumber, false);
      AddParameter("Color", (object)entity.Color, false);
      AddParameter("StandardCost", (object)entity.StandardCost, false);
      AddParameter("ListPrice", (object)entity.ListPrice, false);
      AddParameter("Size", (object)entity.Size ?? DBNull.Value, true);
      AddParameter("Weight", (object)entity.Weight ?? DBNull.Value, true);
      AddParameter("ProductCategoryID", (object)entity.ProductCategoryID, false);
      AddParameter("ProductModelID", (object)entity.ProductModelID, false);
      AddParameter("SellStartDate", (object)entity.SellStartDate, false);
      AddParameter("SellEndDate", (object)entity.SellEndDate ?? DBNull.Value, true);
      AddParameter("DiscontinuedDate", (object)entity.DiscontinuedDate ?? DBNull.Value, true);
      AddParameter("ModifiedDate", (object)entity.ModifiedDate, false);
    }
    #endregion
  }
}
