using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace WPF.Common
{
  /// <summary>
  /// Call this converter to make a control Visible when a string value is not empty
  /// </summary>
  public class StringNotEmptyVisibilityConverter : IValueConverter
  { 
    /// <summary>
    /// Convert a True/False value to Visibility.Visible/Visibility.Collapsed value
    /// </summary>
    /// <param name="value">A boolean value</param>
    /// <param name="targetType">The type of object</param>
    /// <param name="parameter">Any parameters passed via XAML</param>
    /// <param name="culture">The current culture</param>
    /// <returns>A Visibility Enumeration</returns>
    public object Convert(object value, Type targetType,
                          object parameter, CultureInfo culture)
    {
      string str = value == null ? string.Empty : value.ToString();

      if (string.IsNullOrEmpty(str))
        return Visibility.Collapsed;
      else
        return Visibility.Visible;
    }

    /// <summary>
    /// NOT IMPLEMENTED
    /// </summary>
    /// <param name="value">A boolean value</param>
    /// <param name="targetType">The type of object</param>
    /// <param name="parameter">Any parameters passed via XAML</param>
    /// <param name="culture">The current culture</param>
    /// <returns>NOT IMPLEMENTED</returns>
    public object ConvertBack(object value, Type targetType,
                              object parameter, CultureInfo culture)
    {
      throw new NotImplementedException("StringNotEmptyVisibilityConverter ConvertBack Method Not Implemented");
    }
  }
}