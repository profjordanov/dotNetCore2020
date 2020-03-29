using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataWrapper.Samples.AppLayer;

namespace DataWrapper.Samples.DataLayer
{
  public partial class ProductCategory : AppEntityBase
  {
    /// <summary>
    /// Get/Set ProductCategoryID
    /// </summary>
    [Required]
    [Column("ProductCategoryID")]
    public int Id { get; set; }

    /// <summary>
    /// Get/Set Name
    /// </summary>
    [Required]
    [Column("Name")]
    public string CategoryName { get; set; }

    /// <summary>
    /// Get/Set ModifiedDate
    /// </summary>
    [Required]
    public DateTime ModifiedDate { get; set; }

    public override string ToString()
    {
      return CategoryName + " (" + Id.ToString() + ")";
    }
  }
}
