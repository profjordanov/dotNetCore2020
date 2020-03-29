using System.Windows;
using System.Windows.Controls;
using DataWrapper.Samples.DataLayer;
using DataWrapper.Samples.ViewModelLayer;

namespace DataWrapper.Samples.UserControls
{
  public partial class ProductGetSPControl : UserControl
  {
    public ProductGetSPControl()
    {
      InitializeComponent();

      _viewModel = (ProductSPViewModel)this.Resources["viewModel"];
    }

    private readonly ProductSPViewModel _viewModel;

    private void GetAllButton_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.GetAll();
    }

    private void GetAllWithOutputButton_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.GetAllWithOutputParameter();
    }

    private void GetButton_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.Get(680);
    }

    private void SearchButton_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.Search();
    }

    private void CountButton_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.Count();
    }

    private void CountSearchButton_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.CountUsingSearch();
    }
  }
}