using System.ComponentModel.DataAnnotations.Schema;
using Common.Library.BaseClasses;

namespace Common.DataLayer.OracleClasses
{
  public class OracleEntityBase : EntityBase
  {
    [NotMapped]
    public int RETURN_VALUE { get; set; }
  }
}
