using DataWrapper.Samples.ViewModelLayer;
using System.Windows;
using System.Windows.Controls;

namespace DataWrapper.Samples.UserControls
{
  public partial class ProductGetControl : UserControl
  {
    public ProductGetControl()
    {
      InitializeComponent();

      _viewModel = (ProductViewModel)this.Resources["viewModel"];
    }

    private readonly ProductViewModel _viewModel;
       
    private void GetAllButton_Click(object sender, RoutedEventArgs e)
    {
      // Get all products
      _viewModel.GetAll();
    }

    private void GetButton_Click(object sender, RoutedEventArgs e)
    {
      // Get a single product
      _viewModel.Get(680);
    }

    private void SearchButton_Click(object sender, RoutedEventArgs e)
    {      
      // Search for Products
      _viewModel.Search();
    }

    private void CountButton_Click(object sender, RoutedEventArgs e)
    {
      // Count all Products
      _viewModel.Count();
    }

    private void CountSearchButton_Click(object sender, RoutedEventArgs e)
    {
      // Count Products
      _viewModel.CountUsingSearch();
    }

    private void MultipleResultSetsButton_Click(object sender, RoutedEventArgs e)
    {
      // Get Multiple Result Sets
      _viewModel.MultipleResultSets();
    }
  }
}