using Common.Library;

namespace DataWrapper.Samples.AppLayer
{
  /// <summary>
  /// This class is the base class for all view models for this specific application
  /// </summary>
  public class AppViewModelBase : ViewModelBase
  {
    #region Properties
    private string _ResultText;

    public string ResultText
    {
      get { return _ResultText; }
      set {
        _ResultText = value;
        RaisePropertyChanged("ResultText");
      }
    }

    public string EventAction { get; set; }
    public string EventValue { get; set; }
    #endregion

    #region Init Method
    public override void Init()
    {
      base.Init();

      ResultText = string.Empty;
    }
    #endregion
  }
}
