using Common.Library.Exceptions;
using DataWrapper.Samples.AppLayer;
using System;
using System.Collections.Generic;
using System.Data;

namespace DataWrapper.Samples.DataLayer
{
  public partial class ProductManager : AppDataManagerBase
  {
    #region GetAll Method
    public List<Product> GetAll()
    {
      // Submit SQL to get rows from a table
      return GetRecords<Product>("SELECT * FROM SalesLT.Product");
    }
    #endregion

    #region Get Method
    public virtual Product Get(int productId)
    {
      // Create SQL to get a single row
      SQL = "SELECT * FROM SalesLT.Product WHERE ProductID = @ProductID";

      // Create parameters for counting
      var parameters = new List<IDbDataParameter> {
        // Add parameters for CommandObject
        CreateParameter("ProductID", (object)productId, false)
      };

      // Submit SQL to get a single row from a table
      return GetRecord<Product>(SQL, parameters.ToArray());
    }
    #endregion

    #region Count Methods
    public virtual int Count()
    {
      // Submit SQL to count all records in a table
      return CountRecords("SELECT Count(*) FROM SalesLT.Product");
    }

    public virtual int Count(ProductSearch search)
    {
      // Create SQL to count rows
      SQL = "SELECT Count(*) FROM SalesLT.Product";
      SQL += " WHERE (@Name          IS NULL OR NAME          LIKE @Name + '%')";
      SQL += " AND   (@ProductNumber IS NULL OR ProductNumber LIKE @ProductNumber + '%')";
      SQL += " AND   (@BeginningCost IS NULL OR StandardCost  >=   @BeginningCost)";
      SQL += " AND   (@EndingCost    IS NULL OR StandardCost  <=   @EndingCost)";

      // Create parameters for counting
      var parameters = new List<IDbDataParameter> {
        // Add parameters for CommandObject
        CreateParameter("Name", (object)search.Name ?? DBNull.Value, true),
        CreateParameter("ProductNumber", (object)search.ProductNumber ?? DBNull.Value, true),
        CreateParameter("BeginningCost", (object)search.BeginningCost ?? DBNull.Value, true),
        CreateParameter("EndingCost", (object)search.EndingCost ?? DBNull.Value, true)
      };

      // Submit SQL to count records
      return CountRecords(SQL, parameters.ToArray());
    }
    #endregion

    #region Search Method
    public virtual List<Product> Search(ProductSearch search)
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
      return GetRecords<Product>(SQL, parameters.ToArray());
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
      // Reset all properties
      Reset();

      // Attempt to validate the data, a ValidationException is thrown if validation rules fail
      Validate<Product>(entity);

      // Create SQL to INSERT a row using dynamic SQL
      SQL = "INSERT INTO SalesLT.Product(Name, ProductNumber, Color, StandardCost, ListPrice, Size, Weight, ProductCategoryID, ProductModelID, SellStartDate, SellEndDate, DiscontinuedDate, ModifiedDate) ";
      SQL += " VALUES(@Name, @ProductNumber, @Color, @StandardCost, @ListPrice, @Size, @Weight, @ProductCategoryID, @ProductModelID, @SellStartDate, @SellEndDate, @DiscontinuedDate, @ModifiedDate)";

      // Create standard insert parameters
      BuildInsertUpdateParameters(entity);

      // Execute Query and set the IDENTITY property
      RowsAffected = ExecuteNonQuery(true);

      // Get the primary key generated from the SQL Server IDENTITY
      entity.ProductID = GetIdentityValue<int>(-1);

      return RowsAffected;
    }
    #endregion

    #region Update Method
    public virtual int Update(Product entity)
    {
      // Reset all properties
      Reset();

      // Attempt to validate the data, a ValidationException is thrown if validation rules fail
      Validate<Product>(entity);

      // Create SQL to UPDATE a row using dynamic SQL
      SQL = "UPDATE SalesLT.Product SET Name=@Name, ProductNumber=@ProductNumber, Color=@Color, StandardCost=@StandardCost, ";
      SQL += " ListPrice=@ListPrice, Size=@Size, Weight=@Weight, ProductCategoryID=@ProductCategoryID, ProductModelID=@ProductModelID, ";
      SQL += " SellStartDate = @SellStartDate, SellEndDate = @SellEndDate, DiscontinuedDate = @DiscontinuedDate, ModifiedDate = @ModifiedDate ";
      SQL += " WHERE ProductID = @ProductID";

      // Create standard update parameters
      BuildInsertUpdateParameters(entity);

      // Add primary parameter to CommandObject
      AddParameter("@ProductId", (object)entity.ProductID, false);

      // Execute Query and set RowsAffected
      return ExecuteNonQuery();
    }
    #endregion

    #region Delete Method
    public virtual int Delete(Product entity)
    {
      // Reset all properties
      Reset();

      // Create SQL to DELETE a row using dynamic SQL
      SQL = "DELETE FROM SalesLT.Product WHERE ProductID = @ProductID";

      // Add primary parameter to CommandObject
      AddParameter("@ProductId", (object)entity.ProductID, false);

      // Execute Query and set RowsAffected
      return ExecuteNonQuery();
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