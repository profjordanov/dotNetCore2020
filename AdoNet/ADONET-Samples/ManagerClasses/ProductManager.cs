using System.Data;
using System.Data.SqlClient;

namespace ADONET_Samples.ManagerClasses
{
  public class ProductManager
  {
    public const string PRODUCT_SQL = "SELECT ProductId, ProductName, IntroductionDate, Url, Price, RetireDate, ProductCategoryId FROM Product";
    public const string PRODUCT_CATEGORY_SQL = "SELECT ProductCategoryId, CategoryName FROM ProductCategory";

    #region GetProductsAsDataTable Method
    public DataTable GetProductsAsDataTable()
    {
      DataTable ret = null;

      // Create a connection
      using (SqlConnection ConnectionObject = new SqlConnection(AppSettings.ConnectionString))
      {
        // Open the connection
        ConnectionObject.Open();

        // Create command object
        using (SqlCommand CommandObject = new SqlCommand(PRODUCT_SQL, ConnectionObject))
        {
          // Create a SQL Data Adapter
          using (SqlDataAdapter da = new SqlDataAdapter(CommandObject))
          {
            // Fill DataTable using Data Adapter
            ret = new DataTable();
            da.Fill(ret);
          }
        }
      }

      return ret;
    }
    #endregion
  }
}
