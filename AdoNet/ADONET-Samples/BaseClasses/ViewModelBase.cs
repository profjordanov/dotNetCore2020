namespace ADONET_Samples
{
  public class ViewModelBase : CommonBase
  {
    #region Private variables
    private string _ResultText;
    private int _RowsAffected;
    #endregion

    #region Public Properties
    /// <summary>
    /// Get/Set Result Text
    /// </summary>
    public string ResultText
    {
      get { return _ResultText; }
      set {
        _ResultText = value;
        RaisePropertyChanged("ResultText");
      }
    }

    /// <summary>
    /// Get/Set Rows Affected by last SQL statement
    /// </summary>
    public int RowsAffected
    {
      get { return _RowsAffected; }
      set {
        _RowsAffected = value;
        RaisePropertyChanged("RowsAffected");
      }
    }
    #endregion
  }
}
