using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataWrapper.Samples.AppLayer;

namespace DataWrapper.Samples.DataLayer
{
  [Table("Product", Schema = "SalesLT")]
  public partial class Product : AppEntityBase
  {
    /// <summary>
    /// Get/Set ProductID
    /// </summary>
    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductID { get; set; }

    /// <summary>
    /// Get/Set Name
    /// </summary>
    [Required]
    [Column()]
    public string Name { get; set; }

    /// <summary>
    /// Get/Set ProductNumber
    /// </summary>
    [Required]
    public string ProductNumber { get; set; }

    /// <summary>
    /// Get/Set Color
    /// </summary>
    public string Color { get; set; }

    /// <summary>
    /// Get/Set StandardCost
    /// </summary>
    [Required]
    public decimal StandardCost { get; set; }

    /// <summary>
    /// Get/Set ListPrice
    /// </summary>
    [Required]
    public decimal ListPrice { get; set; }

    /// <summary>
    /// Get/Set Size
    /// </summary>
    public string Size { get; set; }

    /// <summary>
    /// Get/Set Weight
    /// </summary>
    public decimal? Weight { get; set; }

    /// <summary>
    /// Get/Set ProductCategoryID
    /// </summary>
    public int ProductCategoryID { get; set; }

    /// <summary>
    /// Get/Set ProductModelID
    /// </summary>
    public int ProductModelID { get; set; }


    /// <summary>
    /// Get/Set SellStartDate
    /// </summary>
    [Required]
    public DateTime SellStartDate { get; set; }

    /// <summary>
    /// Get/Set SellEndDate
    /// </summary>
    public DateTime? SellEndDate { get; set; }

    /// <summary>
    /// Get/Set DiscontinuedDate
    /// </summary>
    public DateTime? DiscontinuedDate { get; set; }

    /// <summary>
    /// Get/Set ModifiedDate
    /// </summary>
    [Required]
    public DateTime ModifiedDate { get; set; }

    public override string ToString()
    {
      return ProductNumber + " (" + ProductID.ToString() + ")";
    }
  }
}
