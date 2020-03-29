using System;
using System.Collections.ObjectModel;
using Common.Library.Exceptions;

namespace Common.Library
{
  public class ViewModelBase : CommonBase
  {
    #region Private Variables
    private ObservableCollection<ValidationMessage> _ValidationMessages = new ObservableCollection<ValidationMessage>();
    private bool _IsValidationVisible = false;
    private Exception _LastException = null;
    private int _RowsAffected = 0;
    private string _LastExceptionMessage = string.Empty;
    #endregion

    #region Public Properties
    public ObservableCollection<ValidationMessage> ValidationMessages
    {
      get { return _ValidationMessages; }
      set {
        _ValidationMessages = value;
        RaisePropertyChanged("ValidationMessages");
      }
    }

    public bool IsValidationVisible
    {
      get { return _IsValidationVisible; }
      set {
        _IsValidationVisible = value;
        RaisePropertyChanged("IsValidationVisible");
      }
    }

    /// <summary>
    /// Get/Set LastException
    /// </summary>
    public Exception LastException
    {
      get { return _LastException; }
      set {
        _LastException = value;
        LastExceptionMessage = (value == null ? string.Empty : value.Message);
        RaisePropertyChanged("LastException");
      }
    }

    /// <summary>
    /// Get/Set LastExceptionMessage
    /// </summary>
    public string LastExceptionMessage
    {
      get { return _LastExceptionMessage; }
      set {
        _LastExceptionMessage = value;
        RaisePropertyChanged("LastExceptionMessage");
      }
    }

    /// <summary>
    /// Get/Set Rows Affected
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

    #region Init Method
    public virtual void Init()
    {
      ValidationMessages.Clear();
      IsValidationVisible = false;
    }
    #endregion

    #region DisplayStatusMessage Method
    public virtual void DisplayStatusMessage(string msg = "")
    {
    }
    #endregion

    #region Validate Method
    public virtual bool Validate()
    {
      return true;
    }
    #endregion

    #region ValidationFailed Method
    public virtual void ValidationFailed(ValidationException ex)
    {
      ValidationMessages = new ObservableCollection<ValidationMessage>(ex.ValidationMessages);
      IsValidationVisible = true;
    }
    #endregion

    #region PublishException Method
    public void PublishException(Exception ex)
    {
      LastException = ex;
      LastExceptionMessage = ex.ToString();

      // Publish Exception
      ExceptionManager.Instance.Publish(ex);
    }
    #endregion

    #region Close Method
    public virtual void Close(bool wasCancelled = true)
    {
    }
    #endregion
  }
}
