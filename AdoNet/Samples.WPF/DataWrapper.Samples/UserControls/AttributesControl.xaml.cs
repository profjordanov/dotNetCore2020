using DataWrapper.Samples.ViewModelLayer;
using System.Windows;
using System.Windows.Controls;

namespace DataWrapper.Samples.UserControls
{
  public partial class AttributesControl : UserControl
  {
    public AttributesControl()
    {
      InitializeComponent();

      _viewModel = (ProductCategoryViewModel)this.Resources["viewModel"];
    }

    private readonly ProductCategoryViewModel _viewModel;

    private void GetAllAttributesButton_Click(object sender, RoutedEventArgs e)
    {
      // Get all products
      _viewModel.GetAll();
    }
  }
}