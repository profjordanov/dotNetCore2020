using DataWrapper.Samples.AppLayer;

namespace DataWrapper.Samples.ViewModelLayer
{
  public class MainWindowViewModel : AppViewModelBase
  {
    #region Private Variables
    private string _StatusMessage;
    #endregion

    #region Public Properties   
    public string StatusMessage
    {
      get { return _StatusMessage; }
      set {
        _StatusMessage = value;
        RaisePropertyChanged("StatusMessage");
      }
    }
    #endregion
  }
}
