using System.ComponentModel.DataAnnotations.Schema;
using Common.Library.BaseClasses;

namespace Common.DataLayer.SqlServerClasses
{
  public class SqlServerEntityBase : EntityBase
  {
    [NotMapped]
    public int RETURN_VALUE { get; set; }
  }
}
