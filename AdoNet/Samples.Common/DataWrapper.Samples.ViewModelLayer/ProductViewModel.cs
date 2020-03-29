using Common.Library.Exceptions;
using DataWrapper.Samples.DataLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace DataWrapper.Samples.ViewModelLayer
{
  public class ProductViewModel : ProductViewModelBase
  {
    #region Properties
    private ProductAndCategory _ProductsAndCategories = new ProductAndCategory();

    public ProductAndCategory ProductsAndCategories
    {
      get { return _ProductsAndCategories; }
      set {
        _ProductsAndCategories = value;
        RaisePropertyChanged("ProductsAndCategories");
      }
    }
    #endregion

    #region HandleRequest Method
    public void HandleRequest()
    {
      switch (EventAction.ToLower()) {
        case "getall":
          GetAll();
          break;
        case "get":
          Get(Convert.ToInt32(EventValue));
          break;
        case "search":
          Search();
          break;
        case "count":
          Count();
          break;
        case "countsearch":
          CountUsingSearch();
          break;
        case "resetsearch":
          Init();
          break;
        case "insert":
          Insert();
          break;
        case "update":
          Update();
          break;
        case "delete":
          Delete();
          break;
        case "multipleresults":
          MultipleResultSets();
          break;
        case "trans":
          PerformTransaction();
          break;
      }
    }
    #endregion

    #region GetAll Method
    public void GetAll()
    {
      using (ProductManager mgr = new ProductManager()) {
        mgr.ConnectStringName = "Sandbox";
        try {
          DataCollection = mgr.GetAll();
          RowsAffected = mgr.RowsAffected;
          ResultText = "Rows Affected: " + RowsAffected.ToString();
        }
        catch (Exception ex) {
          PublishException(ex);
        }
      }
    }
    #endregion

    #region Get Method
    public Product Get(int productId)
    {
      DataCollection = new List<Product>();
      using (ProductManager mgr = new ProductManager()) {
        try {
          Entity = mgr.Get(productId);
          RowsAffected = mgr.RowsAffected;
          ResultText = "Rows Affected: " + RowsAffected.ToString();

          DataCollection.Add(Entity);
        }
        catch (Exception ex) {
          PublishException(ex);
        }
      }

      return Entity;
    }
    #endregion

    #region Search Method
    public void Search()
    {
      using (ProductManager mgr = new ProductManager()) {
        try {
          DataCollection = mgr.Search(SearchEntity);
          RowsAffected = mgr.RowsAffected;
          ResultText = "Rows Affected: " + RowsAffected.ToString();
        }
        catch (Exception ex) {
          PublishException(ex);
        }
      }
    }
    #endregion

    #region Count Method
    public int Count()
    {
      using (ProductManager mgr = new ProductManager()) {
        try {
          DataCollection = new List<Product>();
          RowsAffected = mgr.Count();

          ResultText = "Total Rows Counted: " + RowsAffected.ToString();
        }
        catch (Exception ex) {
          PublishException(ex);
        }
      }

      return RowsAffected;
    }
    #endregion

    #region CountUsingSearch Method
    public int CountUsingSearch()
    {
      using (ProductManager mgr = new ProductManager()) {
        try {
          DataCollection = new List<Product>();
          RowsAffected = mgr.Count(SearchEntity);

          ResultText = "Total Rows Counted: " + RowsAffected.ToString();
        }
        catch (Exception ex) {
          PublishException(ex);
        }
      }

      return RowsAffected;
    }
    #endregion

    #region MultipleResultSets Method
    public void MultipleResultSets()
    {
      using (ProductAndCategoryManager mgr = new ProductAndCategoryManager()) {
        try {
          ProductsAndCategories = mgr.MultipleResultSets();
          DataCollection = ProductsAndCategories.Products;

          ResultText = "Multiple result sets read";

          RowsAffected = mgr.RowsAffected;
        }
        catch (Exception ex) {
          PublishException(ex);
        }
      }
    }
    #endregion

    #region Insert Method
    public void Insert()
    {
      using (ProductManager mgr = new ProductManager()) {
        try {
          RowsAffected = mgr.Insert(Entity);

          if (RowsAffected > 0) {
            ResultText = "Insert Successful. Rows Affected: " + RowsAffected.ToString() + Environment.NewLine + "ProductID: " + Entity.ProductID.ToString();
            RaisePropertyChanged("Entity");
          }
        }
        catch (ValidationException ex) {
          ValidationFailed(ex);
        }
        catch (Exception ex) {
          PublishException(ex);
        }
      }
    }
    #endregion

    #region Update Method
    public void Update()
    {
      using (ProductManager mgr = new ProductManager()) {
        try {
          RowsAffected = mgr.Update(Entity);

          if (RowsAffected > 0) {
            ResultText = "Update Successful. Rows Affected: " + RowsAffected.ToString() + Environment.NewLine + "ProductID: " + Entity.ProductID.ToString();
            RaisePropertyChanged("Entity");
          }
        }
        catch (ValidationException ex) {
          ValidationFailed(ex);
        }
        catch (Exception ex) {
          PublishException(ex);
        }
      }
    }
    #endregion

    #region Delete Method
    public void Delete()
    {
      using (ProductManager mgr = new ProductManager()) {
        try {
          RowsAffected = mgr.Delete(Entity);

          if (RowsAffected > 0) {
            ResultText = "Delete Successful. Rows Affected: " + RowsAffected.ToString() + Environment.NewLine + "ProductID: " + Entity.ProductID.ToString();
            RaisePropertyChanged("Entity");
          }
        }
        catch (Exception ex) {
          PublishException(ex);
        }
      }
    }
    #endregion

    #region PerformTransaction Method
    public void PerformTransaction()
    {
      Product prod1 = new Product
      {
        Name = "Transaction 1",
        ProductNumber = "TRN-01",
        Color = "Red",
        StandardCost = 5,
        ListPrice = 10,
        Size = "Small",
        ProductCategoryID = 1,
        ProductModelID = 1,
        SellStartDate = DateTime.Now,
        ModifiedDate = DateTime.Now
      };

      Product prod2 = new Product
      {
        Name = "Transaction 2",
        ProductNumber = "TRN-02",
        Color = "Blue",
        StandardCost = 10,
        ListPrice = 20,
        Size = "Med",
        ProductCategoryID = 1,  // Comment out this line to test rollback
        ProductModelID = 1,
        SellStartDate = DateTime.Now,
        ModifiedDate = DateTime.Now
      };

      // Execute Query and return DataReader
      using (ProductManager mgr = new ProductManager()) {
        using (IDbTransaction trans = mgr.BeginTransaction()) {
          try {
            // Submit the two action statements
            mgr.Insert(prod1);
            mgr.Insert(prod2);

            // Commit the transaction
            mgr.Commit();

            ResultText = "Transaction Committed";
          }
          catch (Exception ex) {
            // Rollback the transaction
            mgr.Rollback();
            ResultText = "Transaction Rolled Back";

            // Publish the exception
            PublishException(ex);
          }
        }
      }
    }
    #endregion
  }
}
