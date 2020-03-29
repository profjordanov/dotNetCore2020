using DataWrapper.Samples.AppLayer;
using DataWrapper.Samples.DataLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DataWrapper.Samples.ViewModelLayer
{
  public class ProductCategoryViewModel : AppViewModelBase
  {
    #region Properties
    private List<ProductCategory> _DataCollection = new List<ProductCategory>();

    public List<ProductCategory> DataCollection
    {
      get { return _DataCollection; }
      set {
        _DataCollection = value;
        RaisePropertyChanged("DataCollection");
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
      }
    }
    #endregion

    #region Init Method
    public override void Init()
    {
      DataCollection = new List<ProductCategory>();
     
      base.Init();
    }
    #endregion

    #region GetAll Method
    public void GetAll()
    {
      System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
      stopWatch.Start();

      using (ProductCategoryManager mgr = new ProductCategoryManager()) {
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
      // Format and display the TimeSpan value.
      ResultText += " - RunTime " + String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
      Console.WriteLine(ResultText);
    }
    #endregion
  }
}
