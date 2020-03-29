using System;
using System.ComponentModel;
using System.Reflection;

namespace Common.Library
{
  /// <summary>
  /// This class implements the INotifyPropertyChanged Event Procedure
  /// </summary>
  public class CommonBase : INotifyPropertyChanged
  {
    #region INotifyPropertyChanged
    /// <summary>
    /// The PropertyChanged Event to raise to any UI object
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// The PropertyChanged Event to raise to any UI object
    /// The event is only invoked if data binding is used
    /// </summary>
    /// <param name="propertyName">The property name that is changing</param>
    protected void RaisePropertyChanged(string propertyName)
    {
      // Grab a handler
      PropertyChangedEventHandler handler = this.PropertyChanged;
      // Only raise event if handler is connected
      if (handler != null) {
        PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);

        // Raise the PropertyChanged event.
        handler(this, args);
      }
    }
    #endregion

    #region Clone Method
    public void Clone<T>(T original, T cloneTo)
    {
      if (original != null && cloneTo != null) {
        // Use reflection so the RaisePropertyChanged event is fired for each property
        foreach (var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)) {
          var value = prop.GetValue(original, null);
          prop.SetValue(cloneTo, value, null);
        }
      }
    }
    #endregion
  }
}
