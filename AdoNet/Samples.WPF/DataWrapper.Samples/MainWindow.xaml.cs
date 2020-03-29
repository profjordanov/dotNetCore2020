using DataWrapper.Samples.ViewModelLayer;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DataWrapper.Samples
{
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();

      // Connect to instance of the view model created by the XAML
      _viewModel = (MainWindowViewModel)this.Resources["viewModel"];
      
      // Get the original status message
      _originalMessage = _viewModel.StatusMessage;
    }
    
    // Main window's view model class
    private readonly MainWindowViewModel _viewModel = null;
    // Hold the main window's original status message
    private readonly string _originalMessage = string.Empty;

    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {
      MenuItem mnu = (MenuItem)sender;
      string tag;

      // The Tag property contains a command 
      // or the name of a user control to load
      if (mnu.Tag != null) {
        tag = mnu.Tag.ToString();
        if (tag.Contains(".")) {
          // Display a user control
          LoadUserControl(tag);
        }
        else {
          // Process special commands
          ProcessMenuCommands(tag);
        }
      }
    }

    private bool ShouldLoadUserControl(string controlName)
    {
      bool ret = true;

      // Make sure you don't reload a control already loaded.
      if (contentArea.Children.Count > 0) {
        if (((UserControl)contentArea.Children[0]).GetType().Name ==
            controlName.Substring(controlName.LastIndexOf(".") + 1)) {
          ret = false;
        }
      }

      return ret;
    }

    private void LoadUserControl(string controlName)
    {
      Type ucType;
      UserControl uc;

      if (ShouldLoadUserControl(controlName)) {
        // Create a Type from controlName parameter
        ucType = Type.GetType(controlName);
        if (ucType == null) {
          MessageBox.Show("The Control: " + controlName
                           + " does not exist.");
        }
        else {
          // Close current user control in content area
          // NOTE: Optionally add current user control to a list 
          //     so you can restore it when you close the newly added one
          CloseUserControl();

          // Create an instance of this control
          uc = (UserControl)Activator.CreateInstance(ucType);
          if (uc != null) {
            // Display control in content area
            DisplayUserControl(uc);
          }
        }
      }
    }

    private void ProcessMenuCommands(string command)
    {
      switch (command.ToLower()) {
        case "exit":
          this.Close();
          break;
          
        default:
          break;
      }
    }
    
    private void CloseUserControl()
    {
      // Remove current user control
      contentArea.Children.Clear();
      
      // Restore the original status message
      _viewModel.StatusMessage = _originalMessage;
    }

    public void DisplayUserControl(UserControl uc)
    {
      // Add new user control to content area
      contentArea.Children.Add(uc);
    }
  }
}
