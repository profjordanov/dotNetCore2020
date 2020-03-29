using System.Configuration;

namespace ADONET_Samples
{
  public static class AppSettings
  {
    public static string ConnectionString
    {
      get { return ConfigurationManager.ConnectionStrings["ADONETSamples"].ConnectionString; }
    }
  }
}
