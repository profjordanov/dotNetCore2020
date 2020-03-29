using Common.DataLayer.SqlServerClasses;
using System.Data;

namespace DataWrapper.Samples.AppLayer
{
  /// <summary>
  /// Use this class to add any standard parameters, 
  /// properties or methods to all manager classes
  /// </summary>
  public class AppDataManagerBase : SqlServerDataManagerBase
  {
    #region Constructor
    /// <summary>
    /// Pass in either a connection string, or the name in 
    /// the &lt;connectionStrings&gt; element that 
    /// contains the connection string.
    /// </summary>
    public AppDataManagerBase() : base("AdventureWorksLT") { }
    #endregion

    public override void AddStandardParameters()
    {
      base.AddStandardParameters();

      if (CommandObject.CommandType == CommandType.StoredProcedure) {
        // TODO: Add any standard parameters you have in your 
        //       stored procedures for this application

      }
    }

    public override void GetStandardOutputParameters()
    {
      base.GetStandardOutputParameters();

      if (CommandObject.CommandType == CommandType.StoredProcedure) {
        // TODO: Add any standard OUTPUT parameters you have in your 
        //       stored procedures for this application
      }
    }
  }
}
