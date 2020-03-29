using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Common.DataLayer.SqlServerClasses
{
  public class SqlServerDataManagerBase : DataManagerBase
  {
    #region Constructor
    public SqlServerDataManagerBase(string nameOrConnectionString) : base(nameOrConnectionString) { }
    #endregion

    #region Properties
    public int RETURN_VALUE { get; set; }
    #endregion
    
    #region Initialize Method
    /// <summary>
    /// This method is called by the constructor
    /// </summary>
    protected override void Initialize()
    {
      base.Initialize();

      CommandObject = new SqlCommand();
      ParameterToken = "@";
    }
    #endregion

    #region Reset Methods
    public override void Reset(CommandType type)
    {
      base.Reset(type);

      if (CommandObject == null) {
        CommandObject = new SqlCommand
        {
          CommandType = type
        };
      }
      
      RETURN_VALUE = 0;
    }
    #endregion

    #region CreateConnection Method
    public override IDbConnection CreateConnection(string connectString)
    {
      return new SqlConnection(connectString);
    }
    #endregion

    #region CreateCommand Method
    public override IDbCommand CreateCommand()
    {
      return new SqlCommand();
    }
    #endregion

    #region CreateDataAdapter Method
    public override DbDataAdapter CreateDataAdapter(IDbCommand cmd)
    {
      return new SqlDataAdapter((SqlCommand)cmd);
    }
    #endregion

    #region CreateParameter Methods
    public override IDbDataParameter CreateParameter(string name, object value, bool isNullable)
    {
      // Ensure parameter name contains token
      name = name.Contains(ParameterToken) ? name : ParameterToken + name;
      // Add parameter
      return new SqlParameter { ParameterName = name, Value = value, IsNullable = isNullable };
    }

    public override IDbDataParameter CreateParameter(string name, object value, bool isNullable, System.Data.DbType type, System.Data.ParameterDirection direction = System.Data.ParameterDirection.Input)
    {
      // Ensure parameter name contains token
      name = name.Contains(ParameterToken) ? name : ParameterToken + name;
      // Add parameter
      return new SqlParameter { ParameterName = name, Value = value, IsNullable = isNullable, DbType = type, Direction = direction };
    }

    public override IDbDataParameter CreateParameter(string name, object value, bool isNullable, System.Data.DbType type, int size, System.Data.ParameterDirection direction = System.Data.ParameterDirection.Input)
    {
      // Ensure parameter name contains token
      name = name.Contains(ParameterToken) ? name : ParameterToken + name;
      // Add parameter
      return new SqlParameter { ParameterName = name, Value = value, IsNullable = isNullable, DbType = type, Direction = direction, Size = size };
    }
    #endregion
    
    #region AddStandardParameters Method
    public override void AddStandardParameters()
    {
      if (CommandObject.CommandType == CommandType.StoredProcedure) {
        AddParameter("RETURN_VALUE", 0, false, DbType.Int32, ParameterDirection.ReturnValue);
      }
    }
    #endregion

    #region GetOutputParameters Method
    public override void GetStandardOutputParameters()
    {
      if (CommandObject.CommandType == CommandType.StoredProcedure) {
        RETURN_VALUE = GetParameterValue<int>("RETURN_VALUE", default(int));
      }
    }
    #endregion

    #region GetParameterValue Method
    public override T GetParameterValue<T>(string name, object defaultValue)
    {
      T ret;
      string value;

      value = ((SqlParameter)GetParameter(name)).Value.ToString();
      if (string.IsNullOrEmpty(value)) {
        ret = (T)defaultValue;
      }
      else {
        ret = (T)Convert.ChangeType(value, typeof(T));
      }

      return ret;
    }
    #endregion

    #region ThrowDbException Method
    public override void ThrowDbException(Exception ex, IDbCommand cmd, string exceptionMsg = "")
    {
      Exceptions.DataException exc;
      exceptionMsg = string.IsNullOrEmpty(exceptionMsg) ? string.Empty : exceptionMsg + " - ";

      if (ex is SqlException) {
        exc = new SqlServerDataException(exceptionMsg + ex.Message, ex)
        {
          ConnectionString = cmd.Connection.ConnectionString,
          ConnectStringNameInConfigFile = ConnectStringName,
          Database = cmd.Connection.Database,
          SQL = SQL,
          CommandParameters = cmd.Parameters,
          WorkstationId = Environment.MachineName
        };
      }
      else {
        exc = new Common.DataLayer.Exceptions.DataException(exceptionMsg + ex.Message, ex)
        {
          ConnectionString = cmd.Connection.ConnectionString,
          ConnectStringNameInConfigFile = ConnectStringName,
          Database = cmd.Connection.Database,
          SQL = SQL,
          CommandParameters = cmd.Parameters,
          WorkstationId = Environment.MachineName
        };
      }

      // Set the last exception
      LastException = exc;

      // Throw the exception
      throw exc;
    }
    #endregion
  }
}
