using DataWrapper.Samples.ViewModelLayer;
using System.Windows;
using System.Windows.Controls;

namespace DataWrapper.Samples.UserControls
{
  public partial class ProductGetSPDataSetControl : UserControl
  {
    public ProductGetSPDataSetControl()
    {
      InitializeComponent();

      _viewModel = (ProductSPDataSetViewModel)this.Resources["viewModel"];
    }

    private readonly ProductSPDataSetViewModel _viewModel;

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
  }
}