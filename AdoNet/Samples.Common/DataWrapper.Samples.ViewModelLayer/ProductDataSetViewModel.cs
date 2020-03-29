using DataWrapper.Samples.DataLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DataWrapper.Samples.ViewModelLayer
{
  public class ProductDataSetViewModel : ProductViewModelBase
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
        case "resetsearch":
          Init();
          break;
        case "multipleresults":
          MultipleResultSets();
          break;
      }
    }
    #endregion

    #region GetAll Method
    public void GetAll()
    {
      System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
      stopWatch.Start();

      using (ProductDataSetManager mgr = new ProductDataSetManager()) {
        try {
          DataCollection = mgr.GetAll();
          RowsAffected = mgr.RowsAffected;
          ResultText = "Rows Affected: " + RowsAffected.ToString();
        }
        catch (Exception ex) {
          PublishException(ex);
        }
      }

      stopWatch.Stop();
      // Get the elapsed time as a TimeSpan value.
      TimeSpan ts = stopWatch.Elapsed;
      // Format and display the TimeSpan value
      ResultText += " - RunTime " + String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
      Console.WriteLine(ResultText);
    }
    #endregion

    #region Get Method
    public Product Get(int productId)
    {
      DataCollection = new List<Product>();
      using (ProductDataSetManager mgr = new ProductDataSetManager()) {
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
      using (ProductDataSetManager mgr = new ProductDataSetManager()) {
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

    #region MultipleResultSets Method
    public void MultipleResultSets()
    {
      using (ProductDataSetManager mgr = new ProductDataSetManager()) {
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
  }
}
