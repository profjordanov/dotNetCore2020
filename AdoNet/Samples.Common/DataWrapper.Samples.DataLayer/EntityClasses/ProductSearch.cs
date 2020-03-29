namespace DataWrapper.Samples.DataLayer
{
  public class ProductSearch
  {
    /// <summary>
    /// Get/Set Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Get/Set ProductNumber
    /// </summary>
    public string ProductNumber { get; set; }

    /// <summary>
    /// Get/Set Beginning Cost to search for
    /// </summary>
    public decimal? BeginningCost { get; set; }

    /// <summary>
    /// Get/Set Ending Cost to search for
    /// </summary>
    public decimal? EndingCost { get; set; }
  }
}
