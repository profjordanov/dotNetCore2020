using DataWrapper.Samples.AppLayer;
using DataWrapper.Samples.DataLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DataWrapper.Samples.ViewModelLayer
{
  public class ProductViewModelBase : AppViewModelBase
  {
    #region Constructor
    public ProductViewModelBase()
    {
      _SearchEntity = new ProductSearch
      {
        //Name = "Cl",
        //ProductNumber = "VE-C304-L",

        BeginningCost = 1,
        EndingCost = 4
      };
    }
    #endregion

    #region Properties
    private Product _Entity = new Product();
    private ProductSearch _SearchEntity = new ProductSearch();
    private List<Product> _DataCollection = new List<Product>();
    
    public Product Entity
    {
      get { return _Entity; }
      set {
        _Entity = value;
        RaisePropertyChanged("Entity");
      }
    }

    public ProductSearch SearchEntity
    {
      get { return _SearchEntity; }
      set {
        _SearchEntity = value;
        RaisePropertyChanged("SearchEntity");
      }
    }

    public List<Product> DataCollection
    {
      get { return _DataCollection; }
      set {
        _DataCollection = value;
        RaisePropertyChanged("DataCollection");
      }
    }
    #endregion

    #region Init Method
    public override void Init()
    {
      DataCollection = new List<Product>();
      Entity = new Product();
      SearchEntity = new ProductSearch();

      base.Init();
    }
    #endregion

    #region CreateNewEntity Method
    public void CreateNewEntity()
    {
      Entity = new Product
      {
        Name = "TEST",
        ProductNumber = "TEST-01",
        Color = "Red",
        StandardCost = 1.50M,
        ListPrice = 5.00M,
        Size = "99",
        Weight = 100M,
        ProductCategoryID = 6,
        ProductModelID = 30,
        SellStartDate = DateTime.Now,
        ModifiedDate = DateTime.Now
      };
    }
    #endregion
  }
}
