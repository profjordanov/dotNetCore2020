using System;

namespace ADONET_Samples
{
  public class Product : CommonBase
  {
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public DateTime IntroductionDate { get; set; }
    public string Url { get; set; }
    public decimal Price { get; set; }
    public DateTime? RetireDate { get; set; }
    public int? ProductCategoryId { get; set; }
  }
}
