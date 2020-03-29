using System.Reflection;

namespace Common.DataLayer
{
  public class ColumnMapper
  {
    public string ColumnName { get; set; }
    public PropertyInfo ColumnProperty { get; set; }
  }
}
