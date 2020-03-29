using ADONET_Samples.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ADONET_Samples.UserControls
{
  public partial class DataRowColumnControl : UserControl
  {
    public DataRowColumnControl()
    {
      InitializeComponent();

      _viewModel = (DataRowColumnViewModel)this.Resources["viewModel"];
    }

    private readonly DataRowColumnViewModel _viewModel;
      
    private void BuildDataTable_Click(object sender, RoutedEventArgs e)
    {
      grdProducts.DataContext = _viewModel.BuildDataTable().DefaultView;
    }

    private void CloneDataTable_Click(object sender, RoutedEventArgs e)
    {
      grdProducts.DataContext = _viewModel.CloneDataTable().DefaultView;
    }

    private void CopyDataTable_Click(object sender, RoutedEventArgs e)
    {
      grdProducts.DataContext = _viewModel.CopyDataTable().DefaultView;
    }

    private void SelectUsingRowByRow_Click(object sender, RoutedEventArgs e)
    {
      grdProducts.DataContext = _viewModel.SelectCopyRowByRow().DefaultView;
    }

    private void SelectUsingCopyToDataTable_Click(object sender, RoutedEventArgs e)
    {
      grdProducts.DataContext = _viewModel.SelectUsingCopyToDataTable().DefaultView;
    }
  }
}
