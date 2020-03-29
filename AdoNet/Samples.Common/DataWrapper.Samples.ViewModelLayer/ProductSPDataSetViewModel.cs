using DataWrapper.Samples.DataLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DataWrapper.Samples.ViewModelLayer
{
  public class ProductSPDataSetViewModel : ProductViewModelBase
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
        case "resetsearch":
          Init();
          break;
      }
    }
    #endregion

    #region GetAll Method
    public void GetAll()
    {
      using (ProductSPDataSetManager mgr = new ProductSPDataSetManager()) {
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
      using (ProductSPDataSetManager mgr = new ProductSPDataSetManager()) {
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
      using (ProductSPDataSetManager mgr = new ProductSPDataSetManager()) {
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
      using (ProductSPDataSetManager mgr = new ProductSPDataSetManager()) {
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
  }
}
