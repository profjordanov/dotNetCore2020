using System;
using System.Windows;
using System.Windows.Controls;
using DataWrapper.Samples.DataLayer;
using DataWrapper.Samples.ViewModelLayer;

namespace DataWrapper.Samples.UserControls
{
  public partial class ProductModifySPControl : UserControl
  {
    public ProductModifySPControl()
    {
      InitializeComponent();

      _viewModel = (ProductSPViewModel)this.Resources["viewModel"];
      
      _viewModel.CreateNewEntity();
    }

    private ProductSPViewModel _viewModel;    

    private void InsertButton_Click(object sender, RoutedEventArgs e)
    {      
      _viewModel.Insert();
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.Update();
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.Delete();
    }
  }
}
