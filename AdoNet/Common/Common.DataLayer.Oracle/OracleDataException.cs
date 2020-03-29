using Common.Library.Exceptions;
#if ORACLE
using Oracle.DataAccess.Client;
#endif
using System;
using System.Text;

namespace Common.DataLayer.OracleClasses
{
#if ORACLE
    public class OracleDataException : DataException
    {
  #region Constructors
        public OracleDataException() : base() { }
        public OracleDataException(string message) : base(message) { }
        public OracleDataException(string message, Exception innerException) : base(message, innerException) { }
  #endregion

  #region GetDatabaseSpecificError Method
        /// <summary>
        /// Create a string with as much specific database error as possible
        /// </summary>
        /// <param name="ex">The exception</param>
        /// <returns>A string</returns>
        public override string GetDatabaseSpecificError(Exception ex)
        {
            OracleException exp = null;
            StringBuilder sb = new StringBuilder(1024);

            if (ex is OracleException)
            {
                exp = ((OracleException)(ex));

                for (int index = 0; index <= exp.Errors.Count - 1; index++)
                {                    
                    sb.AppendLine(new string('*', 40));
                    sb.AppendLine("**** BEGIN: Oracle Exception #" + (index + 1).ToString() + " ****");
                    sb.AppendLine("    Type: " + exp.Errors[index].GetType().FullName);
                    sb.AppendLine("    Message: " + exp.Errors[index].Message);
                    sb.AppendLine("    Source: " + exp.Errors[index].Source);
                    sb.AppendLine("    Number: " + exp.Errors[index].Number.ToString());
                    sb.AppendLine("    Procedure: " + exp.Errors[index].Procedure);
                    sb.AppendLine("    BindIndex: " + exp.Errors[index].ArrayBindIndex.ToString());
                    sb.AppendLine("**** END: Oracle Exception #" + (index + 1).ToString() + " ****");
                    sb.AppendLine(new string('*', 40));
                }
            }
            else
            {
                sb.Append(ex.Message);
            }

            return sb.ToString();
        }
  #endregion

  #region IsDatabaseSpecificError Method
        public override bool IsDatabaseSpecificError(Exception ex)
        {
            return (ex is OracleException);
        }
  #endregion
    }
#endif
}
