using System;
using System.Data.SqlClient;
using System.Text;
using Common.DataLayer.Exceptions;

namespace Common.DataLayer.SqlServerClasses
{
  public class SqlServerDataException : DataException
  {
    #region Constructors
    public SqlServerDataException() : base() { }
    public SqlServerDataException(string message) : base(message) { }
    public SqlServerDataException(string message, Exception innerException) : base(message, innerException) { }
    #endregion

    #region GetDatabaseSpecificError Method
    /// <summary>
    /// Create a string with as much specific database error as possible
    /// </summary>
    /// <param name="ex">The exception</param>
    /// <returns>A string</returns>
    public override string GetDatabaseSpecificError(Exception ex)
    {
      SqlException exp;
      StringBuilder sb = new StringBuilder(1024);

      if (ex is SqlException) {
        exp = ((SqlException)(ex));

        for (int index = 0; index <= exp.Errors.Count - 1; index++) {
          sb.AppendLine(new string('*', 40));
          sb.AppendLine("**** BEGIN: SQL Server Exception #" + (index + 1).ToString() + " ****");
          sb.AppendLine("    Type: " + exp.Errors[index].GetType().FullName);
          sb.AppendLine("    Message: " + exp.Errors[index].Message);
          sb.AppendLine("    Source: " + exp.Errors[index].Source);
          sb.AppendLine("    Number: " + exp.Errors[index].Number.ToString());
          sb.AppendLine("    State: " + exp.Errors[index].State.ToString());
          sb.AppendLine("    Class: " + exp.Errors[index].Class.ToString());
          sb.AppendLine("    Server: " + exp.Errors[index].Server);
          sb.AppendLine("    Procedure: " + exp.Errors[index].Procedure);
          sb.AppendLine("    LineNumber: " + exp.Errors[index].LineNumber.ToString());
          sb.AppendLine("**** END: SQL Server Exception #" + (index + 1).ToString() + " ****");
          sb.AppendLine(new string('*', 40));
        }
      }
      else {
        sb.Append(ex.Message);
      }

      return sb.ToString();
    }
    #endregion

    #region IsDatabaseSpecificError Method
    public override bool IsDatabaseSpecificError(Exception ex)
    {
      return (ex is SqlException);
    }
    #endregion
  }
}
