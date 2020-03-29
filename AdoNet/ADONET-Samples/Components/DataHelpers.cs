using System;
using System.Data.SqlClient;

namespace ADONET_Samples
{
  public static class DataReaderHelpers
  {
    public static T GetFieldValue<T>(this SqlDataReader dr, string name)
    {
      T ret = default;

      if (!dr[name].Equals(DBNull.Value)) {
        ret = (T)dr[name];
      }

      return ret;
    }
  }
}
