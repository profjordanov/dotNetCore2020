using Common.Library.Exceptions;
using DataWrapper.Samples.DataLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DataWrapper.Samples.ViewModelLayer
{
  public class ProductSPViewModel : ProductViewModelBase
  {
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
      }
    }
    #endregion

    #region GetAll Method
    public void GetAll()
    {
      using (ProductSPManager mgr = new ProductSPManager()) {
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

    #region GetAllWithOutputParameter Method
    public void GetAllWithOutputParameter()
    {
      using (ProductSPManager mgr = new ProductSPManager()) {
        try {
          DataCollection = mgr.GetAllWithOutputParameter();
          RowsAffected = mgr.RowsAffected;

          ResultText = "Check the Output Window for OUTPUT parameter value";
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
      using (ProductSPManager mgr = new ProductSPManager()) {
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
      using (ProductSPManager mgr = new ProductSPManager()) {
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
      using (ProductSPManager mgr = new ProductSPManager()) {
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
      using (ProductSPManager mgr = new ProductSPManager()) {
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

    #region Insert Method
    public void Insert()
    {
      using (ProductSPManager mgr = new ProductSPManager()) {
        try {
          RowsAffected = mgr.Insert(Entity);

          if (RowsAffected > 0) {
            ResultText = "Insert Successful. Rows Affected: " + RowsAffected.ToString() + Environment.NewLine + "ProductID: " + Entity.ProductID.ToString() + Environment.NewLine + "Return_Value: " + Entity.RETURN_VALUE.ToString();
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
      using (ProductSPManager mgr = new ProductSPManager()) {
        try {
          RowsAffected = mgr.Update(Entity);

          if (RowsAffected > 0) {
            ResultText = "Update Successful. Rows Affected: " + RowsAffected.ToString() + Environment.NewLine + "ProductID: " + Entity.ProductID.ToString() + Environment.NewLine + "Return_Value: " + Entity.RETURN_VALUE.ToString();
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
      using (ProductSPManager mgr = new ProductSPManager()) {
        try {
          RowsAffected = mgr.Delete(Entity);

          if (RowsAffected > 0) {
            ResultText = "Delete Successful. Rows Affected: " + RowsAffected.ToString() + Environment.NewLine + "ProductID: " + Entity.ProductID.ToString() + Environment.NewLine + "Return_Value: " + Entity.RETURN_VALUE.ToString();
            RaisePropertyChanged("Entity");
          }
        }
        catch (Exception ex) {
          PublishException(ex);
        }
      }
    }
    #endregion
  }
}
