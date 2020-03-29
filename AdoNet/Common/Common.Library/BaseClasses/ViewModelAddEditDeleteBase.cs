namespace Common.Library
{
  public class ViewModelAddEditDeleteBase : ViewModelBase
  {
    #region Private Variables
    private bool _IsListEnabled = true;
    private bool _IsDetailEnabled = false;
    private bool _IsAddMode = false;
    #endregion

    #region Public Properties
    public bool IsListEnabled
    {
      get { return _IsListEnabled; }
      set {
        _IsListEnabled = value;
        RaisePropertyChanged("IsListEnabled");
      }
    }

    public bool IsDetailEnabled
    {
      get { return _IsDetailEnabled; }
      set {
        _IsDetailEnabled = value;
        RaisePropertyChanged("IsDetailEnabled");
      }
    }

    public bool IsAddMode
    {
      get { return _IsAddMode; }
      set {
        _IsAddMode = value;
        RaisePropertyChanged("IsAddMode");
      }
    }
    #endregion

    #region BeginEdit Method
    public virtual void BeginEdit(bool isAddMode = false)
    {
      IsListEnabled = false;
      IsDetailEnabled = true;
      IsAddMode = isAddMode;
    }
    #endregion

    #region CancelEdit Method
    public virtual void CancelEdit()
    {
      base.Init();

      IsListEnabled = true;
      IsDetailEnabled = false;
      IsAddMode = false;
    }
    #endregion

    #region Save Method
    public virtual bool Save()
    {
      return true;
    }
    #endregion

    #region Delete Method
    public virtual bool Delete()
    {
      return true;
    }
    #endregion
  }
}
