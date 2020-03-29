using DataWrapper.Samples.ViewModelLayer;
using System.Windows;
using System.Windows.Controls;

namespace DataWrapper.Samples.UserControls
{
  public partial class ProductGetDataSetControl : UserControl
  {
    public ProductGetDataSetControl()
    {
      InitializeComponent();

      _viewModel = (ProductDataSetViewModel)this.Resources["viewModel"];
    }

    private readonly ProductDataSetViewModel _viewModel;
    
    private void GetAllDataSetButton_Click(object sender, RoutedEventArgs e)
    {
      // Get all products
      _viewModel.GetAll();
    }

    private void GetDataSetButton_Click(object sender, RoutedEventArgs e)
    {
      // Get a single product
      _viewModel.Get(680);
    }

    private void SearchDataSetButton_Click(object sender, RoutedEventArgs e)
    {      
      // Search for Products
      _viewModel.Search();
    }

    private void MultipleResultSetsButton_Click(object sender, RoutedEventArgs e)
    {
      // Get Multiple Result Sets
      _viewModel.MultipleResultSets();
    }
  }
}