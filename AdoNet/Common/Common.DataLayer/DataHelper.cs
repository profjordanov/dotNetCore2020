using System;
using System.Data;

namespace Common.DataLayer
{
  public static class DataHelper
  {
    public static T GetFieldValue<T>(this IDataReader dr, string name)
    {
      T ret = default;

      if (!dr[name].Equals(DBNull.Value)) {
        ret = (T)dr[name];
      }

      return ret;
    }
  }
}
