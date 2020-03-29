using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Library.Exceptions
{
  public class ValidationException : Exception
  {
    #region Constructors
    public ValidationException() : base()
    {
      Init();
    }
    public ValidationException(string message) : base(message)
    {
      Init();
    }
    public ValidationException(string message, Exception innerException) : base(message, innerException)
    {
      Init();
    }
    public ValidationException(List<ValidationMessage> messages) : base()
    {
      Init();
      ValidationMessages = messages;
    }
    #endregion

    #region Properties
    public List<ValidationMessage> ValidationMessages { get; set; }
    #endregion

    #region Init Method
    public virtual void Init()
    {
      ValidationMessages = new List<ValidationMessage>();
    }
    #endregion

    #region Override of ToString Method
    /// <summary>
    /// Gathers all information from the exception information gathered and returns a string
    /// </summary>
    /// <returns>A database specific error string</returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder(1024);

      foreach (ValidationMessage item in ValidationMessages) {
        sb.AppendLine("Property Name: " + item.PropertyName + " - Message: " + item.Message);
      }

      return sb.ToString();
    }
    #endregion
  }
}
