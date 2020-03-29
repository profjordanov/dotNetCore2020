using System;
using System.Windows;
using System.Windows.Controls;
using DataWrapper.Samples.DataLayer;
using DataWrapper.Samples.ViewModelLayer;

namespace DataWrapper.Samples.UserControls
{
  public partial class ProductModifyControl : UserControl
  {
    public ProductModifyControl()
    {
      InitializeComponent();

      _viewModel = (ProductViewModel)this.Resources["viewModel"];

      _viewModel.CreateNewEntity();
    }

    private ProductViewModel _viewModel;    

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

    private void TransactionButton_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.PerformTransaction();
    }
  }
}
